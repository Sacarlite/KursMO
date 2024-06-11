using Autofac;
using Domain.Factories;

namespace Application.Factory;

public class WindowVievModelsFactory<TResult> :IWindowVievModelsFactory<TResult>
{
    private readonly IComponentContext _componentContext;

    public WindowVievModelsFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public TResult Create()
    {
        var factory = _componentContext.Resolve<Func<TResult>>();
        return factory.Invoke();
    }
}