using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IMatchService
    {
        public string GetList(MatchSearchVM search, out object result);
        public string Insert(MatchInsertVM insert);
        public string Update(MatchUpdateVM update);
        public string Delete(Guid MatchID);
        public string GetDetail(Guid MatchID, out object result);
    }
    public class MatchService : IMatchService
    {
        public string GetList(MatchSearchVM search, out dynamic result)
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
                var Match = connection.Query<GetMatchVM>("usp_Match_GetList", parameter, commandType: CommandType.StoredProcedure);



                result.data = Match.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Match.Count();

            };
            return string.Empty;
        }
        public string GetDetail(Guid MatchID, out dynamic result)
        {

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    MatchID
                };
                var Match = connection.Query<GetMatchVM>("usp_Match_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Match;

            };
            return string.Empty;
        }

        public string Delete(Guid MatchID)
        {
            string msg = GetDetail(MatchID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {MatchID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    MatchID
                };
                connection.Execute("usp_Match_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(MatchInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    MatchID = Guid.NewGuid(),
                    insert.MatchTime,
                    insert.CompetitorA,
                    insert.CompetitorB,
                    insert.CompetitionID,
                    insert.Status,
                    insert.RefereeID,
                    insert.Title,
                    insert.ScoreCompetitorA,
                    insert.ScoreCompetitorB,
                    insert.Description,
                    CreateDate = DateTime.Now
                };
                connection.Execute("usp_Match_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(MatchUpdateVM update)
        {
            string msg = GetDetail(update.MatchID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {update.MatchID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.MatchID,
                    update.MatchTime,
                    update.CompetitorA,
                    update.CompetitorB,
                    update.CompetitionID,
                    update.Status,
                    update.RefereeID,
                    update.Title,
                    update.ScoreCompetitorA,
                    update.ScoreCompetitorB,
                    update.Description,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Match_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
