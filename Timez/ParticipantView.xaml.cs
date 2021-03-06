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
    /// Interaction logic for ParticipantView.xaml
    /// </summary>
    public partial class ParticipantView : UserControl
    {
        public ParticipantView()
        {
            InitializeComponent();
        }

        Point _position;

        public Point Position {
            set {
                _position = value;
                var cx = Width / 2; // (double)Circle.GetValue(Canvas.LeftProperty);
                var cy = Height / 2; // (double)Circle.GetValue(Canvas.TopProperty);

                this.SetValue(Canvas.LeftProperty, value.X - cx);
                this.SetValue(Canvas.TopProperty, value.Y - cy);
            }
            get {
                return _position;
            }
        }
    }
}
