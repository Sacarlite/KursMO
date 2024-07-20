using VievModel.PageVievModels;
using Vievs.Pages;

namespace Vievs.Page
{
    public interface IPageManager
    {
        System.Windows.Controls.Page GetPage<TPageVievModel>(TPageVievModel pageVievModel) where TPageVievModel : IPageVievModel;
    }
}