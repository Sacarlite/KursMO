using ChartDirector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BruteForceMethod.VisualizationPages
{
    /// <summary>
    /// Логика взаимодействия для PointTable.xaml
    /// </summary>
    public partial class PointTable : Page
    {
        public PointTable(List<List<MetaInfo.Point>> points)
        {
            InitializeComponent();
            Points = points;

        }
        private readonly List<List<MetaInfo.Point>> Points;
        private void Activate(object sender, RoutedEventArgs e)
        {
            var dt = new DataTable();
            DataColumn t2Column = new DataColumn("-", Type.GetType("System.Decimal"));
            dt.Columns.Add(t2Column);
            for (int i=0;i< Points.Count;i++)
            {
                DataColumn t1Column = new DataColumn(Points[i][0].T1.ToString(), Type.GetType("System.Decimal"));
                dt.Columns.Add(t1Column);
            }
            for (int i = 0; i < Points.Count; i++)
            {
                List<object> pointList = new List<object>();   
                for (int j = 0; j < Points[i].Count; j++)
                {
                    if (j == 0)
                    {
                        pointList.Add(Points[j][i].T2);
                    }
                    pointList.Add(Points[j][i].Cf);
                }
                DataRow row = dt.NewRow();
                row.ItemArray = pointList.ToArray();
                dt.Rows.Add(row);
            }
        }
    }
}
