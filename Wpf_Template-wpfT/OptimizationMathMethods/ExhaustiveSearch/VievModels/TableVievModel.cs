using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationMathMethods.ExhaustiveSearch.VievModels
{
    public partial class TableVievModel:ObservableObject
    {
        private readonly List<List<Point>> points;
        [ObservableProperty]
        DataTable pointsTable;

        public TableVievModel(List<List<Point>>points)
        {
            if (points != null)
            {
                this.points = points;
            }
            else
            {
                points = new List<List<Point>>() { };
            }
            GetTable();
        }

        private void GetTable()
        {
            pointsTable=new DataTable();
            pointsTable.Columns.Add("", typeof(string));
            foreach (var elem in points)
            {
                    pointsTable.Columns.Add($"T1 = {elem[0].T1}",typeof(string));               
            }

            for (var i = 0; i < pointsTable.Columns.Count; i++)
            {
                bool flag = true;
                List<object> tmp = new List<object>();
                for (var j = -1; j < pointsTable.Columns.Count; j++)
                {
                    if(flag) {
                        tmp.Add($"T2 = {points[j+1][i].T2}");
                        flag = false;
                    }
                    else
                    {
                        tmp.Add($" {points[j + 1][i].T2}");
                    }
                }
                pointsTable.Rows.Add(tmp);
            }
        }
    }
}
