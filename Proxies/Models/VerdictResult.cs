using System.Net;

namespace Proxies.Models
{
    public class VerdictResult
    {
        public VerdictDto VerdictDto { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}