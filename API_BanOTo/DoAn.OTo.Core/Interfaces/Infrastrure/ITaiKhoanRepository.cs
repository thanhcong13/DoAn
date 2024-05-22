using DoAn.OTo.Core.DTO;
using DoAn.OTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.OTo.Core.Interfaces.Infrastrure
{
    public interface ITaiKhoanRepository : IBaseRepository<TaiKhoan>
    {
        int Delete(string TenTaiKhoan);
        int Update (TaiKhoan taikhoan , string TenTaiKhoan);
        Task<FormatedResponse> GetPage(int page, int pageSize);
        Task<FormatedResponse> GetAllEmployee();
        Task<FormatedResponse> Login(string tenTaiKhoan, string matKhau);

    }
}
