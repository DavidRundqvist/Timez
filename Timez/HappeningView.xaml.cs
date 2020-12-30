﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Timez
{
    /// <summary>
    /// Interaction logic for HappeningView.xaml
    /// </summary>
    public partial class HappeningView : UserControl
    {
        public HappeningView()
        {
            InitializeComponent();
        }

        public HappeningViewModel Happening {
            get => this.DataContext as HappeningViewModel;
            set => this.DataContext = value;
        }



    }
}
