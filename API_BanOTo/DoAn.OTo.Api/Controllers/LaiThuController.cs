using DoAn.OTo.Core.Entities;
using DoAn.OTo.Core.Interfaces.Infrastrure;
using DoAn.OTo.Core.Interfaces.Service;
using DoAn.OTo.Infrastrure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace DoAn.OTo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LaiThuController : BaseController<LaiThu>
    {
        ILaiThuRepository _laiThuRepository;
        ILaiThuService _laiThuService;
        public LaiThuController(ILaiThuRepository laiThuRepository, ILaiThuService laiThuService):base(laiThuRepository, laiThuService)
        {
            _laiThuRepository = laiThuRepository;
            _laiThuService = laiThuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByLaiThu(int page, int pageSize)
        {
            try
            {
                var res = await _laiThuRepository.GetPage(page, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel()
        {
            var data = await _laiThuRepository.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LaiThu");
                worksheet.Cells["A1"].LoadFromCollection<LaiThu>((IEnumerable<LaiThu>)data, true);

                // Định dạng tiêu đề
                using (var range = worksheet.Cells["A1:Z1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                string excelName = $"LaiThuExport-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }

}
