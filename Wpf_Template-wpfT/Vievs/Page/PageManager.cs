using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.PageVievModels;
using VievModels.Windows;
using Vievs.Pages;
using Vievs.Windows;

namespace Vievs.Page
{

    internal class PageManager : IPageManager
    {
        private readonly Dictionary<IPageVievModel, System.Windows.Controls.Page> _viewModelToWindowMap = new();
        private readonly IPageFactory _pageFactory;
        private readonly Dictionary<System.Windows.Controls.Page, IPageVievModel> _windowToViewModelMap = new();

        public PageManager(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public System.Windows.Controls.Page GetPage<TPageVievModel>(TPageVievModel pageVievModel) where TPageVievModel : IPageVievModel
        {
            if (_viewModelToWindowMap.TryGetValue(pageVievModel, out var page))
            {
                return page;
            }

            page = _pageFactory.Create(pageVievModel);

            _viewModelToWindowMap.Add(pageVievModel, page);
            _windowToViewModelMap.Add(page, pageVievModel);
            return page;
        }


    } 
}
