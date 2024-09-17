using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Domain.MethodsBD;
using MetaInfo;

namespace OptimizationMathMethods
{
    public partial class OptmizitationMethod : VisualisationOptimization, IDisposable
    {
        private readonly string? _name;
        private readonly string? _path;
        private Type? _type;
        private MethodInfo? _getInfo;
        private AssemblyLoadContext? _assemblyLoadContext;

        public OptmizitationMethod(
            Method method,
            CorrectionFactors correctionFactors,
            Limitations limitations,
            ExhaustiveSearchFactors exhaustiveSearchFactors
        )
            : base(correctionFactors, limitations, exhaustiveSearchFactors)
        {
            _name = method.Name;
            _path = method.Path;
            LoadAssembly();
        }

        public OptmizitationMethod(string path, string name = "")
        {
            _path = path;
            _name = name;
            LoadAssembly();
        }

        public OptmizitationMethod() { }

        public Point GetExtr()
        {
            try
            {
                var result = (Point)
                    _getInfo?.Invoke(
                        null,
                        new object[] { correctionFactors, limitations, exhaustiveSearchFactors }
                    );
                return result;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void LoadAssembly()
        {
            try
            {
                _assemblyLoadContext = new AssemblyLoadContext(name: _name, isCollectible: true);
                var asm = _assemblyLoadContext.LoadFromAssemblyPath(_path);
                var fileName = Path.GetFileNameWithoutExtension(_path);
                _type = asm.GetType(fileName + '.' + fileName);
                if (_type is null)
                {
                    throw new Exception();
                }
                _getInfo = _type.GetMethod("GetInfo", BindingFlags.Public | BindingFlags.Static);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Произошла ошибка при выгрузке данных у метода {_name},по пути {_path}"
                );
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
