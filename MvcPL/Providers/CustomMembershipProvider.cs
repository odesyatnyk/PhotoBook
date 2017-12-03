//Select Assemblies - > Extensions -> System.Web.Helpers

using System;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    //провайдер членства помогает системе идентифицировать пользователя
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserService UserService
        {
            get { return (IUserService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService)); }
        }

        public IRoleService RoleService
        {
            get { return (IRoleService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService)); }
        }

        public IProfileService ProfileService
        {
            get
            {
                return (IProfileService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IProfileService));
            }
        }

        public MembershipUser CreateUser(string email, string name, string password)
        {
            var user = new UserEntity
            {
                Email = email,
                UserName = name,
                Password = Crypto.HashPassword(password),
                RoleId = RoleService.GetRoleByName("User").Id,
                Profile = new ProfileEntity()
                //http://msdn.microsoft.com/ru-ru/library/system.web.helpers.crypto(v=vs.111).aspx
            };


            UserService.Create(user);
            var membershipUser = GetUser(name, false);
            return membershipUser;
        }

        public override bool ValidateUser(string name, string password)
        {
            var user = UserService.GetUserByName(name);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                //Определяет, соответствуют ли заданный хэш RFC 2898 и пароль друг другу
            {
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string name, bool userIsOnline)
        {
            var user = UserService.GetUserByName(name);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.UserName,
                null, null, null, null,
                false, false, default(DateTime),
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }


        public override string GetUserNameByEmail(string email)
        {
            var user = UserService.GetUserByEmail(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.UserName,
                null, null, null, null,
                false, false, default(DateTime),
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser.UserName;
        }

        #region Stabs

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}