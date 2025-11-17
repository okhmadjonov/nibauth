using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using NIBAUTH.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock;
using NIBAUTH.Application.Operations.BranchBlock.Queries.GetAll;


namespace NIBAUTH.WebApi.Controllers
{
    public class BranchBlocksController:BaseController
    {


        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public BranchBlocksController(IMapper mapper, INIBAUTHDbContext context, IConfiguration configuration, UserManager<User> userManager) =>
            (_mapper, _context, _configuration, _userManager) = (mapper, context, configuration, userManager);

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<ActionResult<CreateBrancheBlockVm>> Create([FromForm] CreateBrancheBlockDto createRBDto)
        {
            var command = _mapper.Map<CreateBrancheBlockCommand>(createRBDto);
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
        public async Task<ActionResult<BranchesBlockAllVm>> GetAll(string s = "", Guid? RegionBranchId = null)
        {
            //string local = await GetLocal(_context);
            var query = new BranchesBlockAllQuery
            {
                Search = s,
                RegionBranchId = RegionBranchId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
