using Microsoft.AspNetCore.Mvc;
using WALK_IN_PORTAL_API.Models;

namespace WALK_IN_PORTAL_API.DataAccess
{
    public interface IDataAccess
    {
        RegistrationData GetRegistrationData();

        ResponseMessage RegisterUser(User user);

        ResponseMessage InsertEducationData(EducationQualification user);
        ResponseMessage InsertProfessionalData(ProfessionalQualification user);

        ResponseMessage LoginUser(Login user);

        List<WalkInDrive> GetWalkInDrives();

        WalkInDrive GetWalkInDrive(int id);

        ResponseMessage InsertUserAppliedJob(AppliedJobRole jobRole);
    }
}
