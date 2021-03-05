using Microsoft.AspNetCore.Mvc;

namespace ConnectFourWinner.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        private const string Running = "Running...";     

        [HttpGet]
        public string Get()
        {
            return Running;
        }
    }
}
