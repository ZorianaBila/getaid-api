namespace GetAidBackend.Auth.Requests
{
    public class ChangePasswordRequest
    {
        public string UserEmail { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}