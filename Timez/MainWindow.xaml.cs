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
            _controller = new GraphController(data, content);

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.UpdateTarget();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _controller.UpdateTarget();
        }

        private void zoomAndPanControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void zoomAndPanControl_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void zoomAndPanControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void zoomAndPanControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}
