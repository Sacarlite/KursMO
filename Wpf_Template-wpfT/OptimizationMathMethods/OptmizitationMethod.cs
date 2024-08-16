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
using Domain.MethodsBD;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Logging;

namespace OptimizationMathMethods
{
    public partial class OptmizitationMethod: VisualisationOptimization, IDisposable
    {

        private readonly string? _name;
        private readonly string? _path;
        private Type? _type;
        private MethodInfo? _getInfo;
        private AssemblyLoadContext? _assemblyLoadContext;
        public OptmizitationMethod(Method method, CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors) :base( correctionFactors,  limitations,  exhaustiveSearchFactors)
        {
            _name = method.Name;
            _path = method.Path;
            LoadAssembly();
        }
        public OptmizitationMethod()
        {
        }
        public Tuple<Point,Point> GetExtr()
        {
            try
            {
                var result = (Tuple<Point, Point>)_getInfo?.Invoke(null, new object[] { correctionFactors, limitations, exhaustiveSearchFactors });
                return new Tuple<Point, Point>(result.Item1, result.Item2);
            }
            catch (Exception) {
                throw new Exception();
            }
        }
       
        private void LoadAssembly()
        {
            try
            {
                _assemblyLoadContext = new AssemblyLoadContext(name: _name, isCollectible: true);
                var asm = _assemblyLoadContext.LoadFromAssemblyPath(_path);
                var fileName = Path.GetFileNameWithoutExtension(_path);
                _type = asm.GetType(fileName+'.'+ fileName);
                _getInfo = _type.GetMethod("GetInfo", BindingFlags.Public | BindingFlags.Static);
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
    }
}
