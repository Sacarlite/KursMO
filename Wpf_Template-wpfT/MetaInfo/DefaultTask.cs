using System.Windows.Controls;
using MetaInfo.TaskVisualization.Task1;

namespace MetaInfo
{
    public class DefaultTask : Task
    {
        public override Page TaskPage => new TaskVisualization.DefaultTask.ComponentPage();
        public new string LatexForm
        {
            get { return "-"; }
        }

        public override bool AllOc()
        {
            return false;
        }

        public override void ExportInputData()
        {
            throw new NotImplementedException();
        }

        public override double GetCalc(Point p)
        {
            throw new NotImplementedException();
        }

        public override double GetEps()
        {
            throw new NotImplementedException();
        }

        public override bool GetExtrType()
        {
            throw new NotImplementedException();
        }

        public override Tuple<double, double, double, double, double> GetFirstLimitations()
        {
            throw new NotImplementedException();
        }

        public override bool GetSecondLimitations(Point p)
        {
            throw new NotImplementedException();
        }

        public override double GetTau()
        {
            throw new NotImplementedException();
        }
        public override double GetStep()
        {
            throw new NotImplementedException();
        }

        public override void ImportData(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
