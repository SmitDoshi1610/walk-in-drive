using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using WALK_IN_PORTAL_API.DataAccess;
using WALK_IN_PORTAL_API.Models;

namespace WALK_IN_PORTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IDataAccess dataAccess;

        public UserController(IDataAccess dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
        }

        [HttpPost("RegisterUser")]
        public ResponseMessage RegisterUser([FromBody] User user)
        {
            var result = dataAccess.RegisterUser(user);
            return result;
        }

        [HttpPost("InsertEducationData")]
        public ResponseMessage InsertEducationData([FromBody] EducationQualification user)
        {
            var result = dataAccess.InsertEducationData(user);
            return result;
        }

        [HttpPost("InsertProfessionalData")]
        public ResponseMessage InsertProfessionalData([FromBody] ProfessionalQualification user)
        {
            var result = dataAccess.InsertProfessionalData(user);
            return result;
        }

        [HttpPost("Login")]
        public ResponseMessage LoginUser([FromBody] Login login)
        {
            var token = dataAccess.LoginUser(login);
            return token; 
        }
    }
}
