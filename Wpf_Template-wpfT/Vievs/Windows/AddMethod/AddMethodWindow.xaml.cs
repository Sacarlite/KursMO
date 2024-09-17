using VievModel.VievModels.AddMethodVievModel;

namespace Vievs.Windows.AddMethod
{
    /// <summary>
    /// Логика взаимодействия для AddMethodWindow.xaml
    /// </summary>
    public partial class AddMethodWindow : System.Windows.Window, IAddMethodWindow
    {
        public AddMethodWindow(IAddMethodVievModel addMethodVievModel)
        {
            InitializeComponent();
            DataContext = addMethodVievModel;
        }
    }
}
