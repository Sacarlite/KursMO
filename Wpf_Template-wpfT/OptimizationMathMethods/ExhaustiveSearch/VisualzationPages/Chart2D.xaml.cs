using ChartDirector;
using OptimizationMathMethods.ExhaustiveSearch.VievModels;
using System;
using System.Collections.Generic;
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

namespace OptimizationMathMethods.ExhaustiveSearch.VisualzationPages
{
    /// <summary>
    /// Логика взаимодействия для Chart2D.xaml
    /// </summary>
    public partial class Chart2D : Page
    {
     
        //Data
        private List<double> dataX;
        private List<double> dataY;
        private List<double> dataZ;
     
        public Chart2D(List<List<Point>> points)
        {
            InitializeComponent();
            if (points != null)
            {
                Points = points;
            }
            else
            {
                points=new List<List<Point>>() { };
            }
        }

        private readonly List<List<Point>> Points;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // The (x, y, z) coordinates of the scattered data
            dataX = new List<double>();
            dataY = new List<double>();
            dataZ = new List<double>();
            foreach (var elem in Points)
            {
                foreach (var point in elem)
                {
                    dataX.Add(point.T1);
                    dataY.Add(point.T2);
                    dataZ.Add(point.Cf);
                }
            }

            // Draw the chart
            WPFChartViewer1.updateViewPort(true, false);
        }
        //
        // The ViewPortChanged event handler
        //
        private void WPFChartViewer1_ViewPortChanged(object sender, WPFViewPortEventArgs e)
        {
            // Update the chart if necessary
            if (e.NeedUpdateChart)
                drawChart((WPFChartViewer)sender);
        }
        public void drawChart(WPFChartViewer viewer)
        {
            // Create a XYChart object of size 450 x 540 pixels
            XYChart _chart = new XYChart(450, 540);

            // Add a title to the chart using 15 points Arial Italic font
            _chart.addTitle("      Contour Chart with Scattered Data", "Arial Italic", 15);

            // Set the plotarea at (65, 40) and of size 360 x 360 pixels. Use semi-transparent black
            // (c0000000) for both horizontal and vertical grid lines
            _chart.setPlotArea(65, 40, 360, 360, -1, -1, -1, unchecked((int)0xc0000000), -1);

            // Set x-axis and y-axis title using 12 points Arial Bold Italic font
            _chart.xAxis().setTitle("T1-Axis Title Place Holder", "Arial Bold Italic", 10);
            _chart.yAxis().setTitle("T2-Axis Title Place Holder", "Arial Bold Italic", 10);

            // Set x-axis and y-axis labels to use Arial Bold font
            _chart.xAxis().setLabelStyle("Arial Bold");
            _chart.yAxis().setLabelStyle("Arial Bold");

            // When x-axis and y-axis color to transparent
            _chart.xAxis().setColors(Chart.Transparent);
            _chart.yAxis().setColors(Chart.Transparent);

            // Add a scatter layer to the chart to show the position of the data points. Disable the
            // image map for the scatter layer. We will use the contour layer to provide the
            // tooltip.
            _chart.addScatterLayer(dataX.ToArray(), dataY.ToArray(), "", Chart.Cross2Shape(0.2), 7, 0x000000
                ).setHTMLImageMap("{disable}");

            // Add a contour layer using the given data
            ContourLayer layer = _chart.addContourLayer(dataX.ToArray(), dataY.ToArray(), dataZ.ToArray());

            // Move the grid lines in front of the contour layer
            _chart.getPlotArea().moveGridBefore(layer);

            // Add a color axis (the legend) in which the top center is anchored at (245, 455). Set
            // the length to 330 pixels and the labels on the top side.
            ColorAxis cAxis = layer.setColorAxis(245, 455, Chart.TopCenter, 330, Chart.Top);

            // Add a bounding box to the color axis using the default line color as border.
            cAxis.setBoundingBox(Chart.Transparent, Chart.LineColor);

            // Add a title to the color axis using 12 points Arial Bold Italic font
            cAxis.setTitle("Color Legend Title Place Holder", "Arial Bold Italic", 10);

            // Set color axis labels to use Arial Bold font
            cAxis.setLabelStyle("Arial Bold");

            // Set the color axis range as 0 to 20, with a step every 2 units
            cAxis.setLinearScale(0, 20, 2);
            // Output the chart
            viewer.Chart = _chart;

            //include tool tip for the chart
            viewer.ImageMap = _chart.getHTMLImageMap("", "",
                "title='<*cdml*>X: {x|2}<*br*>Y: {y|2}<*br*>Z: {z|2}'");
        }
     
    }
}
