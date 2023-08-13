using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface ICategoryService
    {
        public string GetList(CategorySearchVM search, out object result);
        public string Insert(CategoryInsertVM insert);
        public string Update(CategoryUpdateVM update);
        public string Delete(Guid CategoryID);
        public string GetDetail(Guid CategoryID, out object result);
    }
    public class CategoryService : ICategoryService
    {
        public string GetList(CategorySearchVM search, out dynamic result)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    search.TextSearch
                };
                var Category = connection.Query<GetCategoryVM>("usp_Category_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Category.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Category.Count();

            };
            return string.Empty;
        }
        public string GetDetail(Guid CategoryID, out dynamic result)
        {
            string msg = string.Empty;
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    CategoryID
                };
                var Category = connection.Query<GetCategoryVM>("usp_Category_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Category;

                if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {CategoryID}!";
            };
            return string.Empty;
        }

        public string Delete(Guid CategoryID)
        {
            string msg = GetDetail(CategoryID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {CategoryID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    CategoryID
                };
                connection.Execute("usp_Category_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(CategoryInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    CategoryID = Guid.NewGuid(),
                    insert.CategoryName,
                    CreateDate = DateTime.Now,
                    insert.ParentID,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Category_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(CategoryUpdateVM update)
        {
            string msg = GetDetail(update.CategoryID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại CategoryID : {update.CategoryID}!";

            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.CategoryID,
                    update.CategoryName,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Category_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
