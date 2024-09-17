using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Mvvm.Native;
using Domain.MethodsBD;
using Domain.Settings;
using OptimizationMathMethods;
using VievModel.Windows;

namespace VievModel.VievModels.AddMethodVievModel
{
    public partial class AddMethodVievModel
        : WindowVievModel<IAddMethodWindowMementoWrapper>,
            IAddMethodVievModel
    {
        private readonly IAddMethodWindowMementoWrapper addMethodWindowMementoWrapper;
        private IMethodsDatabaseLocator methodsDatabaseLocator;

        [ObservableProperty]
        ObservableCollection<Сlassification> types;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddMethodCommand))]
        Сlassification? selectedСlassification;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddMethodCommand))]
        string name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddMethodCommand))]
        private string description;

        [ObservableProperty]
        string methodDll;

        [ObservableProperty]
        Visibility showMethodButton;

        [ObservableProperty]
        Visibility showAddMethodButton;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddMethodCommand))]
        string methodPath;

        public AddMethodVievModel(
            IAddMethodWindowMementoWrapper addMethodWindowMementoWrapper,
            IMethodsDatabaseLocator methodsDatabaseLocator
        )
            : base(addMethodWindowMementoWrapper)
        {
            this.addMethodWindowMementoWrapper = addMethodWindowMementoWrapper;
            this.methodsDatabaseLocator = methodsDatabaseLocator;
            Types = methodsDatabaseLocator.Context.Сlasses.ToObservableCollection();
            ShowAddMethodButton = Visibility.Visible;
            ShowMethodButton = Visibility.Collapsed;
        }

        public event Action<Method?> AddNewMethod;
        public event Action WindowClosingAct;

        private bool CanExcuteAddMethodButton()
        {
            return !string.IsNullOrEmpty(Name)
                && !string.IsNullOrEmpty(Description)
                && !string.IsNullOrEmpty(MethodPath)
                && SelectedСlassification is not null;
        }

        [RelayCommand]
        void AddMethodPath()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "MyApp"; // Default file name
            dialog.DefaultExt = ".dll"; // Default file extension
            dialog.Filter = "(.dll)|*.dll"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            MethodPath = "";
            if (result == true)
            {
                // Open document
                MethodPath = dialog.FileName;
            }
            try
            {
                OptmizitationMethod optmizitationMethod = new OptmizitationMethod(MethodPath);
                MethodDll = Path.GetFileNameWithoutExtension(MethodPath);

                ShowAddMethodButton = Visibility.Collapsed;
                ShowMethodButton = Visibility.Visible;
                MessageBox.Show(
                    "Данные загружены",
                    "Open success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception e)
            {
                MethodPath = "";
                MessageBox.Show(
                    e.Message,
                    "Open error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        [RelayCommand]
        void Delete()
        {
            ShowAddMethodButton = Visibility.Visible;
            ShowMethodButton = Visibility.Collapsed;
            MethodPath = "";
        }

        [RelayCommand(CanExecute = nameof(CanExcuteAddMethodButton))]
        private void AddMethod()
        {
            Method method = new Method(Name, MethodPath, Description);
            method.Classification = SelectedСlassification;
            methodsDatabaseLocator.Context.Methods.Add(method);
            methodsDatabaseLocator.Context.SaveChanges();
            AddNewMethod.Invoke(method);
            WindowClosingAct.Invoke();
        }

        public void Dispose() { }
    }
}
