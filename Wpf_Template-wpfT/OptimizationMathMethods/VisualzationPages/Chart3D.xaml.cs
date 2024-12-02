using System.Windows.Controls;
using System.Windows.Input;
using ChartDirector;

namespace OptimizationMathMethods.VisualzationPages
{
    /// <summary>
    /// Логика взаимодействия для Chart3D.xaml
    /// </summary>
    public partial class Chart3D : Page
    {
        // 3D view angles
        private double m_elevationAngle;
        private double m_rotationAngle;

        //Data
        private List<double> dataX;
        private List<double> dataY;
        private List<double> dataZ;

        // Keep track of mouse drag
        private int m_lastMouseX;
        private int m_lastMouseY;
        private bool m_isDragging;

        public Chart3D(List<List<MetaInfo.Point>> points)
        {
            InitializeComponent();
            if (points != null)
            {
                Points = points;
            }
            else
            {
                points = new List<List<MetaInfo.Point>>() { };
            }
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
            // 3D view angles
            m_elevationAngle = 30;
            m_rotationAngle = 45;

            // Keep track of mouse drag
            m_isDragging = false;
            m_lastMouseX = -1;
            m_lastMouseY = -1;

            // Draw the chart
            WPFChartViewer1.updateViewPort(true, false);
        }

        private readonly List<List<MetaInfo.Point>> Points;

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
            // Create a SurfaceChart object of size 720 x 600 pixels
            SurfaceChart c = new SurfaceChart(720, 600);

            // Set the center of the plot region at (330, 290), and set width x depth x height to
            // 360 x 360 x 270 pixels
            c.setPlotRegion(330, 290, 360, 360, 270);

            // Set the data to use to plot the chart
            c.setData(dataX.ToArray(), dataY.ToArray(), dataZ.ToArray());

            // Spline interpolate data to a 80 x 80 grid for a smooth surface
            c.setInterpolation(80, 80);

            // Set the view angles
            c.setViewAngle(m_elevationAngle, m_rotationAngle);

            // Check if draw frame only during rotation
            if (m_isDragging)
                c.setShadingMode(Chart.RectangularFrame);

            // Add a color axis (the legend) in which the left center is anchored at (660, 270). Set
            // the length to 200 pixels and the labels on the right side.
            c.setColorAxis(650, 270, Chart.Left, 200, Chart.Right);

            // Set the x, y and z axis titles using 10 points Arial Bold font
            c.xAxis().setTitle("T1", "Arial Bold", 15);
            c.yAxis().setTitle("T2", "Arial Bold", 15);

            // Set axis label font
            c.xAxis().setLabelStyle("Arial", 10);
            c.yAxis().setLabelStyle("Arial", 10);
            c.zAxis().setLabelStyle("Arial", 10);
            c.colorAxis().setLabelStyle("Arial", 10);

            // Output the chart
            viewer.Chart = c;
        }

        //
        // Draw track cursor when mouse is moving over plotarea
        //
        private void WPFChartViewer1_MouseMoveChart(object sender, MouseEventArgs e)
        {
            int mouseX = WPFChartViewer1.ChartMouseX;
            int mouseY = WPFChartViewer1.ChartMouseY;

            // Drag occurs if mouse button is down and the mouse is captured by the m_ChartViewer
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (m_isDragging)
                {
                    // The chart is configured to rotate by 90 degrees when the mouse moves from
                    // left to right, which is the plot region width (360 pixels). Similarly, the
                    // elevation changes by 90 degrees when the mouse moves from top to buttom,
                    // which is the plot region height (270 pixels).
                    m_rotationAngle += (m_lastMouseX - mouseX) * 90.0 / 360;
                    m_elevationAngle += (mouseY - m_lastMouseY) * 90.0 / 270;
                    WPFChartViewer1.updateViewPort(true, false);
                }

                // Keep track of the last mouse position
                m_lastMouseX = mouseX;
                m_lastMouseY = mouseY;
                m_isDragging = true;
            }
        }

        private void WPFChartViewer1_MouseUpChart(object sender, MouseEventArgs e)
        {
            m_isDragging = false;
            WPFChartViewer1.updateViewPort(true, false);
        }
    }
}
