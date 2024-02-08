using Microsoft.AspNetCore.Identity;

namespace MyApp2._1._7.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        // IdentityUserには管理テーブルの情報が含まれている(UserName)
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserName(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.UserName;
        }
    }
}
