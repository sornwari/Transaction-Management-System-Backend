using Database;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class UserService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public UserService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<List<UserViewModel>> SearchUsers(SearchUsersViewModel searchModel)
        {
            try
            {
                DateTime utcDateTimeFrom = new DateTime();
                DateTime utcDateTimeTo = new DateTime();
                if (searchModel.FromDate != null)
                {
                    utcDateTimeFrom = DateTime.SpecifyKind(searchModel.FromDate.Value, DateTimeKind.Utc);
                }
                if(searchModel.ToDate != null)
                {
                    utcDateTimeTo = DateTime.SpecifyKind(searchModel.ToDate.Value, DateTimeKind.Utc);
                }
                var ads = 0;
                var query = from user in _db.User
                            join role in _db.Role on user.RoleId equals role.Id
                            where (string.IsNullOrEmpty(searchModel.UserName) || user.UserName.ToLower() == searchModel.UserName.ToLower())
                            && (string.IsNullOrEmpty(searchModel.Name) || user.Name.ToLower() == searchModel.Name.ToLower())
                            && (string.IsNullOrEmpty(searchModel.RoleName) || role.Name == searchModel.RoleName)
                            && (searchModel.FromDate == null || user.UpdateDate >= utcDateTimeFrom)
                            && (searchModel.ToDate == null || user.UpdateDate <= utcDateTimeTo)
                            select new UserViewModel
                            {
                                Id = user.Id,
                                Name = user.Name,
                                UserName = user.UserName,
                                Password = user.Password,
                                Role = new RoleViewModel
                                {
                                    Id = role.Id,
                                    Name = role.Name,
                                }
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateUser(CreateUserViewModel model)
        {
            try
            {
                var isSuccess = false;
                var existUser = await _db.User.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefaultAsync();
                if (existUser == null)
                {
                    var user = new User
                    {
                        Name = model.Name,
                        UserName = model.UserName,
                        Password = model.Password,
                        RoleId = model.RoleId,
                        CreateDate = DateTime.UtcNow,
                        CreateBy = model.CreateBy,
                    };

                    await _db.User.AddAsync(user);
                    await _db.SaveChangesAsync();
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateUser(UpdateUserViewModel model)
        {
            try
            {
                var isSuccess = false;
                var existUser = await _db.User.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (existUser != null)
                {
                    existUser.Name = model.Name;
                    existUser.UserName = model.UserName;
                    existUser.Password = model.Password;
                    existUser.RoleId = model.RoleId;
                    existUser.UpdateDate = DateTime.UtcNow;
                    existUser.UpdateBy = model.UpdateBy;

                    _db.User.Update(existUser);
                    await _db.SaveChangesAsync();
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                var isSuccess = false;
                var existUser = await _db.User.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if(existUser != null)
                {
                    _db.User.Remove(existUser);
                    await _db.SaveChangesAsync();
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
