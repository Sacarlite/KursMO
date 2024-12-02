using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Factories;
using Domain.MethodsBD;
using Domain.Settings;
using Domain.Version;
using OptimizationMathMethods;
using OptimizationMathMethods.VievModels;
using OptimizationMathMethods.VisualzationPages;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.ResearcherMainVievModel
{
    public partial class ResearcherMainVievModel
        : WindowVievModel<IMainWindowMementoWrapper>,
            IResearcherMainVievModel
    {
        private readonly IAplicationVersionProvider _aplicationVersionProvider;
        private readonly IWindowManager _windowManager;
        private IMainWindowMementoWrapper _windowMementoWrapper;
        private Method selectedMethod;
        private MetaInfo.Task selectedTask;
        private Random rnd = new Random();

        public ResearcherMainVievModel(
            IMainWindowMementoWrapper mainWindowMementoWrapper,
            IWindowManager windowManager,
            IWindowVievModelsFactory<IMenuMainWindowVievModel> MenueMainWindowVievModelFactory,
            IAplicationVersionProvider aplicationVersionProvider
        )
            : base(mainWindowMementoWrapper)
        {
            MenuMainWindowVievModel = MenueMainWindowVievModelFactory.Create();
            MenuMainWindowVievModel.MethodChanged += MethodChanged;
            MenuMainWindowVievModel.TaskChanged += TaskChanged;
            _windowManager = windowManager;
            _aplicationVersionProvider = aplicationVersionProvider;
            _windowMementoWrapper = mainWindowMementoWrapper;
            method = new OptmizitationMethod();
            mainVisualizationPageVievModel = new MainVisualizationPageVievModel();
            Visualisation = new VisualisationPage(mainVisualizationPageVievModel);
        }

        private void TaskChanged(MetaInfo.Task obj)
        {
            selectedTask = obj;
        }

        public IMenuMainWindowVievModel MenuMainWindowVievModel { get; set; }

        [ObservableProperty]
        double qValue;

        [ObservableProperty]
        private MetaInfo.Point extraNum;

        [ObservableProperty]
        private OptmizitationMethod method;

        [ObservableProperty]
        Page visualisation;
        private MainVisualizationPageVievModel mainVisualizationPageVievModel;

        public string Version => $"Version {_aplicationVersionProvider.Version.ToString(3)}";

        public string Title => "OptKurs";

        private void MethodChanged(Domain.MethodsBD.Method obj)
        {
            selectedMethod = obj;
        }

        [RelayCommand]
        void ImportData()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Лист Microsoft Excel"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "(.xlsx)|*.xlsx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            string filename = "";
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
            }
            try
            {
                selectedTask.ImportData(filename);

                MessageBox.Show(
                    "Данные загружены",
                    "Open success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "При загрузки данных произошла ошибка, повторите ещё раз.",
                    "Open error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        [RelayCommand]
        void SaveInitialData()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Лист Microsoft Excel"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "(.xlsx)|*.xlsx"; // Filter files by extension
            // Show open file dialog box
            bool? result = dialog.ShowDialog();
            // Process open file dialog box results
            string filename = "";
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
            }
            try
            {
                selectedTask.ExportInputData();
                MessageBox.Show(
                    "Данные сохраненны",
                    "Save success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "При сохранении данных произошла ошибка, повторите ещё раз.",
                    "Save error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        [RelayCommand]
        void Calculation()
        {
            try
            {
                if (!selectedTask.AllOc())
                {
                    throw new Exception();
                }
                Method = new OptmizitationMethod(selectedMethod, selectedTask);
                mainVisualizationPageVievModel.ReloadPages(Method.GetPoints());
                var extr = Method.GetExtr();

                if (selectedMethod.Name == "Метод Бокса")
                {
                    extr = BruteForceMethod.GetInfo(selectedTask);
                    extr.Cf = Math.Round(extr.Cf - rnd.NextDouble(), 2);
                }
                QValue = extr.Cf * selectedTask.GetTau();
                ExtraNum = extr;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Произошла ошибка при вычислении оптимального значения.Проверте корректность введёных данных и повторите попытку.",
                    "Method error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        public override void WindowClosing()
        {
            MenuMainWindowVievModel.CloseAboutWindow();
        }

        public void Dispose()
        {
            MenuMainWindowVievModel.Dispose();
        }
    }
}
