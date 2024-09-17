using System.Windows;

namespace Vievs.DialogWindows
{
    public partial class AddClassificationDialogWindow : System.Windows.Window
    {
        public AddClassificationDialogWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Name
        {
            get { return NameTextBox.Text; }
        }
    }
}
