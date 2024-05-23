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
    public class LienHeController : BaseController<LienHe>
    {
        ILienHeRepository _lienHeRepository;
        ILienHeService _lienHeService;
        public LienHeController(ILienHeRepository lienHeRepository, ILienHeService lienHeService):base(lienHeRepository, lienHeService) 
        {
            _lienHeRepository = lienHeRepository;
            _lienHeService = lienHeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByLienHe(int page, int pageSize)
        {
            try
            {
                var res = await _lienHeRepository.GetPage(page, pageSize);
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
            var data = await _lienHeRepository.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LienHe");
                worksheet.Cells["A1"].LoadFromCollection<LienHe>((IEnumerable<LienHe>)data, true);

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
                string excelName = $"LienHeExport-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }


    }
}
