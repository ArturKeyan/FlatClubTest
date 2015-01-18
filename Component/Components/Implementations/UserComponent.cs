using Component.Components.Interfaces;
using Component.Models;
using DataAccess.Entity;
using DataAccess.UnitOfWork;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Component.Components.Implementations
{
    public class UserComponent : ComponentBase, IUserComponent
    {
        public UserComponent(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                unit.UserRepository.Insert(new User {
                    Name = username,
                    Password = password.GetMD5()
                });

                unit.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public UserModel GetUser(object providerUserKey)
        {
            try
            {
                var user = unit.UserRepository.GetByID((int)providerUserKey);

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public UserModel GetUser(string username, string password)
        {
            try
            {
                var user = unit.UserRepository.Get(username, password.GetMD5());

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;         
        }

        public UserModel GetUser(string username)
        {
            try
            {
                var user = unit.UserRepository.Get(username);

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
