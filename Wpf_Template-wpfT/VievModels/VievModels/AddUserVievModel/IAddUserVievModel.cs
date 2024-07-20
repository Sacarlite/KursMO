﻿using Domain.UserEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModels.Windows;

namespace VievModel.VievModels.AddUserVievModel
{
    public interface IAddUserVievModel : IWindowViewModel, IDisposable
    {
        event Action<IEventArgs> AddNewUser;
        event Action WindowClosingAct;
            }
}
