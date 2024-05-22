using DoAn.OTo.Core.Entities;
using DoAn.OTo.Core.Exceptions;
using DoAn.OTo.Core.Interfaces.Infrastrure;
using DoAn.OTo.Core.Interfaces.Service;
using DoAn.OTo.Core.Services;
using DoAn.OTo.Infrastrure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DoAn.OTo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SanPhamController : BaseController<SanPham>
    {
        ISanPhamRepository _sanPhamRepository;
        ISanPhamService _sanPhamService;
        private readonly string _uploadFolder = "uploads";
        public SanPhamController(ISanPhamRepository sanPhamRepository, ISanPhamService sanPhamService) :base(sanPhamRepository, sanPhamService)
        {
            _sanPhamRepository = sanPhamRepository;
            _sanPhamService = sanPhamService;
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }
        [HttpGet()]
        public IActionResult QueryList(int page, int pageSize)
        {
            try
            {
                var res = _sanPhamRepository.QueryList(page, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{MaHang}")]
        public IActionResult GetByHang(Guid MaHang)
        {
            try
            {
                var res = _sanPhamRepository.GetByHang(MaHang);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{MaCH}")]
        public IActionResult GetByMaCH(Guid MaCH)
        {
            try
            {
                var res = _sanPhamRepository.GetByMaCH(MaCH);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPageProduct(int page , int pageSize )
        {
            try
            {
                var res = await _sanPhamRepository.GetPage(page, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCH()
        {
            var response = await _sanPhamRepository.GetAllCH();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHang()
        {
            var response = await _sanPhamRepository.GetAllHang();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _sanPhamRepository.GetAllProducts();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _sanPhamRepository.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về URL của file sau khi upload
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var fileUrl = baseUrl + "/" + _uploadFolder + "/" + fileName;

            return Ok(new { url = fileUrl });
        }
    }
}
