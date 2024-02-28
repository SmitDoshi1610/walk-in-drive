using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WALK_IN_PORTAL_API.Models;

namespace WALK_IN_PORTAL_API.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;  
        public DataAccess(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration["ConnectionStrings:DB"];
        }

        MySql.Data.MySqlClient.MySqlConnection connection;
        MySql.Data.MySqlClient.MySqlConnection connection2;


        public RegistrationData GetRegistrationData()
        {
            var registrationData = new RegistrationData();

            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                string query = "SELECT * FROM registration_data";

                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    {
                        registrationData.preferedJobRoles.Add((string)reader["preferred_job_roles"]);
                        registrationData.yearOfPassing.Add((int)reader["year_of_passing"]);
                        registrationData.qualification.Add((string)reader["qualification"]);
                        registrationData.stream.Add((string)reader["stream"]);
                        registrationData.college.Add((string)reader["college"]);
                        registrationData.tecnologiesExpert.Add((string)reader["technologies_expert"]);
                        registrationData.tecnologiesFamiliar.Add((string)reader["technologies_familiar"]);
                        registrationData.noticePeriod.Add((int)reader["notice_period"]);

                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            connection.Close();
            return registrationData;

        }

        
        public ResponseMessage RegisterUser(User user)
        {
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                //CHECKING USER PRESENT OR NOT 
                string query = "SELECT COUNT(*) FROM USER WHERE email='" + user.email + "';";
                cmd.CommandText = query;

                var count = (long)cmd.ExecuteScalar();

                if (count > 0)
                {
                    connection.Close();
                    return new ResponseMessage { message = "Email Id Already Exits", statusCode = 409, moveNext = false };
                }

                //INSERTING INTO THE USER TABLE IF NOT PRESENT
                query = "INSERT INTO USER (firstname, lastname, email, password, mobile, profile_picture, resume, portfolio_url, reffered_by, job_updates) VALUES (@firstname, @lastname, @email, @password, @mobile, @profile_picture, @resume, @portfolio_url, @reffered_by, @job_updates);";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@firstname", user.firstName);
                cmd.Parameters.AddWithValue("@lastname", user.lastName);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@mobile", user.phone);
                cmd.Parameters.AddWithValue("@profile_picture", user.profilePicture);
                cmd.Parameters.AddWithValue("@resume", user.resume);
                cmd.Parameters.AddWithValue("@portfolio_url", user.portfolioUrl);
                cmd.Parameters.AddWithValue("@reffered_by", user.referredBy);
                cmd.Parameters.AddWithValue("@job_updates", user.emailNotification);

                int rowAffected = cmd.ExecuteNonQuery();
   
                if (rowAffected > 0)
                {
                    query = "SELECT MAX(user_id) as Id FROM USER";
                    cmd.CommandText = query;
                    var uId = 0;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        uId = (int)reader["Id"];
                    }
                    reader.Close();

                    if (uId > 0)
                    {
                        foreach (var ID in user.preferredJobRoles)
                        {
                            if (ID != null)
                            {
                                cmd.Parameters.Clear();
                                query = "INSERT INTO user_has_preferedjobrole VALUES(@uId, @Id);";
                                cmd.Parameters.AddWithValue("@uid", uId);
                                cmd.Parameters.AddWithValue("@Id", ID);
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    connection.Close();
                    return new ResponseMessage { message="Registered Succesfully", statusCode=200, moveNext=true};
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return new ResponseMessage { message = "Internal Error Occured", moveNext = false };
            }
            connection.Close();
            return new ResponseMessage { message = "Something Went Wrong", statusCode = 400, moveNext = false };
        }

        public ResponseMessage InsertEducationData(EducationQualification user)
        {
            try
            {

                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                string query = "SELECT MAX(user_id) as Id FROM USER";
                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();
                var userId = 0;
                while (reader.Read()) { userId = (int)reader["Id"]; }
                reader.Close();

                

               query = "INSERT INTO user_education_qualification (percentage, passingYear, qualification, stream, college, other_college, collegeLocation,user_userID) " +
            "VALUES (@percentage, @passingYear, @qualification, @stream, @college, @other_college, @collegeLocation, @user_Id)";

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@percentage", user.percentage);
                cmd.Parameters.AddWithValue("@passingYear", user.passingYear);
                cmd.Parameters.AddWithValue("@qualification", user.qualification);
                cmd.Parameters.AddWithValue("@stream", user.stream);
                cmd.Parameters.AddWithValue("@college", user.college);
                cmd.Parameters.AddWithValue("@other_college", user.otherCollege);
                cmd.Parameters.AddWithValue("@collegeLocation", user.collegeLocation);
                cmd.Parameters.AddWithValue("@user_Id", userId);

                int rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    connection.Close();
                    return new ResponseMessage { statusCode = 200, message = "Succesfully Added", moveNext = true };
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return new ResponseMessage { message = "Something Went Wrong" , moveNext=false};
            }
            connection.Close();
            return new ResponseMessage { message="Something went wrong", moveNext=false};
        }

        public ResponseMessage InsertProfessionalData(ProfessionalQualification user)
        {
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                string query = "SELECT MAX(user_id) as Id FROM USER";
                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();
                var userId = 0;
                while (reader.Read()) { userId = (int)reader["Id"]; }
                reader.Close();

                if (user.applicationType == "Fresher")
                {
                   query = "INSERT INTO user_professional_qualification (applicationType, otherTechnology, " +
                    "haveAppliedTestBefore, roleAppliedFor, user_userID) " +
                    "VALUES (@applicationType, @otherTechnology, @haveAppliedTestBefore, @roleAppliedFor, @user_Id);";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@applicationType", user.applicationType);
                    cmd.Parameters.AddWithValue("@otherTechnology", user.fresherOtherFamiliar);
                    cmd.Parameters.AddWithValue("@haveAppliedTestBefore", user.haveAppliedTestBefore);
                    cmd.Parameters.AddWithValue("@roleAppliedFor", user.fresherRoleAppliedFor);
                    cmd.Parameters.AddWithValue("@user_Id", userId);
                    
                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        query = "SELECT MAX(qualificationId) as ID FROM user_professional_qualification;";
                        cmd.CommandText = query;

                        var qID = 0;

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            qID = (int)reader["ID"];
                        }
                        reader.Close();

                        foreach (var ID in user.fresherTechnologiesFamiliar)
                        {
                            if (ID != null)
                            {
                                cmd.Parameters.Clear();
                                query = "INSERT INTO user_professional_qualification_has_technologiesfamiliar VALUES (@qID, @tID);";
                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@qID", qID);
                                cmd.Parameters.AddWithValue("@tID", ID);
                                cmd.ExecuteNonQuery();
                            }
                           
                        }
                    }
                    connection.Close();
                    return new ResponseMessage { message = "Registered Succesfully", moveNext = true, statusCode = 200 };

                }
                else
                {
                   query = "INSERT INTO user_professional_qualification (applicationType, yearsExperience, " +
                 "currentCTC, expectedCTC, isInNoticePeriod, noticePeriodEnd, noticePeriodDuration, " +
                 "haveAppliedTestBefore, roleAppliedFor, user_userID) VALUES (@applicationType, @yearsExperience, " +
                 "@currentCTC, @expectedCTC, @isInNoticePeriod, @noticePeriodEnd, @noticePeriodDuration, " +
                 "@haveAppliedTestBefore, @roleAppliedFor, @user_Id);";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@applicationType", user.applicationType);
                    cmd.Parameters.AddWithValue("@yearsExperience", user.yearsOfExperience);
                    cmd.Parameters.AddWithValue("@currentCTC", user.currentCTC);
                    cmd.Parameters.AddWithValue("@expectedCTC", user.expectedCTC);
                    cmd.Parameters.AddWithValue("@isInNoticePeriod", user.isInNoticePeriod);
                    cmd.Parameters.AddWithValue("@noticePeriodEnd", user.noticePeriodEnd);
                    cmd.Parameters.AddWithValue("@noticePeriodDuration", user.noticePeriodDuration);
                    cmd.Parameters.AddWithValue("@haveAppliedTestBefore", user.haveAppliedTestBefore);
                    cmd.Parameters.AddWithValue("@roleAppliedFor", user.experienceRoleAppliedFor);
                    cmd.Parameters.AddWithValue("@user_Id", userId);
                    
                    int rowAffected =  cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        query = "SELECT MAX(qualificationId) as ID FROM user_professional_qualification;";
                        cmd.CommandText = query;

                        var qID = 0;

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            qID = (int)reader["ID"];
                        }
                        reader.Close();
                        if (qID > 0) 
                        {
                            foreach (var ID in user.experienceTechnologiesFamiliar)
                            {
                               if (ID != null)
                                {
                                    cmd.Parameters.Clear();
                                    query = "INSERT INTO user_professional_qualification_has_technologiesfamiliar VALUES (@qID, @tID);";
                                    cmd.CommandText = query;
                                    cmd.Parameters.AddWithValue("@qID", qID);
                                    cmd.Parameters.AddWithValue("@tID", ID);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            foreach (var ID in user.technologiesExpert)
                            {
                               if (ID != null)
                                {
                                    cmd.Parameters.Clear();
                                    query = "INSERT INTO user_professional_qualification_has_technologiesexpert VALUES (@qID, @tID);";
                                    cmd.CommandText = query;
                                    cmd.Parameters.AddWithValue("@qID", qID);
                                    cmd.Parameters.AddWithValue("@tID", ID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }   
                    }
                    connection.Close();
                    return new ResponseMessage { message = "Registered Successfully Done", moveNext = true, statusCode = 200 };
                    
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return new ResponseMessage { statusCode = 400 , message = e.Message , moveNext = false};
            }
        }

        public ResponseMessage LoginUser(Login login)
        {
            User user = new User();

            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                //CHECKING USER PRESENT OR NOT 
                string query = "SELECT COUNT(*) FROM USER WHERE email = @email AND password = @password;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@email", login.email);
                cmd.Parameters.AddWithValue("@password", login.password);
                cmd.CommandText = query;

                var count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    connection.Close();
                    return new ResponseMessage
                    {
                        message = "Invalid Username and Password",
                        moveNext = false,
                        statusCode = 404
                    };
                }
               
                query = "SELECT * FROM USER WHERE email = @email AND password = @password;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@email", login.email);
                cmd.Parameters.AddWithValue("@password", login.password);
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Id = (int)reader["user_id"];
                    user.firstName = (string)reader["firstname"];
                    user.lastName = (string)reader["lastname"];
                    user.phone = (string)reader["mobile"];
                }
                string key = "NljcYaV7iWWC2cBA9w1JUjHbkLSf5QEX";
                string duration = "60";
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("firstname", user.firstName),
                    new Claim("lastname", user.lastName),
                    new Claim("phone", user.phone)

                };

                var jwtToken = new JwtSecurityToken(

                    issuer: "localhost",
                    audience: "localhost",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Int32.Parse(duration)),
                    signingCredentials: credentials);

                connection.Close();
                return new ResponseMessage { moveNext = true, message = new JwtSecurityTokenHandler().WriteToken(jwtToken), statusCode=200 };
            }
            catch (MySqlException e)
            {
                connection.Close();
                return new ResponseMessage { statusCode = 400, message = e.Message, moveNext = false };
            }
        }

        public List<WalkInDrive> GetWalkInDrives()
        {
            List<WalkInDrive> drives = new List<WalkInDrive>();

            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                string query = "SELECT * FROM walk_in_drive;";
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    WalkInDrive drive = new WalkInDrive();
                    drive.Id = (int)reader["job_id"];
                    drive.name = (string)reader["name"];
                    drive.startDate = (DateTime)reader["start_date"];
                    drive.endDate = (DateTime)reader["end_date"];
                    drive.location = (string)reader["location"];
                    drive.timePreference = (string)reader["time_preference"];
                    drive.user_Id = (int)reader["user_UserID"];

                    connection2 = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    connection2.Open();
                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.Connection = connection2;

                    query = "SELECT role_name FROM jobroles WHERE job_JobID = @job_Id;";
                    cmd2.Parameters.AddWithValue("@job_Id", drive.Id);
                    cmd2.CommandText = query;
                    MySqlDataReader InsideReader = cmd2.ExecuteReader();
                    while (InsideReader.Read())
                    {
                        string name = (string)InsideReader["role_name"];
                        drive.jobPreference.Add(name);
                    }
                    drives.Add(drive);
                    InsideReader.Close();
                    connection2.Close();
                }
                reader.Close();
                connection.Close();
                return drives;
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return drives;
            }
            return drives;
        }

        public WalkInDrive GetWalkInDrive(int id)
        {
            WalkInDrive drive = new WalkInDrive();

            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                string query = "SELECT * FROM walk_in_drive WHERE job_id = @id;";
                cmd.Parameters.AddWithValue("@id", id); 
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    drive.Id = (int)reader["job_id"];
                    drive.name = (string)reader["name"];
                    drive.timePreference = (string)reader["time_preference"];
                    drive.location = (string)reader["location"];
                    drive.startDate = (DateTime)reader["start_date"];
                    drive.endDate = (DateTime)reader["end_date"];
                }

                reader.Close();
                cmd.Parameters.Clear();
                query = "SELECT * FROM jobroles WHERE job_JobID = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();

                List<JobRole> roles = new List<JobRole>();
                while (reader.Read())
                {
                    JobRole role = new JobRole();
                    role.Id = (int)reader["jobRole_id"];
                    role.roleName = (string)reader["role_name"];
                    role.roleDescription = (string)reader["role_description"];
                    role.roleRequirement = (string)reader["role_requirement"];
                    role.roleCompensation = (float)reader["role_compensation"];

                    roles.Add(role);
                }
                cmd.Parameters.Clear();
                foreach(JobRole role in roles)
                {
                    drive.walkInJob.Add(role);
                }
                return drive;

            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                return drive; 
            }
            throw new NotImplementedException();
        }

        public ResponseMessage InsertUserAppliedJob(AppliedJobRole jobRole)
        {
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;

                string query = "SELECT COUNT(*) FROM user_applied_walk_in_drive WHERE user_user_id = @user_id AND walk_in_drive_job_id = @job_id;";
                cmd.Parameters.AddWithValue("@user_id", jobRole.userId);
                cmd.Parameters.AddWithValue("@job_id", jobRole.driveId);
                cmd.CommandText = query;

                var count = (long)cmd.ExecuteScalar();

                if (count > 0)
                {
                    connection.Close();
                    return new ResponseMessage { message = "Already Applied to this Drive", moveNext = false, statusCode = 409 };
                }

                cmd.Parameters.Clear();
                query = "INSERT INTO user_applied_walk_in_drive (user_user_id, walk_in_drive_job_id, time_slot, resume) " +
                    "VALUES (@user_id, @drive_id, @time_slot, @resume);";
                cmd.Parameters.AddWithValue("@user_id", jobRole.userId);
                cmd.Parameters.AddWithValue("@drive_id", jobRole.driveId);
                cmd.Parameters.AddWithValue("@time_slot", jobRole.timeSlot);
                cmd.Parameters.AddWithValue("@resume", jobRole.resume);
                cmd.CommandText = query;

                int rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    foreach(var preference in jobRole.jobPreference)
                    {
                        if (preference != null)
                        {
                            cmd.Parameters.Clear();
                            query = "INSERT INTO user_applied_jobroles (user_user_id, jobroles_jobRole_id, drive_id) VALUES (@user_id, @role_id, @drive_id);";
                            cmd.Parameters.AddWithValue("@user_id", jobRole.userId);
                            cmd.Parameters.AddWithValue("@role_id", preference);
                            cmd.Parameters.AddWithValue("@drive_id", jobRole.driveId);
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
                return new ResponseMessage { message="Successfully Applied", moveNext=true, statusCode = 200 };
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return new ResponseMessage { message = e.Message, statusCode = 400, moveNext = false };
            }
            return new ResponseMessage { message = "Something Went Wrong", statusCode = 400, moveNext = false };
        }
    }
}
