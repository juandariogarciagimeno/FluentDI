using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDI.Interfaces
{
    public interface IRuntimeDependencySelector<TInterface>
    {
        public Func<TInterface, object[], bool> CanInject { get; }
    }
}
