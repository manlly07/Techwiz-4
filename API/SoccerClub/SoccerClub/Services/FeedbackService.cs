using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IFeedbackService
    {
        public string GetList(FeedbackSearchVM search, out object result);
        public string Insert(FeedbackInsertVM insert);
        //public string Update(FeedbackUpdateVM update);
        public string Delete(int FeedbackID);
        public string GetDetail(int FeedbackID, out object result);
    }
    public class FeedbackService : IFeedbackService
    {
        public string GetList(FeedbackSearchVM search, out dynamic result)
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
                var Feedback = connection.Query<GetFeedbackVM>("usp_Feedback_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Feedback.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Feedback.Count();

            };
            return string.Empty;
        }
        public string GetDetail(int FeedbackID, out dynamic result)
        {
            string msg = string.Empty;
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    FeedbackID
                };
                var Feedback = connection.Query<GetFeedbackVM>("usp_Feedback_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Feedback;

                if (result.data.Count == 0) return msg = $"Không tồn tại FeedbackID : {FeedbackID}!";
            };
            return string.Empty;
        }

        public string Delete(int FeedbackID)
        {
            string msg = GetDetail(FeedbackID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result == null) return $"Không tồn tại FeedbackID : {FeedbackID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    FeedbackID
                };
                connection.Execute("usp_Feedback_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(FeedbackInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    insert.Title,
                    insert.Content,
                    insert.Vote,
                    insert.UserID,
                    CreateDate = DateTime.Now,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Feedback_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        //public string Update(FeedbackUpdateVM update)
        //{
        //    string msg = GetDetail(update.FeedbackID, out dynamic result);
        //    if (msg.Length > 0) return msg;

        //    if (result.data.Count == 0) return msg = $"Không tồn tại FeedbackID : {update.FeedbackID}!";
        //    using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
        //    {
        //        connection.Open();
        //        var parameter = new
        //        {
        //            update.FeedbackID,
        //            update.FeedbackName,

        //            ModifeDate = DateTime.Now
        //        };
        //        connection.Execute("usp_Feedback_Update", parameter, commandType: CommandType.StoredProcedure);
        //    }
        //    return string.Empty;
        //}
    }
}
