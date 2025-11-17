using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Operations.Users.Queries.GetDetails;
using NIBAUTH.Application.Operations.Users.Queries.GetList;
using NIBAUTH.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NIBAUTH.WebApi.Controllers
{

    public class UsersController : BaseController
    {
        private readonly INIBAUTHDbContext _context;
        public UsersController(INIBAUTHDbContext context) => _context = context;

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersListVm>> GetList(int pi = 1, int ps = 10, string ot = "desc",
             string oc = "UserName", string userName = ""
            )
        {

            var query = new UsersListQuery
            {
                OrderColumn = oc,
                OrderType = ot,
                PageIndex = pi,
                PageSize = ps,
                UserName = userName

            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<UserDetailsVm>> GetDetails(Guid id)
        {

            var query = new UserDetailsQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


    }
}
