using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Constants
{
    public class ActionConsts
    {
        public const string ApiVersion = "api/v1";

        public class User
        {
            public const string UserSignIn = "user/sign_in";
            public const string UserSignUp = "user/sign_up";
        }
        public class Admin
        {
            public const string UserSignIn = "admin/sign_in";
            public const string UserSignUp = "admin/sign_up";
        }
    }
}
