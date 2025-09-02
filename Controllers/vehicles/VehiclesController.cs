using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using tech_tracker_server.Controllers.vehicles.DTO;
using tech_tracker_server.Data;
using tech_tracker_server.Models;

namespace tech_tracker_server.Controllers.vehicles
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Getting a list of vehicles", Description = "")]
        public ActionResult<List<Vehicle>> Get()
        {
            var vehicles = _context.Vehicles.ToList();
            return Ok(vehicles);
        }

        
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Getting a vehicle by ID", Description = "Returns the vehicle by their unique ID")]
        [SwaggerResponse(200, "The vehicle has been found", typeof(Vehicle))]
        [SwaggerResponse(404, "The vehicle was not found")]
        public ActionResult<Vehicle> Get(string id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with id {id} not found");
            }

            return Ok(vehicle);
        }

        [HttpPost]
        [
            SwaggerOperation(Summary = "Create a new vehicle",
            Description = "Accepts vehicle data and creates it in the system.")
        ]
        [SwaggerResponse(200, "The vehicle was successfully created", typeof(User))]
        [SwaggerResponse(400, "Incorrect data")]
        public ActionResult<Vehicle> Post([FromBody] CreateVehicleDto vehicleDto)
        {
            var currentCompany = _context.Companyes.Find(vehicleDto.CompanyId);

            if (currentCompany == null)
            {
                return BadRequest($"The company with ID: {vehicleDto.CompanyId} was not found");
            }

            if (vehicleDto == null)
            {
                return BadRequest("Incorrect data was transmitted");
            }

            var newVehicle = new Vehicle();

            newVehicle.Name = vehicleDto.Name;
            newVehicle.CompanyId = "";

            currentCompany.Vehicles.Add(newVehicle);    

            //_context.Vehicles.Add(newVehicle);
            _context.SaveChanges();

            return Ok(newVehicle);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updating the vehicle by ID", Description = "Updates the data of an existing vehicle")]
        [SwaggerResponse(200, "The vehicle has been successfully updated", typeof(Vehicle))]
        [SwaggerResponse(404, "The vehicle was not found")]
        public ActionResult<Vehicle> Put(string id, [FromBody] string name)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with id {id} not found");
            }

            vehicle.Name = name;
            _context.SaveChanges();

            return Ok(vehicle);
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleting the vehicle by ID", Description = "Deletes a vehicle by their unique ID")]
        [SwaggerResponse(200, "The vehicle has been successfully deleted")]
        [SwaggerResponse(404, "The vehicle was not found")]
        public IActionResult Delete(string id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound($"The vehicle with id: ${id} not found");
            }

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();

            return Ok("The vehicle has been successfully deleted");
        }
    }
}
