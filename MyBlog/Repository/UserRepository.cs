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
        public async Task<bool> DeleteUser(Guid Id){
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if(user == null){
                return false;
               
            };
             _context.Users.Remove(user);
             await _context.SaveChangesAsync();
             return true;
        }   

        public async Task<UserDto>  EditUser (Guid Id, User user)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if(userExist == null){
                return null;
            };
            userExist.DisplayName = user.DisplayName;
            userExist.Email = user.Email;
            userExist.Phone = user.Phone;
            userExist.DateOfBirth = user.DateOfBirth;
            userExist.Address = user.Address;
            await _context.SaveChangesAsync();

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                ID = user.Id,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
            };
        }
    }
}
