using Microsoft.AspNetCore.Mvc;
using CompanyApi.Models;
using CompanyApi.Services;
using CompanyApi.Interfaces;
namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        // 🔹 GET: api/Company (Get All)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return Ok(await _service.GetAllCompaniesAsync());
        }

        // 🔹 GET: api/Company/{id} (Get by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(int id)
        {
            var company = await _service.GetCompanyByIdAsync(id);
            return company != null ? Ok(company) : NotFound(new { message = "Company not found" });
        }

        // 🔹 GET: api/Company/isin/{isin} (Get by ISIN)
        [HttpGet("isin/{isin}")]
        public async Task<ActionResult<Company>> GetCompanyByIsin(string isin)
        {
            var company = await _service.GetCompanyByIsinAsync(isin);
            return company != null ? Ok(company) : NotFound(new { message = "Company with this ISIN not found" });
        }

        // 🔹 POST: api/Company (Create)
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            try
            {
                var createdCompany = await _service.CreateCompanyAsync(company);
                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 PUT: api/Company/{id} (Update)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            try
            {
                await _service.UpdateCompanyAsync(id, company);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // 🔹 DELETE: api/Company/{id} (Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _service.DeleteCompanyAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
