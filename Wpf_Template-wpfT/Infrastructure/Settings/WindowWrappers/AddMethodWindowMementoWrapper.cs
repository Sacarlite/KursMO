using Bootstrapper.Common;
using Domain.Settings;
using Infrastructure.Settings.DefaultMementas;

namespace Infrastructure.Settings.WindowWrappers
{
    internal class AddMethodWindowMementoWrapper
        : WindowMementoWrapper<AddMethodWindowMemento>,
            IAddMethodWindowMementoWrapper
    {
        public AddMethodWindowMementoWrapper(IPathService pathService)
            : base(pathService) { }

        protected override string MementoName => "AddWindowWindowMemento";
    }
}
