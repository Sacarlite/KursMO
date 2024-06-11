namespace Bootstrapper.Factory
{
    public interface IPageVievModelsFactory<TResult>
    {
        TResult Create();
    }
}