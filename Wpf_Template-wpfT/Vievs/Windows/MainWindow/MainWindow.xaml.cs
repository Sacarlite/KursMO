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
using System.Windows.Shapes;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.MainWindow;
using Vievs.Windows.MainWindow;

namespace Vievs.MainWindow
{
    public partial class MainWindow :System.Windows.Window,IMainWindow
    {
        public MainWindow(IAdminMainVievModel adminMainWindowVievModel)
        {
            InitializeComponent();
            DataContext = adminMainWindowVievModel;
        }
        public MainWindow(IResearcherMainVievModel researcherMainWindowVievModel)
        {
            InitializeComponent();
            DataContext = researcherMainWindowVievModel;
        }
    }
}
