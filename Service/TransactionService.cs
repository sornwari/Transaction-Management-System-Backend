using Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service
{
    public class TransactionService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public TransactionService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }


        public async Task<List<TransactionViewModel>> SearchTransactions(SearchTransactionsViewModel searchModel)
        {
            try
            {
                DateTime utcDateTimeFrom = new DateTime();
                DateTime utcDateTimeTo = new DateTime();
                if (searchModel.FromDate != null)
                {
                    utcDateTimeFrom = DateTime.SpecifyKind(searchModel.FromDate.Value, DateTimeKind.Utc);
                }
                if (searchModel.ToDate != null)
                {
                    utcDateTimeTo = DateTime.SpecifyKind(searchModel.ToDate.Value, DateTimeKind.Utc);
                }
                var query = from transaction in _db.Transaction
                            join account in _db.Account on transaction.AccountId equals account.Id
                            join user in _db.User on account.UserId equals user.Id
                            where (string.IsNullOrEmpty(searchModel.Name) || user.Name.ToLower().Contains(searchModel.Name.ToLower()))
                            && (string.IsNullOrEmpty(searchModel.AccountNo) || account.AccountNo.ToLower().Contains(searchModel.AccountNo.ToLower()))
                            && (string.IsNullOrEmpty(searchModel.TransactionName) || transaction.TransactionName.ToLower().Contains(searchModel.TransactionName.ToLower()))
                            && (string.IsNullOrEmpty(searchModel.TransactionType) || transaction.TransactionType.ToLower() == searchModel.TransactionType.ToLower())
                            && (string.IsNullOrEmpty(searchModel.Status) || transaction.Status.ToLower() == searchModel.Status.ToLower())
                            && (searchModel.FromDate == null || transaction.CreateDate >= utcDateTimeFrom)
                            && (searchModel.ToDate == null || transaction.CreateDate <= utcDateTimeTo)
                            select new TransactionViewModel
                            {
                                Id = transaction.Id,
                                AccountNo = account.AccountNo,
                                Name = user.Name,
                                TransactionName = transaction.TransactionName,
                                TransactionType = transaction.TransactionType,
                                TotalBeforeTransaction = transaction.TotalBeforeTransaction,
                                TotalAfterTransaction = transaction.TotalAfterTransaction,
                                Amount = transaction.Amount,
                                Status = transaction.Status,
                                CreateDate = transaction.CreateDate,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateTransaction(CreateTransactionViewModel model)
        {
            try
            {
                var isSuccess = false;
                var account = await _db.Account.Where(x => x.AccountNo == model.AccountNo).FirstOrDefaultAsync();
                if(account != null)
                {
                    var transaction = new Transaction
                    {
                        AccountId = account.Id,
                        TransactionName = model.TransactionName,
                        TransactionType = model.TransactionType,
                        Amount = model.Amount,
                        TotalBeforeTransaction = account.Balance,
                        TotalAfterTransaction = model.TransactionType == "Deposit" ? account.Balance + model.Amount : account.Balance - model.Amount,
                        Status = model.Status,
                        CreateDate = DateTime.UtcNow,
                        CreateBy = model.CreateBy,
                    };
                    await _db.Transaction.AddAsync(transaction);

                    if(model.Status == "Completed")
                    {
                        if (model.TransactionType == "Deposit")
                        {
                            account.Balance = account.Balance + model.Amount;
                            account.Deposit = account.Deposit + model.Amount;
                        }
                        else
                        {
                            account.Balance = account.Balance - model.Amount;
                            account.Withdraw = account.Withdraw + model.Amount;
                        }
                        _db.Account.Update(account);
                    }

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


        public async Task<bool> UpdateTransaction(UpdateTransactionViewModel model)
        {
            try
            {
                var isSuccess = false;
                var account = await _db.Account.Where(x => x.AccountNo == model.AccountNo).FirstOrDefaultAsync();
                var transaction = await _db.Transaction.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (account != null && transaction != null)
                {
                    transaction.TransactionName = model.TransactionName;
                    transaction.TransactionType = model.TransactionType;
                    transaction.Amount = model.Amount;
                    transaction.TotalBeforeTransaction = account.Balance;
                    transaction.TotalAfterTransaction = model.TransactionType == "Deposit" ? account.Balance + model.Amount : account.Balance - model.Amount;
                    transaction.Status = model.Status;
                    transaction.UpdateDate = DateTime.UtcNow;
                    transaction.UpdateBy = model.UpdateBy;
                    _db.Transaction.Update(transaction);

                    if (model.Status == "Completed")
                    {
                        if (model.TransactionType == "Deposit")
                        {
                            account.Balance = account.Balance + model.Amount;
                            account.Deposit = account.Deposit + model.Amount;
                        }
                        else
                        {
                            account.Balance = account.Balance - model.Amount;
                            account.Withdraw = account.Withdraw + model.Amount;
                        }
                        _db.Account.Update(account);
                    }

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

        public async Task<bool> DeleteTransaction(int Id)
        {
            try
            {
                var isSuccess = false;
                var existTransaction = await _db.Transaction.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (existTransaction != null)
                {
                    _db.Transaction.Remove(existTransaction);
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
