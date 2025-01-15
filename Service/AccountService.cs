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
    public class AccountService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public AccountService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<List<AccountViewModel>> SearchAccounts(SearchAccountsViewModel searchModel)
        {
            try
            {

                var query = from account in _db.Account
                            join user in _db.User on account.UserId equals user.Id
                            where (string.IsNullOrEmpty(searchModel.AccountNo) || account.AccountNo.ToLower().Contains(searchModel.AccountNo.ToLower()))
                            && (string.IsNullOrEmpty(searchModel.Name) || user.Name.ToLower().Contains(searchModel.Name.ToLower()))
                            select new AccountViewModel
                            {
                                Id = account.Id,
                                AccountNo = account.AccountNo,
                                UserId = user.Id,
                                Name = user.Name,
                                Balance = account.Balance,
                                Deposit = account.Deposit,
                                Withdraw = account.Withdraw,
                                CreateDate = account.CreateDate
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateAccount(CreateAccountViewModel model)
        {
            try
            {
                var isSuccess = false;
                var existUser = await _db.User.Where(x => x.Id == model.UserId).FirstOrDefaultAsync();
                var existAccount = await _db.Account.Where(x => x.AccountNo == model.AccountNo).FirstOrDefaultAsync();
                if (existUser != null && existAccount == null)
                {
                    var account = new Account
                    {
                        AccountNo = model.AccountNo,
                        UserId = model.UserId,
                        Deposit = 0,
                        Withdraw = 0,
                        Balance = 0,
                        CreateDate = DateTime.UtcNow,
                        CreateBy = model.CreateBy,
                    };

                    await _db.Account.AddAsync(account);
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

        public async Task<bool> UpdateAccount(UpdateAccountViewModel model)
        {
            try
            {
                var isSuccess = false;
                var existAccount = await _db.Account.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (existAccount != null)
                {
                    existAccount.AccountNo = model.AccountNo;
                    existAccount.UserId = model.UserId;
                    existAccount.UpdateDate = DateTime.UtcNow;
                    existAccount.UpdateBy = model.UpdateBy;

                    _db.Account.Update(existAccount);
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

        public async Task<bool> DeleteAccount(int Id)
        {
            try
            {
                var isSuccess = false;
                var existAccount = await _db.Account.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (existAccount != null)
                {
                    _db.Account.Remove(existAccount);
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
