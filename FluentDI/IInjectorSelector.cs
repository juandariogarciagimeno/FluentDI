using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDI
{
    public interface IInjectorSelector<TInterface>
    {
        public bool CanInject(TInterface @instance);
    }
}
