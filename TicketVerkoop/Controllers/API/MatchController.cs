using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMatchService<Match> matchService;
        private readonly IMapper mapper;

        public MatchController(IMapper mapper, IMatchService<Match> matchService)
        {
            this.mapper = mapper;
            this.matchService = matchService;
        }

        [HttpGet]
        public async Task<ActionResult<MatchSwaggerVM>> Get()
        {
            try
            {
                var listMatches = await matchService.GetAllWithHistory();
                var data = mapper.Map<List<MatchSwaggerVM>>(listMatches);

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
