using Dapper;
using Microsoft.Data.SqlClient;
using SoccerClub.ViewModel;
using System.Data;
using System.Dynamic;

namespace SoccerClub.Services
{
    public interface IOrderService
    {
        public string GetList(OrderSearchVM search, out object result);
        public string Insert(OrderInsertVM insert);
        public string Update(OrderUpdateVM update);
        public string Delete(int OrderID);
        public string GetDetail(int OrderID, out object result);
    }
    public class OrderService : IOrderService
    {
        public string GetList(OrderSearchVM search, out dynamic result)
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
                var Order = connection.Query<GetOrderVM>("usp_Order_GetList", parameter, commandType: CommandType.StoredProcedure);
                result.data = Order.Skip((search.CurrentPage - 1) * search.PageSize).Take(search.PageSize).ToList();
                result.total = Order.Count();

            };
            return string.Empty;
        }
        public string GetDetail(int OrderID, out dynamic result)
        {
            string msg = string.Empty;
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                result = new ExpandoObject();
                connection.Open();
                var parameter = new
                {
                    OrderID
                };
                var Order = connection.Query<GetOrderVM>("usp_Order_GetDetail", parameter, commandType: CommandType.StoredProcedure);
                result.data = Order;

                if (result.data.Count == 0) return msg = $"Không tồn tại OrderID : {OrderID}!";
            };
            return string.Empty;
        }

        public string Delete(int OrderID)
        {
            string msg = GetDetail(OrderID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại OrderID : {OrderID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    OrderID
                };
                connection.Execute("usp_Order_Delete", parameter, commandType: CommandType.StoredProcedure);
            };
            return string.Empty;
        }

        public string Insert(OrderInsertVM insert)
        {
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    insert.UserID,
                    insert.ProductID,
                    insert.Quantity,
                    CreateDate = DateTime.Now
                };
                connection.Execute("usp_Order_Insert", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
        public string Update(OrderUpdateVM update)
        {
            string msg = GetDetail(update.OrderID, out dynamic result);
            if (msg.Length > 0) return msg;

            if (result.data.Count == 0) return msg = $"Không tồn tại OrderID : {update.OrderID}!";
            using (IDbConnection connection = new SqlConnection(Constant.ConnectSQL))
            {
                connection.Open();
                var parameter = new
                {
                    update.OrderID,
                    update.ProductID,
                    update.Quantity,
                    ModifeDate = DateTime.Now
                };
                connection.Execute("usp_Order_Update", parameter, commandType: CommandType.StoredProcedure);
            }
            return string.Empty;
        }
    }
}
