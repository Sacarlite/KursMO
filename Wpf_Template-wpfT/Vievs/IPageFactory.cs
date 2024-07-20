using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.PageVievModels;
using VievModels.Windows;
using Vievs.Pages;
using Vievs.Windows;

namespace Vievs
{
    public interface IPageFactory
    {
        System.Windows.Controls.Page Create<TPageViewModel>(TPageViewModel viewModel)
       where TPageViewModel : IPageVievModel;
    }
}
