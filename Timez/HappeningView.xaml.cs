using System;
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

        public Happening Happening {
            get => this.DataContext as Happening;
            set => this.DataContext = value;
        }

        Point _position;

        public Point Position {
            set {
                _position = value;
                var cx = Width / 2; // (double)Circle.GetValue(Canvas.LeftProperty);
                var cy = 10; // (double)Circle.GetValue(Canvas.TopProperty);

                this.SetValue(Canvas.LeftProperty, value.X - cx);
                this.SetValue(Canvas.TopProperty, value.Y - cy);
            }
            get {
                return _position;
            }
        }
    }
}
