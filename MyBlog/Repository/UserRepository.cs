using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.Models;
using MyBlog.Data;
using MyBlog.Dto.User;

namespace MyBlog.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public UserDto InsertUser(User user)
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            var result = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                ID = user.ID,
                Phone = user.Phone,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
            };
            return result;
        }
        public async Task< List<UserDto>> GetUsers(){
            return await _context.Users.AsNoTracking().Select(user=>new UserDto(){
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                ID = user.Id,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
            }).ToListAsync();
            
        }
    }
}
