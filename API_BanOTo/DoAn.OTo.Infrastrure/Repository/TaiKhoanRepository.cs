using Dapper;
using DoAn.OTo.Core.DTO;
using DoAn.OTo.Core.Entities;
using DoAn.OTo.Core.Interfaces.Infrastrure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.OTo.Infrastrure.Repository
{
    public class TaiKhoanRepository : BaseRepository<TaiKhoan>, ITaiKhoanRepository
    {
        public int Delete(string TenTaiKhoan)
        {
            using (SqlConnection = new MySqlConnector.MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"DELETE FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TenTaiKhoan", TenTaiKhoan);
                var res = SqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        public async Task<FormatedResponse> GetPage(int page, int pageSize)
        {
            using (SqlConnection = new MySqlConnector.MySqlConnection(ConnectionString))
            {
                var offset = (page - 1) * pageSize;
                var query = $"SELECT * FROM TaiKhoan LIMIT @PageSize OFFSET @Offset";
                var response =  await SqlConnection.QueryAsync<TaiKhoan>(query, new { PageSize = pageSize, Offset = offset });
                var query2 = $"SELECT COUNT(*)  FROM TaiKhoan;";
                var response2 = await SqlConnection.ExecuteScalarAsync<int>(query2);
                return new FormatedResponse() { InnerBody = response, Count = response2 };
            }
        }

        public int Update(TaiKhoan taikhoan, string TenTaiKhoan)
        {
            using (SqlConnection = new MySqlConnector.MySqlConnection(ConnectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu

                // Chuỗi truy vấn SQL
                var sqlCommand = "UPDATE TaiKhoan SET TenTaiKhoan = @TenTaiKhoan , MatKhau = @MatKhau ,MaNV =@MaNV , GID =@GID  WHERE TenTaiKhoan = @TenTaiKhoan";

                // Tham số động
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TenTaiKhoan", taikhoan.TenTaiKhoan);
                parameters.Add("@MatKhau", taikhoan.MatKhau);
                parameters.Add("@MaNV", taikhoan.MaNV);
                parameters.Add("@GID", taikhoan.GID);
                

                // Thực thi truy vấn và trả về số lượng bản ghi đã được cập nhật
                var result = SqlConnection.Execute(sqlCommand, parameters);

                // Trả về kết quả cho client
                return result;
            }
        }

        public async Task<FormatedResponse> GetAllEmployee()
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    var query = $"SELECT MaNV , TenNV FROM nhanvien";
                    var response = await SqlConnection.QueryAsync(query);
                    return new FormatedResponse() { InnerBody = response };
                }
            }
            catch (Exception ex)
            {

                return new FormatedResponse() { Exception = ex, StatusCode = EnumStatusCode.StatusCode500};
            }
        }

        public async Task<FormatedResponse> Login(string tenTaiKhoan, string matKhau)
        {
            try
            {
                using (var sqlConnection = new MySqlConnector.MySqlConnection(ConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    var sqlCommand = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan AND MatKhau = @MatKhau";
                    var parameters = new DynamicParameters();
                    parameters.Add("@TenTaiKhoan", tenTaiKhoan);
                    parameters.Add("@MatKhau", matKhau);

                    var result = await sqlConnection.QueryFirstOrDefaultAsync(sqlCommand, parameters);

                    if (result != null)
                    {
                        return new FormatedResponse()
                        {
                            InnerBody = result,
                            StatusCode = EnumStatusCode.StatusCode200
                        };
                    }
                    else
                    {
                        return new FormatedResponse()
                        {
                            InnerBody = "Invalid username or password.",
                            StatusCode = EnumStatusCode.StatusCode401
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new FormatedResponse()
                {
                    Exception = ex,
                    StatusCode = EnumStatusCode.StatusCode500
                };
            }
        }
    }
}
