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

        public GraphController(TimeLines source, Canvas target)
        {
            _source = source;
            _target = target;
        }


        public void UpdateTarget()
        {
            _target.Children.Clear();

            var halfRowHeight = _target.ActualHeight / (2 * _source.Participants.Length);

            var start = _source.Start;
            var end = _source.End;

            var margin = 50;
            var availableWidth = _target.ActualWidth - 2 * margin - 150;

            var durationMS = (end - start).TotalMilliseconds;
            var msPerPixel = durationMS / availableWidth;
            var pixelPerMs = 1 / msPerPixel;

            var participantRows = _source.Participants.Select((participant, index) =>
                new { Participant = participant, Y = (2 * index + 1) * halfRowHeight })
                .ToDictionary(a => a.Participant, a => a.Y);




            // Create participant views
            var participantViews = _source.Participants.Select(p =>
            {
                var result = new ParticipantView() { DataContext = p };
                result.Position = new System.Windows.Point(25, participantRows[p]);
                return result;
            });


            // Create happening views
            var happeningViews = _source.Happenings.Select(happening =>
            {
                var happeningView = new HappeningView() { Happening = happening };
                var x = (happening.Occasion - start).TotalMilliseconds * pixelPerMs + margin + 100;
                var y = (happening.Participants.Select(p => participantRows[p])).Average();
                happeningView.Position = new System.Windows.Point(x, y);
                return (happening, happeningView);
            }).ToDictionary(h => h.happening, h => h.happeningView);


            // Create edges between happenings
            var edges = _source.Participants.SelectMany(p =>
            {
                var happenings = p.Happenings;
                if (happenings.Count <= 1)
                    return Enumerable.Empty<UIElement>();

                var happeningPairs = happenings.Zip(happenings.Skip(1), (h1, h2) => (H1: happeningViews[h1], H2: happeningViews[h2]));
                var edges = happeningPairs.Select(pair =>
                {
                    var line = new Line();
                    var p1 = pair.H1.Position;
                    var p2 = pair.H2.Position;

                    line.X1 = p1.X;
                    line.Y1 = p1.Y;
                    line.X2 = p2.X;
                    line.Y2 = p2.Y;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 2;
                    return line;
                });

                return edges;
            });






            // Update target
            foreach (var v in happeningViews.Values) {
                _target.Children.Add(v);
            }
            foreach(var p in participantViews)
            {
                _target.Children.Add(p);
            }
            foreach(var e in edges)
            {
                _target.Children.Add(e);
            }



        }
    }
}
