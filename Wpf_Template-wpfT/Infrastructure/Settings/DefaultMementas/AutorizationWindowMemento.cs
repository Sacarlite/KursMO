using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Infrastructure.Settings.DefaultMementas
{
    [DataContract]
    internal class AutorizationWindowMemento : WindowMemento
    {
        public AutorizationWindowMemento()
        {
            Left = 100;
            Top = 100;
            Width = 600;
            Height = 400;
            IsMaximized = false;
        }
    }
}
