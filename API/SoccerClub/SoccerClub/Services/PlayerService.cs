using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IPlayerService
    {
        public string GetList(PlayerSearchVM search, out object result);
        public string Insert(PlayerInsertVM insert);
        public string Update(PlayerUpdateVM update);
        public string Delete(Guid PlayerID);
        public string GetDetail(Guid PlayerID, out object result);
    }
    public class PlayerService : IPlayerService
    {
        public string GetList(PlayerSearchVM search, out dynamic result)
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
                var Player = connection.Query<GetPlayerVM>("usp_Player_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Player.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Player.Count();

            };
            return string.Empty;
        }
        public string GetDetail(Guid PlayerID, out dynamic result)
        {
            string msg = string.Empty;
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    PlayerID
                };
                var Player = connection.Query<GetPlayerVM>("usp_Player_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Player;
                if (result.data.Count == 0) return msg = $"Không tồn tại PlayerID : {PlayerID}!";
            };
            return string.Empty;
        }

        public string Delete(Guid PlayerID)
        {
            string msg = GetDetail(PlayerID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại PlayerID : {PlayerID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    PlayerID
                };
                connection.Execute("usp_Player_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(PlayerInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    PlayerID = Guid.NewGuid(),
                    insert.PlayerName,
                    insert.BirthDay,
                    insert.CountryID,
                    insert.Description,
                    CreateDate = DateTime.Now,
                    ModifeDate = DateTime.Now,
                    insert.Achievements
                };
                connection.Execute("usp_Player_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(PlayerUpdateVM update)
        {
            string msg = GetDetail(update.PlayerID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại PlayerID : {update.PlayerID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.PlayerID,
                    update.PlayerName,
                    update.BirthDay,
                    update.Achievements,
                    update.CountryID,
                    update.Description,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Player_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
