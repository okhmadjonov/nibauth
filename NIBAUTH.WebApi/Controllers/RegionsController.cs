using AutoMapper;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Operations.Regions;
using NIBAUTH.Application.Operations.Regions.Queries.GetDetails;
using NIBAUTH.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace BB.WebApi.Controllers
{
    public class RegionsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;
        public RegionsController(IMapper mapper, INIBAUTHDbContext context) => (_mapper, _context) = (mapper, context);

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionGetAllQueryVm>> GetAll()
        {
            var query = new RegionGetAllQuery
            {
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionDetailsVm>> GetDetails(Guid id)
        {

            var query = new RegionDetailsQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
