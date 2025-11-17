//using AutoMapper;
//using NIBAUTH.Application.Interfaces;
//using NIBAUTH.Application.Operations.Languages.Queries.GetAll;
//using NIBAUTH.Application.Operations.Languages.Queries.GetDetails;
//using NIBAUTH.WebApi.Controllers.Base;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace NIBAUTH.WebApi.Controllers
//{
//    public class LanguagesController : BaseController 
//    {
//        public readonly IMapper _mapper;
//        private readonly INIBAUTHDbContext _context;

//        public LanguagesController(IMapper mapper, INIBAUTHDbContext bBDbContext)
//        {
//            _mapper = mapper;
//            _context = bBDbContext;
//        }


//        [HttpGet("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<ActionResult<LanguageDetailsVm>> GetDetails(Guid id)
//        {
//            string local = await GetLocal(_context);

//            var query = new LanguageDetailsQuery
//            {
//                Id = id,
//                LangCode = local
//            };
//            var vm = await Mediator.Send(query);
//            return Ok(vm);
//        }

//        [HttpGet("all")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<ActionResult<LanguagesAllVm>> GetAll()
//        {
//            string local = await GetLocal(_context);
//            var query = new LanguagesAllQuery
//            {
//                LangCode = local
//            };
//            var vm = await Mediator.Send(query);
//            return Ok(vm);
//        }

//    }
//}
