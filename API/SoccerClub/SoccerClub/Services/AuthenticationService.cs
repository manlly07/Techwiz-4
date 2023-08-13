using BSS;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SoccerClub.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SoccerClub.Services
{
    public interface IAuthenticationService
    {
        public string Register(RegisterVM register);
        public string Login(LoginVM login, out TokenVM UserToken);
        public string CheckToken(TokenVM token, out dynamic result);
    }
    public class AuthenticationService : IAuthenticationService
    {
        public string CheckToken(TokenVM token, out dynamic result)
        {
            result = new ExpandoObject();
            string msg = string.Empty;
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var TokenValidationParam = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8qUAIZa51UOZH5dEAQEgvA")),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };




            try
            {
                var tokenInverification = jwtTokenHandler.ValidateToken(token.AcctessToken,
                    TokenValidationParam, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var chekc = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!chekc)
                    {
                        result.ApiResponVm = new ApiResponseVM
                        {
                            Success = false,
                            Error = "Invalid token"

                        };
                    }
                    var utcExpireDate = long.Parse(tokenInverification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                    if (expireDate > DateTime.UtcNow)
                    {
                        result.ApiResponVm = new ApiResponseVM
                        {
                            Success = true

                        };
                    }
                    using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
                    {
                        connection.Open();
                        var paremter = new
                        {
                            Token = token.RefreshToken
                        };
                        RefreshTokenVm UserToken = connection.Query<RefreshTokenVm>("usp_UserToken_GetToken", paremter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        if (UserToken == null)
                        {
                            return msg = $"Không tồn tại Token : {token.RefreshToken}";
                        }
                        var UserID = new
                        {
                            UserToken.UserID
                        };

                        UserTokenVM user = connection.Query<UserTokenVM>("usp_UserToken_GetUserID", UserID, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        if (user == null)
                        {
                            msg = GenerateToken(user, out token);
                            if (msg.Length > 0) return msg;
                        }
                        result.ApiResponVm = new ApiResponseVM
                        {
                            Success = true,
                            data = token

                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result.ApiResponVm = new ApiResponseVM
                {
                    Success = false,
                    Error = "Something went wrong"

                };
            }
            return string.Empty;
        }
    
        private string GenerateToken(UserTokenVM userToken, out TokenVM UserToken)
        {
            UserToken = new();
            string msg = string.Empty;
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes("8qUAIZa51UOZH5dEAQEgvA");

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userToken.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, userToken.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, userToken.Email),
                    new Claim("UserID", userToken.UserID.ToString()),
                    new Claim("RoleType", userToken.RoleType.ToString()),
                    new Claim("Token", HashHEX(userToken.UserID.ToString()))
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            UserToken.AcctessToken = jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescription));
            UserToken.RefreshToken = HashHEX(userToken.UserID.ToString());
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
               

                var parameter = new
                {
                    userToken.UserID,
                    Token = HashHEX(userToken.UserID.ToString()),
                    CreateDate = DateTime.Now,
                    ExpiredDate = DateTime.Now.AddDays(1)
                };
                connection.Execute("usp_UserToken_Insert", parameter, commandType: CommandType.StoredProcedure);
            };

            return string.Empty;

        }

        private string HashHEX(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển đổi mật khẩu sang dạng byte[]
                byte[] bytes = Encoding.UTF8.GetBytes(text);

                // Mã hóa mật khẩu và trả về dạng HEX (hexadecimal) string
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // Chuyển byte sang dạng HEX
                }
                return builder.ToString();
            }
        }
        public string Login(LoginVM login, out TokenVM UserToken)
        {
            string msg = string.Empty;
            UserToken = new();
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var check = new
                {
                    UserName = login.UserName.ToLower(),
                    Password = HashHEX(login.Password)
                };
                var userToken = connection.Query<UserTokenVM>("usp_User_Login", check, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (userToken == null) return msg = $"Bạn nhập sai tài khoản/mật khẩu.";

                UserTokenVM user = connection.Query<UserTokenVM>("usp_UserToken_GetUserID",new { userToken.UserID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (user == null)
                {
                    msg = GenerateToken(userToken, out UserToken);
                    if (msg.Length > 0) return msg;
                }
                
            }
           
            return string.Empty;
        }
        public string Register(RegisterVM register)
        {
            string msg = string.Empty;

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var check = new
                {
                    UserName = register.UserName.ToLower(),
                };
                var ExistUserName = connection.Query<RegisterVM>("usp_User_Check", check, commandType: CommandType.StoredProcedure);
                if (ExistUserName.Count() != 0) return msg = $"Đã tổn tại UserName : {register.UserName} .";

                var parameter = new
                {
                    UserID = Guid.NewGuid(),
                    register.UserName,
                    Password = HashHEX(register.Password),
                    register.FirstName,
                    register.LastName,
                    register.Email,
                    register.PhoneNumber,
                    register.BirthDay,
                    CreateDate = DateTime.Now,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_User_Resgister", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}
