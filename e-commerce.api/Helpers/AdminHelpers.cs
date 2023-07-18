using ecommerce.models.Request.Admin;
using ecommerce.models.Request.User;
using ecommerce.models.Response.Base;
using ecommerce.repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace e_commerce.api.Helpers
{
    public class AdminHelpers : IDisposable
    {

        private EcommerceContext Dbcontext { get; set; }

        public AdminHelpers(EcommerceContext _context)
        {
            this.Dbcontext = _context;
        }

        #region Admin Sign In 
        public async Task<BaseResponse> Login(UserSigninReqeust reqeust)
        {
            try
            {
                if (!this.Dbcontext.Users.Any(x => x.EmailId.Equals(reqeust.EmailId.ToLower()) || !x.Deleted || x.IsAdmin == true))
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "Invalid Email address & passowrd" });

                if (!this.Dbcontext.Users.Any(x => x.EmailId.Equals(reqeust.EmailId.ToLower()) || !x.Deleted || x.IsAdmin == true))
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_account_notfoud" });

                var users = await Dbcontext.Users.FirstOrDefaultAsync(x => x.EmailId.Equals(reqeust.EmailId.ToLower()) &&
                x.Password.Equals(reqeust.Password) &&
                !x.Deleted && x.IsAdmin == true);

                if (!users.Active)
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_user_inactive" });
                var idList = new[] { users.CreatedBy, users.ModifiedBy };

                var userCreators = this.Dbcontext.Users.Where(x => idList.Contains(x.Id))
                .Select(x => new { x.Id, Name = $"{x.FirstName} ({x.LastName})" }).ToArray();

                var createdBy = userCreators.FirstOrDefault(x => x.Id.Equals(users.CreatedBy))?.Name;
                var modifiedBy = userCreators.FirstOrDefault(x => x.Id.Equals(users.ModifiedBy))?.Name;

                users.Active = true;
                this.Dbcontext.SaveChanges();
                var response = new
                {
                    users.Id,
                    users.FirstName,
                    users.LastName,
                    users.EmailId,
                    users.Password,
                    users.CreatedBy,
                    users.ModifiedBy,
                    users.State,
                    users.City,
                    users.Image,
                    users.ZipCode,
                    users.MobileNumber,
                    users.BirthDate,
                    users.Address,
                    users.IsAdmin
                };

                return await Task.FromResult(new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Data = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion

        public void Dispose() => GC.SuppressFinalize(this);
    }
}



