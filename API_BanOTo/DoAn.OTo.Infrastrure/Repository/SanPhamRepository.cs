using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using DoAn.OTo.Core.Interfaces.Infrastrure;
using DoAn.OTo.Core.Entities;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DoAn.OTo.Core.DTO;
using Ubiety.Dns.Core;


namespace DoAn.OTo.Infrastrure.Repository
{
    public class SanPhamRepository : BaseRepository<SanPham>, ISanPhamRepository
    {
        public IEnumerable<SanPham> GetByHang(Guid MaHang)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM SanPham WHERE MaHang= @MaHang";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MaHang", MaHang);
                var product = SqlConnection.Query<SanPham>(sqlCommand, param: parameters);
                return product;
            }
        }

        public IEnumerable<SanPham> GetByMaCH(Guid MaCH)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM SanPham WHERE MaCH= @MaCH";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MaCH", MaCH);
                var product = SqlConnection.Query<SanPham>(sqlCommand, param: parameters);
                return product;
            }
        }

        /* public override int Delete(Guid MaSP)
         {
             using (SqlConnection = new MySqlConnection(ConnectionString))
             {
                 var sqlCommand = $"DELETE FROM SanPham WHERE MaSP = @productID";
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@productID", MaSP);
                 var res = SqlConnection.Execute(sqlCommand, param: parameters);
                 return res;
             }
         }*/

        public async Task<FormatedResponse> QueryList(int page, int pageSize)
        {
            using(SqlConnection= new MySqlConnection(ConnectionString))
            {
                var offset = (page - 1) * pageSize;
                var query = $"SELECT * FROM SanPham LIMIT @PageSize OFFSET @Offset";
                var response = await SqlConnection.QueryAsync<SanPham>(query, new { PageSize = pageSize, Offset = offset });
                return new FormatedResponse() { InnerBody = response, Count = response.Count() };
            }
            
        }

        public async Task<FormatedResponse> GetPage(int page, int pageSize)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var offset = (page - 1) * pageSize;
                var query = $"SELECT * FROM SanPham LIMIT @PageSize OFFSET @Offset";
                var response = await SqlConnection.QueryAsync<SanPham>(query, new { PageSize = pageSize, Offset = offset });
                var query2 = $"SELECT COUNT(*)  FROM SanPham;";
                var response2 = await SqlConnection.ExecuteScalarAsync<int>(query2);
                return new FormatedResponse() { InnerBody = response, Count = response2 };
            }
        }

        public async Task<FormatedResponse> GetAllCH()
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    var query = $"SELECT TenCH FROM cuahang";
                    var response = await SqlConnection.QueryAsync(query);
                    return new FormatedResponse() { InnerBody = response };
                }
            }
            catch (Exception ex)
            {

                return new FormatedResponse() { Exception = ex, StatusCode = EnumStatusCode.StatusCode500};
            }
        }

        public async Task<FormatedResponse> GetAllHang()
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    var query = $"SELECT TenHang FROM hangxe";
                    var response = await SqlConnection.QueryAsync(query);
                    return new FormatedResponse() { InnerBody = response };
                }
            }
            catch (Exception ex)
            {

                return new FormatedResponse() { Exception = ex, StatusCode = EnumStatusCode.StatusCode500 };
            }
        }

        public async Task<FormatedResponse> GetAllProducts()
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    var query = $"SELECT * FROM sanpham";
                    var response = await SqlConnection.QueryAsync(query);
                    return new FormatedResponse() { InnerBody = response };
                }
            }
            catch (Exception ex)
            {

                return new FormatedResponse() { Exception = ex, StatusCode = EnumStatusCode.StatusCode500};
            }
        }
        public async Task<FormatedResponse> GetById(Guid Id)
        {
            try
            {
                var sqlCommand = "SELECT * FROM sanpham WHERE MaSP = @MaSP";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();  // Mở kết nối

                    var parameters = new DynamicParameters();
                    parameters.Add("@MaSP", Id);

                    var res = await connection.QueryAsync(sqlCommand, parameters);

                    return new FormatedResponse() { InnerBody = res };
                }
            }
            catch (Exception ex)
            {

                return new FormatedResponse() { Exception = ex, StatusCode = EnumStatusCode.StatusCode500};
            }
        }
    }
}
