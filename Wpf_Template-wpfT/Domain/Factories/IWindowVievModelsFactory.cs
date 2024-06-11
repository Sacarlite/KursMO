namespace Domain.Factories;

public interface IWindowVievModelsFactory<TResult>
{
    TResult Create();
}