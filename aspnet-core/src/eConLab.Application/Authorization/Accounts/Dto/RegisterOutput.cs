namespace eConLab.Authorization.Accounts.Dto
{
    public class RegisterOutput
    {
        public bool CanLogin { get; set; }
    }

    public class RegisterResult
    {
        public long UserId { get; set; }
    }
}
