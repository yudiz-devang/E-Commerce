namespace ecommerce.models.Response.Base
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string Data { get; set; }

    }

    public class BaseAuditResponse
    {
        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class BaseIdResponse
    {
        public Guid Id { get; set; }
    }

    public class BaseMessageResponse
    {
        public string Message { get; set; }

        public Guid? UseId { get; set; }
    }
}
