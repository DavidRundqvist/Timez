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

            if (_source.Happenings.Length <= 1)
                return;

            var start = _source.Start;
            var end = _source.End;
            var happenings = _source.Happenings;
            var halfRowHeight = 150;
            var participantsColumnWidth = 150;


            if (!_pixelSpacing.HasValue)  {
                var totalTimeSpan = (end - start).TotalSeconds;
                var startWidth = _target.ActualWidth - participantsColumnWidth - 50;
                // start with a pixelspacing that shows the whole graph
                _pixelSpacing = startWidth / totalTimeSpan;
            }







            _target.Width = (end - start).TotalSeconds * _pixelSpacing.Value + participantsColumnWidth + 50;

            

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
            var happeningViews = _source.Happenings.Select(happening =>
            {
                var happeningView = new HappeningView() { Happening = happening };
                var x = (happening.Occasion - start).TotalSeconds * _pixelSpacing.Value + participantsColumnWidth;
                var y = (happening.Participants.Select(p => participantRows[p])).Average();
                happeningView.Position = new Point(x, y);
                return (happening, happeningView);
            }).ToDictionary(h => h.happening, h => h.happeningView);


            // Make an array containing Bezier curve points and control points.
            Point[] MakeCurvePoints(Point[] points, double tension)
            {
                if (points.Length < 2) return null;
                double control_scale = tension / 0.5 * 0.175;

                // Make a list containing the points and
                // appropriate control points.
                List<Point> result_points = new List<Point>();
                result_points.Add(points[0]);

                for (int i = 0; i < points.Length - 1; i++)
    {
                // Get the point and its neighbors.
                Point pt_before = points[Math.Max(i - 1, 0)];
                Point pt = points[i];
                Point pt_after = points[i + 1];
                Point pt_after2 = points[Math.Min(i + 2, points.Length - 1)];

                double dx1 = pt_after.X - pt_before.X;
                double dy1 = pt_after.Y - pt_before.Y;

                Point p1 = points[i];
                Point p4 = pt_after;

                double dx = pt_after.X - pt_before.X;
                double dy = pt_after.Y - pt_before.Y;
                Point p2 = new Point(
                    pt.X + control_scale * dx,
                    pt.Y + control_scale * dy);

                dx = pt_after2.X - pt.X;
                dy = pt_after2.Y - pt.Y;
                Point p3 = new Point(
                    pt_after.X - control_scale * dx,
                    pt_after.Y - control_scale * dy);

                // Save points p2, p3, and p4.
                result_points.Add(p2);
                result_points.Add(p3);
                result_points.Add(p4);
            }

            // Return the points.
            return result_points.ToArray();
        }



            // Create edges between happenings
            var edges = _source.Participants.Select(p =>
            {
                var happenings = p.Happenings;
                if (happenings.Length <= 1)
                    return null;

                var points = happenings.Select(h => happeningViews[h].Position).ToArray(); ;
                var bezierPoints = MakeCurvePoints(points, 0.2f);

                var path = new Path() { Stroke = new SolidColorBrush(p.Color), StrokeThickness = 2 };
                var bezier = new PolyBezierSegment(bezierPoints.Skip(1), true);

                IEnumerable<PathSegment> segments = new[] { bezier };
                path.Data = new PathGeometry(new[] { new PathFigure(bezierPoints.First(), segments, false) });

                return path;
            }).Where(e => e != null);






            // Update target
            foreach (var v in happeningViews.Values)
            {
                _target.Children.Add(v);
            }
            foreach (var p in participantViews)
            {
                _target.Children.Add(p);
            }
            foreach (var e in edges)
            {
                _target.Children.Add(e);
            }



        }
    }
}
