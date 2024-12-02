using System.Windows.Controls;
using MetaInfo.TaskVisualization.Task2;

namespace MetaInfo
{
    public class Task2 : Task
    {
        private Task2ViewModel task2ViewModel = new Task2ViewModel();
        public override Page TaskPage
        {
            get => new TaskVisualization.Task2.ComponentPage(task2ViewModel);
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
            sheet.Cells[2, 1] = (nameof(task2ViewModel.Alpfa) + "=");
            sheet.Cells[2, 2] = task2ViewModel.Alpfa;
            sheet.Cells[3, 1] = (nameof(task2ViewModel.Betta) + "=");
            sheet.Cells[3, 2] = task2ViewModel.Betta;
            sheet.Cells[4, 1] = (nameof(task2ViewModel.Gamma) + "=");
            sheet.Cells[4, 2] = task2ViewModel.Gamma;
            sheet.Cells[5, 1] = (nameof(task2ViewModel.A1) + "=");
            sheet.Cells[5, 2] = task2ViewModel.A1;
            sheet.Cells[6, 1] = (nameof(task2ViewModel.A2) + "=");
            sheet.Cells[6, 2] = task2ViewModel.A2;
            sheet.Cells[7, 1] = (nameof(task2ViewModel.V1) + "=");
            sheet.Cells[7, 2] = task2ViewModel.V1;
            sheet.Cells[8, 1] = (nameof(task2ViewModel.V2) + "=");
            sheet.Cells[8, 2] = task2ViewModel.V2;
            sheet.Cells[9, 1] = (nameof(task2ViewModel.Tau) + "=");
            sheet.Cells[9, 2] = task2ViewModel.Tau;
            //
            sheet.Range[sheet.Cells[1, 3], sheet.Cells[1, 4]].Merge();
            sheet.Cells[1, 3] = "Ограничения";
            sheet.Cells[2, 3] = (nameof(task2ViewModel.MinT1) + "=");
            sheet.Cells[2, 4] = task2ViewModel.MinT1;
            sheet.Cells[3, 3] = (nameof(task2ViewModel.MaxT1) + "=");
            sheet.Cells[3, 4] = task2ViewModel.MaxT1;
            sheet.Cells[4, 3] = (nameof(task2ViewModel.MinT2) + "=");
            sheet.Cells[4, 4] = task2ViewModel.MinT2;
            sheet.Cells[5, 3] = (nameof(task2ViewModel.MaxT2) + "=");
            sheet.Cells[5, 4] = task2ViewModel.MaxT2;
            sheet.Cells[6, 3] = (nameof(task2ViewModel.MaxSumm) + "=");
            sheet.Cells[6, 4] = task2ViewModel.MaxSumm;
            //
            sheet.Range[sheet.Cells[1, 5], sheet.Cells[1, 6]].Merge();
            sheet.Cells[1, 5] = "Параметр метода";
            sheet.Cells[2, 5] = (nameof(task2ViewModel.Eps) + "=");
            sheet.Cells[2, 6] = task2ViewModel.Eps;
            sheet.Cells[3, 5] = (nameof(task2ViewModel.Step) + "=");
            sheet.Cells[3, 6] = task2ViewModel.Step;
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

        public override double GetCalc(Point p)
        {
            return Math.Round(
                task2ViewModel.Alpfa * Math.Pow(p.T2 - p.T1, task2ViewModel.A1)
                    + task2ViewModel.Betta
                        * 1
                        / task2ViewModel.V1
                        * Math.Pow(
                            p.T1 + p.T2 - task2ViewModel.Gamma * task2ViewModel.V2,
                            task2ViewModel.A2
                        ),
                RoundCalc(GetEps())
            );
        }

        public override double GetEps()
        {
            return task2ViewModel.Eps;
        }

        public override bool GetExtrType()
        {
            return task2ViewModel.ExtrType;
        }

        public override Tuple<double, double, double, double, double> GetFirstLimitations()
        {
            ;
            return new Tuple<double, double, double, double, double>(
                task2ViewModel.MinT1,
                task2ViewModel.MaxT1,
                task2ViewModel.MinT2,
                task2ViewModel.MaxT2,
                task2ViewModel.Step
            );
        }

        public new string LatexForm
        {
            get
            {
                return "S=\\alpha\\cdot  (T_2- T_1)^{A1}+ \\beta\\cdot1/V1\\cdot(T_1+T_2-\\gamma\\cdot V2)^{A2} ";
            }
        }

        public override bool GetSecondLimitations(Point p)
        {
            return p.T2 + p.T1 <= task2ViewModel.MaxSumm;
        }

        public override bool AllOc()
        {
            return task2ViewModel.Validation();
        }

        public override double GetTau()
        {
            return task2ViewModel.Tau;
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
            task2ViewModel.Alpfa = Convert.ToDouble(ObjWorkSheet.Cells[2, 2].Text.ToString());
            task2ViewModel.Betta = Convert.ToDouble(ObjWorkSheet.Cells[3, 2].Text.ToString());
            task2ViewModel.Gamma = Convert.ToDouble(ObjWorkSheet.Cells[4, 2].Text.ToString());

            task2ViewModel.A1 = Convert.ToDouble(ObjWorkSheet.Cells[5, 2].Text.ToString());
            task2ViewModel.A2 = Convert.ToDouble(ObjWorkSheet.Cells[6, 2].Text.ToString());
            task2ViewModel.V1 = Convert.ToDouble(ObjWorkSheet.Cells[7, 2].Text.ToString());
            task2ViewModel.V2 = Convert.ToDouble(ObjWorkSheet.Cells[8, 2].Text.ToString());
            task2ViewModel.Tau = Convert.ToDouble(ObjWorkSheet.Cells[9, 2].Text.ToString());
            //
            task2ViewModel.MinT1 = Convert.ToDouble(ObjWorkSheet.Cells[2, 4].Text.ToString());
            task2ViewModel.MaxT1 = Convert.ToDouble(ObjWorkSheet.Cells[3, 4].Text.ToString());
            task2ViewModel.MinT2 = Convert.ToDouble(ObjWorkSheet.Cells[4, 4].Text.ToString());
            task2ViewModel.MaxT2 = Convert.ToDouble(ObjWorkSheet.Cells[5, 4].Text.ToString());
            task2ViewModel.MaxSumm = Convert.ToDouble(ObjWorkSheet.Cells[6, 4].Text.ToString());
            //
            task2ViewModel.Eps = Convert.ToDouble(ObjWorkSheet.Cells[2, 6].Text.ToString());
            task2ViewModel.Step = Convert.ToDouble(ObjWorkSheet.Cells[3, 6].Text.ToString());
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
        }
    }
}
