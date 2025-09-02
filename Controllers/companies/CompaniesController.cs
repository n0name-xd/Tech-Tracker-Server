using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using tech_tracker_server.Controllers.companies.DTO;
using tech_tracker_server.Controllers.users.DTO;
using tech_tracker_server.Data;
using tech_tracker_server.Models;

namespace tech_tracker_server.Controllers.companies
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Getting a list of companies", Description = "")]
        public ActionResult<List<Company>> Get()
        {
            var companyes = _context.Companyes
                .Include(c => c.Owner)
                .Include(c => c.Vehicles)
                .ToList();

            return Ok(companyes);
        }

   
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Getting the company by ID", Description = "Returns the company by their unique ID")]
        [SwaggerResponse(200, "The company has been found", typeof(User))]
        [SwaggerResponse(404, "The company was not found")]
        public ActionResult<Company> Get(string id)
        {
            var company = _context.Companyes
                    .Include(companies => companies.Owner)
                    .Include(c => c.Vehicles)
                    .FirstOrDefault(c => c.Id == id);

            if (company == null) 
            {
                return NotFound($"The company with id {id} not found");
            }

            return Ok(company);
        }

     
        [HttpPost]
        [
            SwaggerOperation(Summary = "Create a new company",
            Description = "Accepts company data and creates it in the system.")
        ]
        [SwaggerResponse(200, "The company was successfully created", typeof(Company))]
        [SwaggerResponse(400, "Incorrect data")]
        public ActionResult<Company> Post([FromBody] CreateCompanyDto compatyDto)
        {
            if (compatyDto == null)
            {
                return BadRequest("Incorrect data was transmitted");
            }

            var owner = _context.Users.Find(compatyDto.OwnerId);

            if (owner == null)
            {
                return BadRequest("The owner was not found");
            }

            var newCompany = new Company(compatyDto.Name, owner);

            _context.Companyes.Add(newCompany);
            _context.SaveChanges();

            return Ok(newCompany);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updating the company by ID", Description = "Updates the data of an existing company")]
        [SwaggerResponse(200, "The company has been successfully updated", typeof(User))]
        [SwaggerResponse(404, "The company was not found")]
        public ActionResult<Company> Put(string id, [FromBody] UpdateCompanyDto companyDto)
        {
            var company = _context.Companyes.Find(id);

            if (company == null)
            {
                return NotFound($"The company with id {id} not found");
            }

            company.Name = companyDto.Name ?? company.Name;

            _context.SaveChanges();

            return Ok(company);
        }

    
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleting the company by ID", Description = "Deletes a company by their unique ID")]
        [SwaggerResponse(200, "The company has been successfully deleted")]
        [SwaggerResponse(404, "The company was not found")]
        public IActionResult Delete(string id)
        {
            var company = _context.Companyes.Find(id);

            if (company == null)
            {
                return NotFound($"The company with id: ${id} not found");
            }

            _context.Companyes.Remove(company);
            _context.SaveChanges();

            return Ok("The company has been successfully deleted");
        }
    }
}
