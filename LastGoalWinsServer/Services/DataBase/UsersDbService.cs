using LastGoalWinsServer.Exceptions;
using LastGoalWinsServer.Models.Login;
using LastGoalWinsServer.Models.SQLModels;
using LastGoalWinsServer.Utils;
using Microsoft.EntityFrameworkCore;

namespace LastGoalWinsServer.Services.DataBase
{
    public class UsersDbService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHelper _passwordHelper;
        private readonly UsersService _usersService;
        public UsersDbService(ApplicationDbContext context, PasswordHelper passwordHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
        }
        public async Task<bool> CreateUserAsync(UserSql newUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            if (existingUser != null)
            {
                return false;
            }

            newUser.Password = _passwordHelper.GeneratePassword(newUser.Password);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<UserSql>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<UserSql> GetOneUserAsync(string userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == int.Parse(userId))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }
           
            return user;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (!Int32.TryParse(userId, out int id))
            {
                // Handle the case where userId is not a valid integer string
                // Return false or throw an exception based on your application's requirement
                return false;
                // Alternatively, you can throw an exception:
                // throw new ArgumentException("Invalid userId format");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserSql> EditUserAsync(string userId, UserSql updatedUser)
        {
            int id = int.Parse(userId);
            var userSqlModel = await _context.Users.FindAsync(id);
            if (userSqlModel == null)
            {
                throw new UserNotFoundException("User with ID: " + id + " not found.");
            }

            // Update fields in UserSqlModel
            userSqlModel.FirstName = updatedUser.FirstName;
            userSqlModel.LastName = updatedUser.LastName;
            userSqlModel.Email = updatedUser.Email;
            userSqlModel.IsAdmin = updatedUser.IsAdmin;
            userSqlModel.Balance = updatedUser.Balance;
            userSqlModel.ProfilePictureUrl = updatedUser.ProfilePictureUrl;

            // Save changes in the database
            await _context.SaveChangesAsync();

            // Convert back to User type before returning, using the User constructor that accepts a UserSqlModel
            return userSqlModel;
        }
        public async Task<UserSql> LoginAsync(LoginModel loginModel)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (user == null || !_passwordHelper.VerifyPassword(user.Password, loginModel.Password))
            {
                throw new AuthenticationException();
            }

            // Consider not including the password in the returned user object
            user.Password = null;
            return user;
        }
        public async Task<UserSql> GetUserByEmail(string userEmail)
        {
            var user = await _context.Users
              .FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return null;
            return user;
        }
    }
}
