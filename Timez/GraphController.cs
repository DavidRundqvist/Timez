using System;
using System.Collections.Generic;
using System.Text;
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


            for (var iIndex = 0; iIndex < _source.Participants.Length; iIndex++ )
            {
                var participant = _source.Participants[iIndex];
                var y = (2 * iIndex + 1) * halfRowHeight;

                var pv = new ParticipantView() { DataContext = participant };
                pv.SetValue(Canvas.LeftProperty, 25.0d);
                pv.SetValue(Canvas.TopProperty, y + 25);

                _target.Children.Add(pv);


                foreach(var happening in participant.Happenings)
                {
                    var x = (happening.Occasion - start).TotalMilliseconds * pixelPerMs + margin + 100;

                    var happeningView = new HappeningView() { DataContext = happening };

                    happeningView.SetValue(Canvas.LeftProperty, x - happeningView.Width / 2);
                    happeningView.SetValue(Canvas.TopProperty, y);

                    _target.Children.Add(happeningView);
                }


            }

        }
    }
}
