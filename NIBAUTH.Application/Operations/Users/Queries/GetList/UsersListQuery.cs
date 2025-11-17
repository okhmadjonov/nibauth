using MediatR;

namespace NIBAUTH.Application.Operations.Users.Queries.GetList
{
    public class UsersListQuery : IRequest<UsersListVm>
    {
        public string OrderColumn { get; set; } = "UserName";
        public string OrderType { get; set; } = "desc";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string UserName { get; set; } = "";

    }
}
