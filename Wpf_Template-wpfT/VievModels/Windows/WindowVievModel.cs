using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Mvvm.UI.ModuleInjection;
using Domain.Settings;
using System;
using VievModels.Windows;

namespace VievModel.Windows;

public abstract class WindowVievModel<TWindowMementoWrapper> : ObservableObject, IWindowViewModel
where TWindowMementoWrapper : class, IWindowMementoWrapper
{
    private readonly TWindowMementoWrapper _windowMementoWrapper;

    protected WindowVievModel(TWindowMementoWrapper windowMementoWrapper)
    {
        _windowMementoWrapper = windowMementoWrapper;

    }
    public double Left
    {
        get => _windowMementoWrapper.Left;
        set => _windowMementoWrapper.Left = value;
    }


    public double Top
    {
        get => _windowMementoWrapper.Top;
        set => _windowMementoWrapper.Top = value;
    }

    public double Width
    {
        get => _windowMementoWrapper.Width;
        set => _windowMementoWrapper.Width = value;
    }

    public double Height
    {
        get => _windowMementoWrapper.Height;
        set => _windowMementoWrapper.Height = value;
    }

    public bool IsMaximized
    {
        get => _windowMementoWrapper.IsMaximized;
        set => _windowMementoWrapper.IsMaximized = value;
    }

    public virtual void WindowClosing()
    {

    }
}