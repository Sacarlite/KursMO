using System.Runtime.Serialization;

namespace Infrastructure.Settings.DefaultMementas
{
    [DataContract]
    internal class AddMethodWindowMemento : WindowMemento
    {
        public AddMethodWindowMemento()
        {
            Left = 100;
            Top = 100;
            Width = 400;
            Height = 200;
            IsMaximized = true;
        }
    }
}
