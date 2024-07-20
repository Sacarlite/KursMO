using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using MetaInfo;
using OptimizationMathMethods.VisualzationPages;
using OptimizationMathMethods.VievModels;

namespace OptimizationMathMethods
{
    public class OptmizitationMethod: VisualisationOptimization, IDisposable
    {

        private readonly string _name;
        private readonly string _path;
        private Type? _type;
        private MethodInfo? _getInfo;
        private AssemblyLoadContext? _assemblyLoadContext;
        public VisualisationPage VisualPage {  get; private set; }
        public OptmizitationMethod(string name,string path, CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors) :base( correctionFactors,  limitations,  exhaustiveSearchFactors)
        {
            _name = name;
            _path = path;
            LoadAssembly();
        }
        public Point GetExtr(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            var result =(Tuple<Point, List<Visualisation>>)_getInfo?.Invoke(null, new object[] { correctionFactors , limitations , exhaustiveSearchFactors });
            ConstructVisualisation(result.Item2);
            GetPoints();
            return result.Item1 ;
        }
       
        private void LoadAssembly()
        {
            try
            {
                _assemblyLoadContext = new AssemblyLoadContext(name: _name, isCollectible: true);
                var asm = _assemblyLoadContext.LoadFromAssemblyPath(_path);
                var fileName = Path.GetFileNameWithoutExtension(_path);
                _type = asm.GetType(fileName);
                _getInfo = _type.GetMethod("GetInfo");
            }
            catch (Exception)
            {

                throw new Exception($"Произошла ошибка при выгрузке данных у метода {_name},по пути {_path}");
            }
        }
        public void Dispose()
        {
            _assemblyLoadContext?.Unload();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public override void ConstructVisualisation(List<Visualisation> visualisation)
        {
            VisualisationPage VisualPage = new VisualisationPage(new MainVisualizationPageVievModel(points,visualisation));
        }
    }
}
