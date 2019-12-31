using System;
using System.Collections.Generic;
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

namespace Timez
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GraphController _controller;

        public MainWindow()
        {
            InitializeComponent();

            var data = TestData.CreateTestData();
            _controller = new GraphController(data, _grid);

            Loaded += MainWindow_Loaded;
            _grid.MouseWheel += _grid_MouseWheel;
            _grid.MouseDown += _grid_MouseDown;
            _grid.MouseUp += _grid_MouseUp;
            _grid.MouseMove += _grid_MouseMove;

        }


        Point? _mouseDown;
        private void _grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDown = e.GetPosition(_scroller);
        }

        private void _grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown.HasValue)
            {
                var newPosition = e.GetPosition(_scroller);
                var delta = newPosition - _mouseDown.Value;
                _mouseDown = newPosition;

                _scroller.ScrollToHorizontalOffset(_scroller.HorizontalOffset - delta.X);
            }
        }

        private void _grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseDown = null;
        }



        private void _grid_MouseWheel(object sender, MouseWheelEventArgs e) {

            var relativeOffset = Mouse.GetPosition(_grid).X / _grid.Width;

            _controller.ChangeScale(e.Delta > 0 ? 1.1f : 0.9f);



            var mouseRelativeToScroller = Mouse.GetPosition(_scroller).X;

            var newOffset = relativeOffset * _grid.Width - mouseRelativeToScroller;
            _scroller.ScrollToHorizontalOffset(newOffset);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.UpdateTarget();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _controller.UpdateTarget();
        }
    }
}
