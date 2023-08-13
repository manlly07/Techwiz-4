using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface ICountryService
    {
        public string GetList(CountrySearchVM search, out object result);
        public string Insert(CountryInsertVM insert);
        public string Update(CountryUpdateVM update);
        public string Delete(int CountryID);
        public string GetDetail(int CountryID, out object result);
    }
    public class CountryService : ICountryService
    {
        public string GetList(CountrySearchVM search, out dynamic result)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    search.TextSearch,
                };
                var Country = connection.Query<GetCountryVM>("usp_Country_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Country.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Country.Count();

            };
            return string.Empty;
        }
        public string GetDetail(int CountryID, out dynamic result)
        {
            string msg = string.Empty;

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    CountryID
                };
                var Country = connection.Query<GetCountryVM>("usp_Country_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Country;

                if (result.data.Count == 0) return msg = $"Không tồn tại CountryID : {CountryID}!";
            };
            return string.Empty;
        }

        public string Delete(int CountryID)
        {
            string msg = GetDetail(CountryID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result == null) return $"Không tồn tại CountryID : {CountryID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    CountryID
                };
                connection.Execute("usp_Country_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(CountryInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    insert.CountryName,
                    CreateDate = DateTime.Now,
                    insert.Description,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Country_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(CountryUpdateVM update)
        {
            string msg = GetDetail(update.CountryID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CountryID : {update.CountryID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.CountryID,
                    update.CountryName,

                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Country_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
