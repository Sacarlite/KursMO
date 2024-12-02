using System.Windows.Controls;
using MetaInfo.TaskVisualization.Task1;

namespace MetaInfo
{
    public class Task1 : Task
    {
        private Task1ViewModel task1ViewModel = new Task1ViewModel();

        public override Page TaskPage
        {
            get => new TaskVisualization.Task1.ComponentPage(task1ViewModel);
        }

        public override double GetCalc(Point p)
        {
            return Math.Round(
                task1ViewModel.Alpfa
                    * task1ViewModel.G
                    * (
                        Math.Pow(p.T2 - task1ViewModel.Betta * task1ViewModel.A, task1ViewModel.N)
                        + task1ViewModel.Mu * Math.Exp(p.T2 + p.T1)
                        + task1ViewModel.Delta * (p.T2 - p.T1)
                    ),
                RoundCalc(GetEps())
            );
        }

        public override Tuple<double, double, double, double, double> GetFirstLimitations()
        {
            return new Tuple<double, double, double, double, double>(
                task1ViewModel.MinT1,
                task1ViewModel.MaxT1,
                task1ViewModel.MinT2,
                task1ViewModel.MaxT2,
                task1ViewModel.Step
            );
        }

        public override bool GetSecondLimitations(Point p)
        {
            return p.T2 - p.T1 >= task1ViewModel.MinDiff;
        }

        public override double GetEps()
        {
            return task1ViewModel.Eps;
        }

        public override bool GetExtrType()
        {
            return task1ViewModel.ExtrType;
        }

        public override bool AllOc()
        {
            return task1ViewModel.Validation();
        }

        public override double GetTau()
        {
            return task1ViewModel.Tau;
        }

        public override void ExportInputData()
        {
            Microsoft.Office.Interop.Excel.Application application =
                new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet sheet =
                (Microsoft.Office.Interop.Excel.Worksheet)application.Sheets[1];
            sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 2]].Merge();
            sheet.Columns[2].ColumnWidth = 30;
            sheet.Cells[1, 1] = "Параметры и множители";
            sheet.Cells[2, 1] = (nameof(task1ViewModel.Alpfa) + "=");
            sheet.Cells[2, 2] = task1ViewModel.Alpfa;
            sheet.Cells[3, 1] = (nameof(task1ViewModel.Betta) + "=");
            sheet.Cells[3, 2] = task1ViewModel.Betta;
            sheet.Cells[4, 1] = (nameof(task1ViewModel.Mu) + "=");
            sheet.Cells[4, 2] = task1ViewModel.Mu;
            sheet.Cells[5, 1] = (nameof(task1ViewModel.Delta) + "=");
            sheet.Cells[5, 2] = task1ViewModel.Delta;
            sheet.Cells[6, 1] = (nameof(task1ViewModel.G) + "=");
            sheet.Cells[6, 2] = task1ViewModel.G;
            sheet.Cells[7, 1] = (nameof(task1ViewModel.A) + "=");
            sheet.Cells[7, 2] = task1ViewModel.A;
            sheet.Cells[8, 1] = (nameof(task1ViewModel.N) + "=");
            sheet.Cells[8, 2] = task1ViewModel.N;
            sheet.Cells[9, 1] = (nameof(task1ViewModel.Tau) + "=");
            sheet.Cells[9, 2] = task1ViewModel.Tau;
            //
            sheet.Range[sheet.Cells[1, 3], sheet.Cells[1, 4]].Merge();
            sheet.Cells[1, 3] = "Ограничения";
            sheet.Cells[2, 3] = (nameof(task1ViewModel.MinT1) + "=");
            sheet.Cells[2, 4] = task1ViewModel.MinT1;
            sheet.Cells[3, 3] = (nameof(task1ViewModel.MaxT1) + "=");
            sheet.Cells[3, 4] = task1ViewModel.MaxT1;
            sheet.Cells[4, 3] = (nameof(task1ViewModel.MinT2) + "=");
            sheet.Cells[4, 4] = task1ViewModel.MinT2;
            sheet.Cells[5, 3] = (nameof(task1ViewModel.MaxT2) + "=");
            sheet.Cells[5, 4] = task1ViewModel.MaxT2;
            sheet.Cells[6, 3] = (nameof(task1ViewModel.MinDiff) + "=");
            sheet.Cells[6, 4] = task1ViewModel.MinDiff;
            //
            sheet.Range[sheet.Cells[1, 5], sheet.Cells[1, 6]].Merge();
            sheet.Cells[1, 5] = "Параметр метода";
            sheet.Cells[2, 5] = (nameof(task1ViewModel.Eps) + "=");
            sheet.Cells[2, 6] = task1ViewModel.Eps;
            sheet.Cells[3, 5] = (nameof(task1ViewModel.Step) + "=");
            sheet.Cells[3, 6] = task1ViewModel.Step;
            //
            application.Visible = true;
            application.ActiveWorkbook.SaveAs(
                "Export.xlsx",
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing
            );
        }

        public override void ImportData(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application ObjWorkExcel =
                new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(
                fileName
            );
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet =
                (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1-й лист
            //
            task1ViewModel.Alpfa = Convert.ToDouble(ObjWorkSheet.Cells[2, 2].Text.ToString());
            task1ViewModel.Betta = Convert.ToDouble(ObjWorkSheet.Cells[3, 2].Text.ToString());
            task1ViewModel.Mu = Convert.ToDouble(ObjWorkSheet.Cells[4, 2].Text.ToString());
            task1ViewModel.Delta = Convert.ToDouble(ObjWorkSheet.Cells[5, 2].Text.ToString());
            task1ViewModel.G = Convert.ToDouble(ObjWorkSheet.Cells[6, 2].Text.ToString());
            task1ViewModel.A = Convert.ToDouble(ObjWorkSheet.Cells[7, 2].Text.ToString());
            task1ViewModel.N = Convert.ToDouble(ObjWorkSheet.Cells[8, 2].Text.ToString());
            task1ViewModel.Tau = Convert.ToDouble(ObjWorkSheet.Cells[9, 2].Text.ToString());
            //
            task1ViewModel.MinT1 = Convert.ToDouble(ObjWorkSheet.Cells[2, 4].Text.ToString());
            task1ViewModel.MaxT1 = Convert.ToDouble(ObjWorkSheet.Cells[3, 4].Text.ToString());
            task1ViewModel.MinT2 = Convert.ToDouble(ObjWorkSheet.Cells[4, 4].Text.ToString());
            task1ViewModel.MaxT2 = Convert.ToDouble(ObjWorkSheet.Cells[5, 4].Text.ToString());
            task1ViewModel.MinDiff = Convert.ToDouble(ObjWorkSheet.Cells[6, 4].Text.ToString());
            //
            task1ViewModel.Eps = Convert.ToDouble(ObjWorkSheet.Cells[2, 6].Text.ToString());
            task1ViewModel.Step = Convert.ToDouble(ObjWorkSheet.Cells[3, 6].Text.ToString());
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
        }

        public new string LatexForm
        {
            get
            {
                return "S=\\alpha\\cdot G\\cdot ((T_2-\\beta\\cdot A)^2+(\\mu\\cdot e^{(T_1+T_2)})^N+\\Delta\\cdot(T_2-T_1)) ";
            }
        }
    }
}
