namespace e_commerce.api.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        #region Object Declarations And Constructor

        protected IStringLocalizer<BaseController> Localizer { get; }
        public ExceptionFilter(IStringLocalizer<BaseController> localizer)
        {
            this.Localizer = localizer;
        }
        #endregion

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                var exception = context.Exception as ApiException;
                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, exception.StatusCode);
            }
            else if (context.Exception is SqlException)
            {
                var exception = context.Exception as SqlException;

                var statusCode = 400;

                if (!string.IsNullOrWhiteSpace(exception.Message) && (exception.Message.Equals("error_access_token_expired") || exception.Message.Equals("sql_error_user_deactivated")))
                    statusCode = 401;

                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, statusCode);
            }
            else
            {
                var exception = context.Exception as Exception;
                // LogManager.LogError(exception.Message, exception);
                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, 500);
            }
        }

        #region API Exception class declaration.

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }
            public IEnumerable<string> Errors { get; }

            public ApiException(string message, int statusCode = 400, IEnumerable<string> Errors = null) : base(message)
            {
                StatusCode = statusCode;
                this.Errors = Errors;
            }

            public ApiException(Exception ex, int statusCode = 400) : base(ex.Message)
            {
                StatusCode = statusCode;
            }
        }

        #endregion API Exception class declaration.S
    }
}