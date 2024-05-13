using Microsoft.AspNetCore.Mvc;

namespace OnboardingApi.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}