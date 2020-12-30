using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Timez
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private readonly Event _event;
        private readonly double _x;
        private readonly double _y;

        public EventViewModel(Event @event, double x, double y)
        {
            _event = @event;
            _x = x;
            _y = y;
        }


        public float Width => 100;
        public double CanvasLeft => _x - Width / 2;

        public double CanvasTop => _y - 10;

        public Point Position => new Point(_x, _y);


        public string Name => _event.Name;
        public string Date => _event.Occasion.ToString("yyyy-MM-dd");



        public event PropertyChangedEventHandler PropertyChanged;



    }
}
