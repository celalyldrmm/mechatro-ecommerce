namespace mechatro_ecommerce.Models
{
    public interface IUserRepository
    {

        Task<User> loginControl(string usernameOrEmail, string password);
        string MD5Sifrele(string value);
        User SelectMemberInfo(string Email);
        string MemberControl(User user);
        bool loginEmailControl(User user);
        bool AddUser(User user);
    }
}
