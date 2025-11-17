//namespace NIBAUTH.Application.Operations.Auth.Login
//{
//    public class LoginVm
//    {
//        public string? Token { get; set; }
//        public string? RefreshToken { get; set; }
//        public DateTime Expiration { get; set; }
//    }
//}


namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginVm
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        //public Guid? RegionId { get; set; }
        public string? RegionName { get; set; }
        public string? RegionCode { get; set; }
        public string? DefaultRole { get; set; }
        public List<string>? Roles { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}