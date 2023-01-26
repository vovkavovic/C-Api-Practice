using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // localhost:5000/weatherforecast name derived from the class name minus "Controller" at the end

    public class BaseApiController : ControllerBase
    {
        
    }
}