using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentDI.Interfaces;

namespace FluentDI.Factory
{
    public class DependencyFactory<TDependency>
    {
        private IEnumerable<TDependency> _dependencies;
        IRuntimeDependencySelector<TDependency> _factory;
        public DependencyFactory(IEnumerable<TDependency> dependencies, IRuntimeDependencySelector<TDependency> factory)
        {
            _dependencies = dependencies;
            _factory = factory;
        }

        public TDependency? Get(params object[] param)
        {
            return _dependencies.FirstOrDefault(x => _factory.CanInject.Invoke(x, param));
        }
    }
}
