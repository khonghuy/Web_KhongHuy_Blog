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
            return await _appDbContext.Users.AsNoTracking().Select(user=>new UserDto(){
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                ID = user.Id,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
            }).ToListAsync();
            
        }
        public async Task<bool> DeleteUser(Guid Id){
            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if(user == null){
                return false;
               
            };
             _appDbContext.Users.Remove(user);
             await _appDbContext.SaveChangesAsync();
             return true;
        }   

        public async Task<UserDto>  EditUser (Guid Id, User user)
        {
            var userExist = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if(userExist == null){
                return null;
            };
            userExist.DisplayName = user.DisplayName;
            userExist.Email = user.Email;
            userExist.Phone = user.Phone;
            userExist.DateOfBirth = user.DateOfBirth;
            userExist.Address = user.Address;
            await _appDbContext.SaveChangesAsync();

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
