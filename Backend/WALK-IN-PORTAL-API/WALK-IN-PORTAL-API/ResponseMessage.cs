using Microsoft.AspNetCore.Http.HttpResults;

namespace WALK_IN_PORTAL_API
{
    public class ResponseMessage
    {
        public string message { get; set; }
        public int statusCode { get; set; }
        public bool moveNext { get; set; }
    }
}
