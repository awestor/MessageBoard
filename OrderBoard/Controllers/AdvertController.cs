using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Adverts.Services;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }
    }
}
