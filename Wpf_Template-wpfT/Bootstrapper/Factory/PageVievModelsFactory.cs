using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Factory
{
    public class PageVievModelsFactory<TResult> : IPageVievModelsFactory<TResult>
    {
        private readonly IComponentContext _componentContext;

        public PageVievModelsFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public TResult Create()
        {
            var factory = _componentContext.Resolve<Func<TResult>>();
            return factory.Invoke();
        }
    }
}
