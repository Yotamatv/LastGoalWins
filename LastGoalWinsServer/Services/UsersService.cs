using LastGoalWinsServer.Exceptions;
using LastGoalWinsServer.Models.Login;
using LastGoalWinsServer.Models.SQLModels;
using LastGoalWinsServer.Services.DataBase;
using LastGoalWinsServer.Utils;

namespace LastGoalWinsServer.Services
{
    public class UsersService
    {
        private readonly UsersDbService _usersDbService;
        private readonly PasswordHelper _passwordHelper;
        public UsersService(UsersDbService usersDbService, PasswordHelper passwordHelper)
        {
            _usersDbService = usersDbService;
            _passwordHelper = passwordHelper;
        }
        public async Task<object> CreateUserAsync(UserSql newUser)
        {
            newUser.Password = _passwordHelper.GeneratePassword(newUser.Password);

            bool result = await _usersDbService.CreateUserAsync(newUser);
            if (result)
            {
                return new { newUser.Id, newUser.FirstName, newUser.Email };
            }
            else
            {
                throw new UserAlreadyExistsException("User with this email already exists.");
            }
        }


        public async Task<List<UserSql>> GetUsersAsync()
        {
            return await _usersDbService.GetAllUsersAsync();
        }

        public async Task<UserSql> GetOneUserAsync(string userId)
        {
            UserSql user = await _usersDbService.GetOneUserAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }
            return user;
        }

        public async Task DeleteUserAsync(string userId)
        {
            bool result = await _usersDbService.DeleteUserAsync(userId);
            if (!result)
            {
                throw new UserNotFoundException(userId);
            }
        }


        public async Task<UserSql> EditUserAsync(string userId, UserSql updatedUser)
        {
            UserSql user = await _usersDbService.EditUserAsync(userId, updatedUser);
            if (user == null)
            {
                throw new UserNotFoundException("User with ID: " + userId + " not found.");
            }
            return user;
        }

        public async Task<UserSql> LoginAsync(LoginModel loginModel)
        {
            var user = await _usersDbService.GetUserByEmail(loginModel.Email);

            if (user == null || !_passwordHelper.VerifyPassword(user.Password, loginModel.Password))
            {
                throw new AuthenticationException();
            }

            // Consider not including the password in the returned user object
            user.Password = null;
            return user;
        }
    }
}
