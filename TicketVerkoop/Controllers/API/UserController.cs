using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IService<AspNetUser> userService;

        public UserController(IMapper mapper, IService<AspNetUser> userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<StadiumVM>> Get()
        {
            try
            {
                var listStadiums = await userService.GetAll();
                var data = mapper.Map<List<UserVM>>(listStadiums);

                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
