using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Factories;
using Domain.Settings;
using Domain.Version;
using OptimizationMathMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.ResearcherMainVievModel
{
    public partial class ResearcherMainVievModel : WindowVievModel<IMainWindowMementoWrapper>, IResearcherMainVievModel
    {
        private readonly IAplicationVersionProvider _aplicationVersionProvider;
        private readonly IWindowManager _windowManager;
        private IMainWindowMementoWrapper _windowMementoWrapper;
        public ResearcherMainVievModel(IMainWindowMementoWrapper mainWindowMementoWrapper,
             IWindowManager windowManager, IFactory<IMenuMainWindowVievModel> MenueMainWindowVievModelFactory,
             IAplicationVersionProvider aplicationVersionProvider)
             : base(mainWindowMementoWrapper)
        {
            MenuMainWindowVievModel = MenueMainWindowVievModelFactory.Create();
            _windowManager = windowManager;
            _aplicationVersionProvider = aplicationVersionProvider;
            _windowMementoWrapper = mainWindowMementoWrapper;
            correctionFactors = new CorrectionFactors();
            limitations = new Limitations();
            exhaustiveSearchFactors = new ExhaustiveSearchFactors();
        }

        public IMenuMainWindowVievModel MenuMainWindowVievModel { get; set; }
        [ObservableProperty]
        private CorrectionFactors correctionFactors;
        [ObservableProperty]
        private Limitations limitations;
        [ObservableProperty]
        private ExhaustiveSearchFactors exhaustiveSearchFactors;
        [ObservableProperty]
        private int tau;
        [ObservableProperty]
        private Point extraNum;
        [ObservableProperty]
        private Page visualizationFrame;
        [RelayCommand]
        private void Calculation()
        {

        }
        public string Version => $"Version {_aplicationVersionProvider.Version.ToString(3)}";

        public string Title => "OptKurs";

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
