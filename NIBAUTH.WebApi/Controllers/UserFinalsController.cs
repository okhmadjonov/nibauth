using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Operations.Final.Commands.CreateFinal;
using NIBAUTH.Application.Operations.Final.Commands.DeleteFinal;
using NIBAUTH.Application.Operations.Final.Queries.GetAll;
using NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche;
using NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll;
using NIBAUTH.Domain.Entities;
using NIBAUTH.WebApi.Controllers.Base;

namespace NIBAUTH.WebApi.Controllers
{
    public class UserFinalsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public UserFinalsController(IMapper mapper, INIBAUTHDbContext context, IConfiguration configuration, UserManager<User> userManager) =>
            (_mapper, _context, _configuration, _userManager) = (mapper, context, configuration, userManager);



        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<ActionResult<CreateUserFinalVm>> Create([FromForm] CreateUserFinalDto userFinalDto)
        {
            var command = _mapper.Map<CreateUserFinalCommand>(userFinalDto);
            if (command == null)
            {
                return BadRequest("form_field_is_not_correct");
            }
            command.CreatedByUserId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserFinalsAllQueryVm>> GetAll(string s = "")
        {
            var query = new UserFinalsAllQuery
            {
                Search = s,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserFinalCommand
            {
                Id = id,
                UserId = UserId,
            };
            await Mediator.Send(command);
            return NoContent();
        }


    }
}
