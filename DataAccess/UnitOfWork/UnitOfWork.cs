using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FlatClubContext ctx { get; set; }

        public UnitOfWork()
        {
            ctx = new FlatClubContext();
        }

        private IUserRepository userRepository;
        private IGroupRepository groupRepository;
        private IStoryRepository storyRepository;

        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(ctx)); }
        }

        public IGroupRepository GroupRepository
        {
            get { return groupRepository ?? (groupRepository = new GroupRepository(ctx)); }
        }

        public IStoryRepository StoryRepository
        {
            get { return storyRepository ?? (storyRepository = new StoryRepository(ctx)); }
        }

        public void Save()
        {
            try
            {
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());

                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }
        
        #region Dispose
        private bool disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }

            disposed = true;
        }
        #endregion Dispose
    }
}
