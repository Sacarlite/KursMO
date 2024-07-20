using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings.DefaultMementas
{
    [DataContract]
    internal class AddUserWindowMemento : WindowMemento
    {
        public AddUserWindowMemento()
        {
            Left = 100;
            Top = 100;
            Width = 600;
            Height = 400;
            IsMaximized = false;
        }

    }
}
