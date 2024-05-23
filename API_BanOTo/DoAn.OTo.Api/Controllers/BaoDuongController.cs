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
    public class BaoDuongController : BaseController<BaoDuong>
    {
        IBaoDuongRepository _baoDuongRepository;
        IBaoDuongService _baoDuongService;
        public BaoDuongController(IBaoDuongRepository baoDuongRepository, IBaoDuongService baoDuongService):base(baoDuongRepository ,baoDuongService)
        {
            _baoDuongRepository = baoDuongRepository;
            _baoDuongService = baoDuongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageBaoDuong(int page, int pageSize)
        {
            try
            {
                var res = await _baoDuongRepository.GetPage(page, pageSize);
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
            var data = await _baoDuongRepository.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("BaoDuong");
                worksheet.Cells["A1"].LoadFromCollection<BaoDuong>((IEnumerable<BaoDuong>)data, true);

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
                string excelName = $"BaoDuongExport-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }
}
