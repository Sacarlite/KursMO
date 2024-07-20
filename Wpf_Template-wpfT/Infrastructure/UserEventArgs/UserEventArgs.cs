using Domain.UserBd;
using Domain.UserEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserEventArgs
{
    public class UserEventArgs :  IEventArgs
    {
        private User _user;
        public UserEventArgs(User user) {
            _user=user;
        }
        public User? User { get { return _user; } }
    }
}
