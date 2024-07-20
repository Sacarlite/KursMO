using Domain.UserBd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserEventArgs
{
    public interface IEventArgs
    {
        User? User { get; }

    }
}
