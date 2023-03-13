using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.Common.Paging;
using Microsoft.EntityFrameworkCore;

namespace Fsoft.Web.Locdx7.DL
{
    public class UserDL : IUserDL
    {

        private readonly Mock_Project_FSoft_locdx7Context _context;
        public UserDL(Mock_Project_FSoft_locdx7Context ctx)
        {
            _context = ctx;
        }
        #region Authentication

        public async Task<bool> VerifyEmail(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerifityToken == token);
            if(user == null)
            {
                return false;
            }

            user.VerifiyAt = DateTime.Now;
            user.VerifyEmail = true;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<string> LoginUser(User userRequest)  
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);

            if(user == null)
            {
                return "Not exits User";
            }

            if(user.Password != userRequest.Password)
            {
                return "Password incorrect";
            }
            var token = user.VerifityToken;

            return token;
        }
        #endregion

        #region Paging
        public async Task<List<User>> GetUsersPaging(PagingFilter filter)
        {
            var TotalPage = await _context.Users.CountAsync();
            filter.TotalPage = TotalPage;

            var validFilter = new PagingFilter(filter.PageNumber, filter.PageSize);


            var userPaging = await _context.Users
                                 .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                 .Take(validFilter.PageSize)
                                 .ToListAsync();
            return userPaging;
        } 
        #endregion


        #region CRUD
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> DeleteUser(int id)
        {
            User user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return id;
            }
            return 0;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }


        public async Task<int> InsertUser(User user)
        {

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                throw new Exception("User is exists");
            }
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                throw new Exception("Email is exists");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<int> UpdateUser(int id, User userUpdate)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                // Đánh dấu rằng user đã bị thay đổi
                // Update 1 object ,ko cần update từng thuộc tính
                _context.Entry(user).CurrentValues.SetValues(userUpdate);
                await _context.SaveChangesAsync();
                return id;
            }

            return 0;
        }
        #endregion
        
    }
}
