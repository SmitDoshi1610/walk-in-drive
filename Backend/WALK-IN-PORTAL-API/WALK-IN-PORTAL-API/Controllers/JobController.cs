using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WALK_IN_PORTAL_API.DataAccess;
using WALK_IN_PORTAL_API.Models;

namespace WALK_IN_PORTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        readonly IDataAccess dataAccess;

        public JobController(IDataAccess dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("WalkInDrives")]
        public List<WalkInDrive> GetWalkInDrives()
        {
            var result = dataAccess.GetWalkInDrives();
            return result;
        }

        [HttpGet("WalkInDrive/{id}")]
        public WalkInDrive GetWalkInDrive(int id)
        {
            var result = dataAccess.GetWalkInDrive(id);
            return result;
        }

        [HttpPost("InsertUserAppliedJob")]
        public ResponseMessage InsertAppliedJobRole([FromBody] AppliedJobRole jobRole)
        {
            var result = dataAccess.InsertUserAppliedJob(jobRole);
            return result;
        }
    }
}
    