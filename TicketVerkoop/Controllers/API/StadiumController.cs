using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private IService<Stadium> stadiumService;
        private readonly IMapper mapper;

        public StadiumController(IMapper mapper, IService<Stadium> stadiumService)
        {
            this.mapper = mapper;
            this.stadiumService = stadiumService;
        }

        [HttpGet]
        public async Task<ActionResult<StadiumVM>> Get()
        {
            try
            {
                var listStadiums = await stadiumService.GetAll();
                var data = mapper.Map<List<StadiumVM>>(listStadiums);

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
