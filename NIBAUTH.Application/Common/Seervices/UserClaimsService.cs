//using Microsoft.AspNetCore.Identity;
//using NIBAUTH.Application.Interfaces;
//using NIBAUTH.Domain.Entities;
//using System.Security.Claims;
//using Microsoft.EntityFrameworkCore;

//namespace NIBAUTH.Application.Common.Services
//{
//    public interface IUserClaimsService
//    {
//        Task<List<Claim>> GetUserClaimsAsync(User user);
//    }

//    public class UserClaimsService : IUserClaimsService
//    {
//        private readonly INIBAUTHDbContext _context;
//        private readonly UserManager<User> _userManager;

//        public UserClaimsService(INIBAUTHDbContext context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        public async Task<List<Claim>> GetUserClaimsAsync(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//            };

//            var roles = await _userManager.GetRolesAsync(user);
//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var userWithDetails = await _context.Users
//                .Include(u => u.RegionBranch)
//                    .ThenInclude(rb => rb.Region)
//                .FirstOrDefaultAsync(u => u.Id == user.Id);

//            if (userWithDetails?.RegionBranch != null)
//            {
//                claims.Add(new Claim("RegionBranchId", userWithDetails.RegionBranch.Id.ToString()));
//                claims.Add(new Claim("RegionBranchName", userWithDetails.RegionBranch.Name ?? ""));

//                if (userWithDetails.RegionBranch.Region != null)
//                {
//                    claims.Add(new Claim("RegionId", userWithDetails.RegionBranch.Region.Id.ToString()));
//                    claims.Add(new Claim("RegionName", userWithDetails.RegionBranch.Region.Name ?? ""));
//                    claims.Add(new Claim("RegionCode", userWithDetails.RegionBranch.Region.Code ?? ""));
//                }
//            }

//            return claims;
//        }
//    }
//}