using MetaInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExelExplorer
{
    public interface IExelExplorer
    { 
            void ExportInputData(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors, int tau, string fileName);
            Tuple<CorrectionFactors, Limitations, ExhaustiveSearchFactors, int> Import(string fileName);
    }
}
