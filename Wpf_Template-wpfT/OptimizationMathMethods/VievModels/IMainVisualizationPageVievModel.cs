using System.Data;
using MetaInfo;

namespace OptimizationMathMethods.VievModels
{
    public interface IMainVisualizationPageVievModel
    {
        void ReloadPages(Tuple<List<List<Point>>, DataTable> tuple);
    }
}
