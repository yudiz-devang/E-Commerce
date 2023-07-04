using AutoMapper;

namespace e_commerce.api.Helpers
{
    public class UserHelper : IDisposable
    {
        #region  Constructor
        private EcommerceContext Dbcontext { get; set; }
        private ICrypto Crypto { get; set; }
        public IMapper Mapper { get; set; }


        public UserHelper(EcommerceContext _context,ICrypto _crypto,IMapper mapper)
            {
                 this.Dbcontext = _context;
                 this.Crypto = _crypto;
                 this.Mapper = mapper;
            }

        #endregion

        #region 1. User Signin 
        
        public async Task<BaseResponse> Login(UserSigninReqeust reqeust)
        {
            try
            {
                //Encrypt Password
                var password = this.Crypto.EncryptPassword(reqeust.Password);

                //Check Username

                if(!this.Dbcontext.Users.Any(x=>x.EmailId.Equals(reqeust.EmailId.ToLower()) || x.Password.Equals(password) && !x.Deleted)) 
                    return await Task.FromResult(new BaseResponse { IsSuccess = false , ErrorMessage = "Invalid Email address & passowrd"});

                if (!this.Dbcontext.Users.Any(x => x.EmailId.Equals(reqeust.EmailId.ToLower()) && !x.Deleted))
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_account_notfoud" });

                var users = await Dbcontext.Users.FirstOrDefaultAsync(x=>x.EmailId.Equals(reqeust.EmailId.ToLower()) &&
                x.Password.Equals(reqeust.Password) &&  
                !x.Deleted);

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
                    users.Address
                };

                return await Task.FromResult(new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) });
            }
            catch (Exception ex) {
                return await Task.FromResult(new BaseResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Data = JsonConvert.SerializeObject(ex)
                });
            }
        }
        public void Dispose() => GC.SuppressFinalize(this);


        #endregion

        #region User SignUp
        
        #endregion
    }
}
