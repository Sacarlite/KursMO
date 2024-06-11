﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VievModel.PageVievModels;

namespace Vievs.Pages.AdminsListPage
{
    /// <summary>
    /// Логика взаимодействия для AdminsListPage.xaml
    /// </summary>
    public partial class AdminsListPage : Page, IAdminsListPage
    {
        public AdminsListPage(IAdminPageVievModel adminPageVievModel)
        {
            InitializeComponent();
            DataContext = adminPageVievModel;
        }
    }
}