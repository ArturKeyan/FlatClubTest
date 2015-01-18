using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Components.Implementations
{
    public class ComponentBase
    {
        protected IUnitOfWork unit;

        protected ComponentBase(IUnitOfWork unit)
        {
            this.unit = unit;
        }
    }
}
