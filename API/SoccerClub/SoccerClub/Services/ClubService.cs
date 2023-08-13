using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IClubService
    {
        public string GetList(ClubSearchVM search, out object result);
        public string Insert(ClubInsertVM insert);
        public string Update(ClubUpdateVM update);
        public string Delete(Guid ClubID);
        public string GetDetail(Guid ClubID, out object result);
    }
    public class ClubService : IClubService
    {
        public string GetList(ClubSearchVM search, out dynamic result)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    search.TextSearch,
                    search.FromDate,
                    search.ToDate
                };
                var Club = connection.Query<GetClubVM>("usp_Club_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Club.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Club.Count();

            };
            return string.Empty;
        }
        public string GetDetail(Guid ClubID, out dynamic result)
        {
            string msg = string.Empty;

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    ClubID
                };
                var Club = connection.Query<GetClubVM>("usp_Club_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Club;

                if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {ClubID}!";
            };
            return string.Empty;
        }

        public string Delete(Guid ClubID)
        {
            string msg = GetDetail(ClubID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {ClubID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    ClubID
                };
                connection.Execute("usp_Club_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(ClubInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    ClubID = Guid.NewGuid(),
                    insert.ClubName,
                    insert.CountryID,
                    CreateDate = DateTime.Now,
                    insert.Description,
                    insert.Logo,
                    insert.Founding
                };
                connection.Execute("usp_Club_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(ClubUpdateVM update)
        {
            string msg = GetDetail(update.ClubID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {update.ClubID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.ClubID,
                    update.ClubName,
                    update.CountryID,
                    ModifeDate = DateTime.Now,
                    update.Description,
                    update.Founding,
                    update.Logo
                };
                connection.Execute("usp_Club_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
