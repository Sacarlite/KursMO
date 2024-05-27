using Bootstrapper.Common;
using Domain.Settings;
using Infrastructure.Settings.DefaultMementas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings.WindowWrappers
{
    internal class RegistrationWindowMementoWrapper : WindowMementoWrapper<RegistrationWindowMemento>, IRegistrationWindowMementoWrapper
    {
        public RegistrationWindowMementoWrapper(IPathService pathService) :
            base(pathService)
        {

        }
        protected override string MementoName => "RegistrationWindowMemento";
    }
}
