using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Factories;
using Domain.Settings;
using Domain.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public IMenuMainWindowVievModel MenuMainWindowVievModel { get; set; }


        public string Version => $"Version {_aplicationVersionProvider.Version.ToString(3)}";

     
        public string Title => "TODO YorTitle";

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
