using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.ExelExplorer;
using Domain.Factories;
using Domain.MethodsBD;
using Domain.Settings;
using Domain.Version;
using MetaInfo;
using OptimizationMathMethods;
using OptimizationMathMethods.VievModels;
using OptimizationMathMethods.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.ResearcherMainVievModel
{
    public partial class ResearcherMainVievModel : WindowVievModel<IMainWindowMementoWrapper>, IResearcherMainVievModel
    {
        private readonly IAplicationVersionProvider _aplicationVersionProvider;
        private IExelExplorer exelExplorer;
        private readonly IWindowManager _windowManager;
        private IMainWindowMementoWrapper _windowMementoWrapper;
        private Method selectedMethod;
        public ResearcherMainVievModel(IMainWindowMementoWrapper mainWindowMementoWrapper,
             IWindowManager windowManager, IWindowVievModelsFactory<IMenuMainWindowVievModel> MenueMainWindowVievModelFactory,
             IAplicationVersionProvider aplicationVersionProvider,IExelExplorer exelExplorer)
             : base(mainWindowMementoWrapper)
        {
            MenuMainWindowVievModel = MenueMainWindowVievModelFactory.Create();
            MenuMainWindowVievModel.MethodChanged += MethodChanged;
            _windowManager = windowManager;
            _aplicationVersionProvider = aplicationVersionProvider;
            this.exelExplorer = exelExplorer;
            _windowMementoWrapper = mainWindowMementoWrapper;
            correctionFactors = new CorrectionFactors();
            limitations = new Limitations();
            exhaustiveSearchFactors = new ExhaustiveSearchFactors();
            method = new OptmizitationMethod();
            ExtrType = true;
            mainVisualizationPageVievModel = new MainVisualizationPageVievModel();
            Visualisation = new VisualisationPage(mainVisualizationPageVievModel);
        }
        public IMenuMainWindowVievModel MenuMainWindowVievModel { get; set; }
        [ObservableProperty]
        double qValue;
        [ObservableProperty]
        private bool extrType;
        [ObservableProperty]
        private CorrectionFactors correctionFactors;
        [ObservableProperty]
        private Limitations limitations;
        [ObservableProperty]
        private ExhaustiveSearchFactors exhaustiveSearchFactors;
        [ObservableProperty]
        private int tau;
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
            selectedMethod=obj;
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
            string filename="";
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

            }
            try {
                var data= exelExplorer.Import(filename);
                CorrectionFactors= data.Item1;
                Limitations= data.Item2;
                ExhaustiveSearchFactors= data.Item3;
                Tau = data.Item4;
                MessageBox.Show("Данные загружены", "Open success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception) {
                MessageBox.Show("При загрузки данных произошла ошибка, повторите ещё раз.", "Open error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                exelExplorer.ExportInputData(CorrectionFactors, Limitations, ExhaustiveSearchFactors, Tau, filename);
                MessageBox.Show("Данные сохраненны", "Save success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("При сохранении данных произошла ошибка, повторите ещё раз.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        [RelayCommand]
        void Calculation()
        {
            try
            {
                if (selectedMethod is null || CorrectionFactors is null || Limitations is null || ExhaustiveSearchFactors is null)
                {
                    throw new Exception();
                }
                Method = new OptmizitationMethod(selectedMethod, CorrectionFactors, Limitations, ExhaustiveSearchFactors);
                mainVisualizationPageVievModel.ReloadPages(Method.GetPoints());
                var extr = Method.GetExtr();
                if (ExtrType)
                {
                    QValue = extr.Item1.Cf * Tau;
                    ExtraNum = extr.Item1;
                }
                else
                {
                    QValue = extr.Item2.Cf * Tau;
                    ExtraNum = extr.Item2;
                }
            }
            catch(Exception) {
                MessageBox.Show("Произошла ошибка при вычислении оптимального значения.Проверте корректность введёных данных и повторите попытку.", "Method error", MessageBoxButton.OK, MessageBoxImage.Error);
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
