using Component.Components.Implementations;
using Component.Components.Interfaces;
using DataAccess.UnitOfWork;
using FlatClub.MemberhipProvider;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FlatClub
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver() 
        {
            kernel = new StandardKernel(); 
            AddBindings(); 
        }

        public object GetService(Type serviceType) 
        { 
            return kernel.TryGet(serviceType);
        }    
        
        public IEnumerable<object> GetServices(Type serviceType) 
        { 
            return kernel.GetAll(serviceType); 
        }

        private void AddBindings() 
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserComponent>().To<UserComponent>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IStoryComponent>().To<StoryComponent>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IGroupComponent>().To<GroupComponent>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Inject(Membership.Provider);
        } 
    }
}