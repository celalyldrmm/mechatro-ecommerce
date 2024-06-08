
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace mechatro_ecommerce.Models
{
    public class UserRepository : IUserRepository
    {
        MechatroContext context = new MechatroContext();
        public async Task<User> loginControl(string usernameOrEmail, string password)
        {
            string md5Sifre = MD5Sifrele(password);
            User? usr = await context.Users.FirstOrDefaultAsync(u => (u.Email == usernameOrEmail || u.UserName == usernameOrEmail) && u.Password == md5Sifre && u.IsAdmin == true && u.IsActive == true);

            return usr;
        }
        public  string MD5Sifrele(string value)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(value);
            btr = md5.ComputeHash(btr);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in btr)
            {
                sb.Append(item.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        public  User SelectMemberInfo(string Email)
        {
            using (MechatroContext context = new MechatroContext())
            {
                User? user = context.Users.FirstOrDefault(u => u.Email == Email);
                return user;
            }
        }
        public string MemberControl(User user)
        {
            using (MechatroContext context = new MechatroContext())
            {
                string answer = "";

                try
                {
                    string md5Sifre = MD5Sifrele(user.Password);
                    User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == md5Sifre);

                    if (usr == null)
                    {
                        answer = "error";
                    }
                    else
                    {
                        if (usr.IsAdmin == true)
                        {
                            answer = "admin";
                        }
                        else
                        {
                            answer = usr.Email;
                        }
                    }
                }
                catch (Exception)
                {
                    return "HATA";
                }
                return answer;
            }
        }
        public bool loginEmailControl(User user)
        {
            using (MechatroContext context = new MechatroContext())
            {
                User usr = context.Users.FirstOrDefault(u => u.Email == user.Email && u.UserName == user.UserName);
                if (usr == null) return false;
                else return true;
            }

        }
        public bool AddUser(User user)
        {
            using (MechatroContext context = new MechatroContext())
            {
                try
                {
                    user.IsActive = true;
                    user.IsAdmin = false;
                    user.Password = MD5Sifrele(user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }

        }



    }
}

