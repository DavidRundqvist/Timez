using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Timez
{
    class GraphController
    {
        TimeLines _source;
        Canvas _target;
        double? _pixelSpacing = null;


        public GraphController(TimeLines source, Canvas target)
        {
            _source = source;
            _target = target;
        }

        public void ChangeScale(float scaleFactor)
        {
            _pixelSpacing *= scaleFactor;
            UpdateTarget();
        }


        public void UpdateTarget()
        {
            _target.Children.Clear();

            if (_source.Events.Length <= 1)
                return;

            var start = _source.Start;
            var end = _source.End;
            var events = _source.Events;
            var halfRowHeight = 150.0d;
            var participantsColumnWidth = 150;


            if (!_pixelSpacing.HasValue)  {
                var totalTimeSpan = (end - start).TotalSeconds;
                var startWidth = _target.ActualWidth - participantsColumnWidth - 50;
                // start with a pixelspacing that shows the whole graph
                _pixelSpacing = startWidth / totalTimeSpan;
            }







            _target.Width = (end - start).TotalSeconds * _pixelSpacing.Value + participantsColumnWidth + 50;


            double GetX(Event @event) {
                return (@event.Occasion - start).TotalSeconds * _pixelSpacing.Value + participantsColumnWidth;
            }


            var participantRows = _source.Participants.Select((participant, index) =>
                new { Participant = participant, Y = (2 * index + 1) * halfRowHeight })
                .ToDictionary(a => a.Participant, a => a.Y);




            // Create participant views
            var participantViews = _source.Participants.Select(p =>
            {
                var result = new ParticipantView() { DataContext = p };
                result.Position = new System.Windows.Point(52, participantRows[p]);
                return result;
            });


            // Create happening views
            var eventViews = _source.Participants.SelectMany(participant => participant.Events.Select(@event => {

                var x = GetX(@event);
                var y = participantRows[participant];

                var vm = new EventViewModel(@event, x, y);
                var eventView = new EventView() { Event = vm };

                return (participant, eventView);
            }))
                .GroupBy(a => a.participant, a => a.eventView)
                .ToDictionary(g => g.Key, g => g.ToArray());



            // Create time lines for each participant
            var edges = _source.Participants.Where(p => p.Events.Length > 1).Select(p => {
                var events = p.Events;

                var positions = eventViews[p].Select(h => h.Event.Position);
                var startPoint = positions.OrderBy(p => p.X).First();
                var endPoint = positions.OrderBy(p => p.X).Last();



                var path = new Path() { Stroke = new SolidColorBrush(p.Color), StrokeThickness = 2 };
                path.Data = new PathGeometry(new[] { 
                    new PathFigure(startPoint, new []{
                        new LineSegment(endPoint, true) }, false) });

                return path;

            });

            // Create connections between events
            var conns = _source.Events.Where(h => h.Participants.Count > 1).Select(h => {
                var ys = h.Participants.Select(p => participantRows[p]).ToArray();
                var ymin = ys.OrderBy(y => y).First();
                var ymax = ys.OrderBy(y => y).Last();

                var x = GetX(h);
                var startPoint = new Point(x, ymin);
                var endPoint = new Point(x, ymax);

                var path = new Path() { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 2 };
                path.Data = new PathGeometry(new[] {
                    new PathFigure(startPoint, new []{
                        new LineSegment(endPoint, true) }, false) });

                return path;
            });




            // Update target
            foreach (var v in eventViews.Values.SelectMany(h => h))
            {
                _target.Children.Add(v);
            }
            foreach (var p in participantViews)
            {
                _target.Children.Add(p);
            }
            foreach (var e in edges) {
                _target.Children.Add(e);
            }
            foreach (var c in conns) {
                _target.Children.Add(c);
            }



        }


    }
}
