using Component.Components.Interfaces;
using DataAccess.UnitOfWork;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace FlatClub.MemberhipProvider
{
    public class FlatClubMembershipProvider : MembershipProvider
    {
        [Inject]
        public IUserComponent UserComponent { get; set; }

        private string applicationName;
        private string providerName;
        private bool requiresUniqueEmail;
        private int minRequiredPasswordLength;

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            applicationName = config["applicationName"].GetDefaultValue(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            minRequiredPasswordLength = Convert.ToInt32(config["minRequiredPasswordLength"].GetDefaultValue("6"));
            requiresUniqueEmail = Convert.ToBoolean(config["requiresUniqueEmail"].GetDefaultValue("false"));
            providerName = name;
        }
        
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get {
                return minRequiredPasswordLength; 
            }
        }

        public override bool RequiresUniqueEmail
        {
            get {
                return requiresUniqueEmail; 
            }
        }

        public string ProviderName
        {
            get {
                return providerName;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            
            var user = GetUser(username, false);

            if (user == null)
            {
                if (UserComponent.RegisterUser(username, password))
                {
                    status = MembershipCreateStatus.Success;
                    return GetUser(username, false);     
                }
                else
                {
                    status = MembershipCreateStatus.ProviderError;
                }
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }        

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = UserComponent.GetUser(providerUserKey);
            if (user != null)
            {
                return new MembershipUser(ProviderName, user.Name, user.Id, "", "", "", true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.Now);
            }
            else
            {
                return null;
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = UserComponent.GetUser(username);
            if (user != null)
            {
                return new MembershipUser(ProviderName, user.Name, user.Id, "", "", "", true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.Now);
            }
            else
            {
                return null;
            }
        }
        
        public override bool ValidateUser(string username, string password)
        {
            var user = UserComponent.GetUser(username, password);

            return user != null;
        }


        #region NotImplemented
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
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

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}