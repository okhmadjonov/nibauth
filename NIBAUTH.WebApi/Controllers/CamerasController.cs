using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Operations.Cameras.Commands.Create;
using NIBAUTH.Application.Operations.Cameras.Queries.GetAll;
using NIBAUTH.Domain.Entities;
using NIBAUTH.WebApi.Controllers.Base;

namespace NIBAUTH.WebApi.Controllers
{
    public class CamerasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public CamerasController(IMapper mapper, INIBAUTHDbContext context, IConfiguration configuration, UserManager<User> userManager) =>
            (_mapper, _context, _configuration, _userManager) = (mapper, context, configuration, userManager);

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<ActionResult<CreateCameraVm>> Create([FromForm] CreateCameraDto createCameraDto)
        {
            var command = _mapper.Map<CreateCameraCommand>(createCameraDto);
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
        public async Task<ActionResult<CamerasAllVm>> GetAll(string s = "", Guid? BranchBlockId = null)
        {
            //string local = await GetLocal(_context);
            var query = new CamerasAllQuery
            {
                Search = s,
                BranchBlockId = BranchBlockId,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        } 
    }
}
