using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WALK_IN_PORTAL_API.DataAccess;

namespace WALK_IN_PORTAL_API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RegisterDataController : ControllerBase
    {
        readonly IDataAccess dataAccess;
        public RegisterDataController(IDataAccess dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
        }
        [HttpGet]
        public IActionResult GetRegistrationData()
        {
            var result = dataAccess.GetRegistrationData();

            return Ok(result);
        }
    }

}
