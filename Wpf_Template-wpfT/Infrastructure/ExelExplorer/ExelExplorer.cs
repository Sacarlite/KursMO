using Domain.ExelExplorer;
using MetaInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExelExplorer
{
    public class ExelExplorer:IExelExplorer
    {
        public void ExportInputData(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors,int tau,string fileName)
        {
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)application.Sheets[1];
            sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 2]].Merge();
            sheet.Columns[2].ColumnWidth = 30;
            sheet.Cells[1, 1] = "Параметры и множители";
            sheet.Cells[2, 1] = (nameof(correctionFactors.Alpfa)+"=");
            sheet.Cells[2, 2] = correctionFactors.Alpfa;
            sheet.Cells[3, 1] = (nameof(correctionFactors.Betta)+"=");
            sheet.Cells[3, 2] = correctionFactors.Betta;
            sheet.Cells[4, 1] = (nameof(correctionFactors.Mu) + "=");
            sheet.Cells[4, 2] = correctionFactors.Mu;
            sheet.Cells[5, 1] = (nameof(correctionFactors.Delta) + "=");
            sheet.Cells[5, 2] = correctionFactors.Delta;
            sheet.Cells[6, 1] = (nameof(correctionFactors.G) + "=");
            sheet.Cells[6, 2] = correctionFactors.G;
            sheet.Cells[7, 1] = (nameof(correctionFactors.A) + "=");
            sheet.Cells[7, 2] = correctionFactors.A;
            sheet.Cells[8, 1] = (nameof(correctionFactors.N) + "=");
            sheet.Cells[8, 2] = correctionFactors.N;
            sheet.Cells[9, 1] = (nameof(tau) + "=");
            sheet.Cells[9, 2] = tau;
            //
            sheet.Range[sheet.Cells[1, 3], sheet.Cells[1, 4]].Merge();
            sheet.Cells[1, 3] = "Ограничения";
            sheet.Cells[2, 3] = (nameof(limitations.MinT1) + "=");
            sheet.Cells[2, 4] = limitations.MinT1;
            sheet.Cells[3, 3] = (nameof(limitations.MaxT1) + "=");
            sheet.Cells[3, 4] = limitations.MaxT1;
            sheet.Cells[4, 3] = (nameof(limitations.MinT2) + "=");
            sheet.Cells[4, 4] = limitations.MinT2;
            sheet.Cells[5, 3] = (nameof(limitations.MaxT2) + "=");
            sheet.Cells[5, 4] = limitations.MaxT2;
            sheet.Cells[6, 3] = (nameof(limitations.MinDiff) + "=");
            sheet.Cells[6, 4] = limitations.MinDiff;
            //
            sheet.Range[sheet.Cells[1, 5], sheet.Cells[1, 6]].Merge();
            sheet.Cells[1, 5] = "Параметр метода";
            sheet.Cells[2, 5] = (nameof(exhaustiveSearchFactors.Eps) + "=");
            sheet.Cells[2, 6] = exhaustiveSearchFactors.Eps;
            sheet.Cells[3, 5] = (nameof(exhaustiveSearchFactors.Step) + "=");
            sheet.Cells[3, 6] = exhaustiveSearchFactors.Step;
            //
            application.Visible = true;
            application.ActiveWorkbook.SaveAs("Export.xlsx", Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
        public void Export()
        {
            

        }
        public Tuple<CorrectionFactors, Limitations, ExhaustiveSearchFactors,int> Import(string fileName) {
            CorrectionFactors correctionFactors = new CorrectionFactors();
            Limitations limitations = new Limitations();
            ExhaustiveSearchFactors exhaustiveSearchFactors = new ExhaustiveSearchFactors();

            Microsoft.Office.Interop.Excel.Application ObjWorkExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(fileName);
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1-й лист
            //
            correctionFactors.Alpfa = Convert.ToDouble(ObjWorkSheet.Cells[2, 2].Text.ToString());
            correctionFactors.Betta = Convert.ToDouble(ObjWorkSheet.Cells[3, 2].Text.ToString());
            correctionFactors.Mu= Convert.ToDouble(ObjWorkSheet.Cells[4, 2].Text.ToString());
            correctionFactors.Delta=Convert.ToDouble(ObjWorkSheet.Cells[5, 2].Text.ToString());
            correctionFactors.G= Convert.ToDouble(ObjWorkSheet.Cells[6, 2].Text.ToString());
            correctionFactors.A= Convert.ToDouble(ObjWorkSheet.Cells[7, 2].Text.ToString());
            correctionFactors.N = Convert.ToDouble(ObjWorkSheet.Cells[8, 2].Text.ToString());
            int tau = Convert.ToInt32(ObjWorkSheet.Cells[9, 2].Text.ToString());
            //
            limitations.MinT1= Convert.ToInt32(ObjWorkSheet.Cells[2, 4].Text.ToString());
            limitations.MaxT1 = Convert.ToInt32(ObjWorkSheet.Cells[3, 4].Text.ToString());
            limitations.MinT2 = Convert.ToInt32(ObjWorkSheet.Cells[4, 4].Text.ToString());
            limitations.MaxT2 = Convert.ToInt32(ObjWorkSheet.Cells[5, 4].Text.ToString());
            limitations.MinDiff = Convert.ToInt32(ObjWorkSheet.Cells[6, 4].Text.ToString());
            //
            exhaustiveSearchFactors.Eps= Convert.ToDouble(ObjWorkSheet.Cells[2, 6].Text.ToString());
            exhaustiveSearchFactors.Step= Convert.ToDouble(ObjWorkSheet.Cells[3, 6].Text.ToString());
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
           return new Tuple<CorrectionFactors, Limitations, ExhaustiveSearchFactors,int> ( correctionFactors, limitations, exhaustiveSearchFactors,tau);
        }

       
    }
}
