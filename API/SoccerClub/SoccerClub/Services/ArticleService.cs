using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IArticleService
    {
        public string GetList(ArticleSearchVM search, out object result);
        public string Insert(ArticleInsertVM insert);
        public string Update(ArticleUpdateVM update);
        public string Delete(Guid ArticleID);
        public string GetDetail(Guid ArticleID, out object result);
    }
    public class ArticleService : IArticleService
    {
        public string GetList(ArticleSearchVM search, out dynamic result)
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
                var Article = connection.Query<GetArticleVM>("usp_Article_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Article.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Article.Count();

            };
            return string.Empty;
        }
        public string GetDetail(Guid ArticleID, out dynamic result)
        {

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                string msg = string.Empty;
                var parameter = new
                {
                    ArticleID
                };
                var Article = connection.Query<GetArticleVM>("usp_Article_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Article;
                if (result.data.Count == 0) return msg = $"Không tồn tại ArticleID : {ArticleID}!";
            };
            return string.Empty;
        }

        public string Delete(Guid ArticleID)
        {
            string msg = GetDetail(ArticleID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại ArticleID : {ArticleID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    ArticleID
                };
                connection.Execute("usp_Article_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(ArticleInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    ArticleID = Guid.NewGuid(),
                    insert.Title,
                    insert.Content,
                    insert.Image,
                    insert.Description,
                    CreateDate = DateTime.Now,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Article_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(ArticleUpdateVM update)
        {
            string msg = GetDetail(update.ArticleID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại ArticleID : {update.ArticleID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.ArticleID,
                    update.Title,
                    update.Content,
                    update.Image,
                    update.Description,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Article_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
