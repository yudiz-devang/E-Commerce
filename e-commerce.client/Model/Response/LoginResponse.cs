namespace e_commerce.client.Model.Response
{
    public class LoginResponse : CallAPIList
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public string Image { get; set; }

        public string BirthDate { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public int? ZipCode { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string? UniqueId { get; set; }

        public bool IsSuccess { get; set; }

    }
}
