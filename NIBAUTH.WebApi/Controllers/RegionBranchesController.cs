using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Domain.Entities;
using NIBAUTH.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NIBAUTH.WebApi.Controllers.Base;
using NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche;
using NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll;

namespace NIBAUTH.WebApi.Controllers
{
    public class RegionBranchesController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public RegionBranchesController(IMapper mapper, INIBAUTHDbContext context, IConfiguration configuration, UserManager<User> userManager) =>
            (_mapper, _context, _configuration, _userManager) = (mapper, context, configuration, userManager);

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<ActionResult<CreateRegionBrancheVm>> Create([FromForm] CreateRegionBrancheDto createRBDto)
        {
            var command = _mapper.Map<CreateRegionBrancheCommand>(createRBDto);
            if (command == null)
            {
                return BadRequest("Form fields is not correct");
            }
            command.CreatedByUserId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }


        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionBranchesAllVm>> GetAll(string s = "", Guid? RegionId = null)
        {
            var query = new RegionBranchesAllQuery
            {
                Search = s,
                RegionId = RegionId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
