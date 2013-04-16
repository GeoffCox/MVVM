namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Fixes a bug with the Calendar that takes over mouse capture.
    /// Without this fix, users must click on a control outside the calendar to release mouse capture.
    /// </summary>
    public class FixCalendarMouseCaptureBehavior : Behavior<Calendar>
    {
        private Calendar associatedCalendar;

        public FixCalendarMouseCaptureBehavior()
        {

        }
        
        protected override void OnAttached()
        {
            base.OnAttached();

            this.associatedCalendar = this.AssociatedObject as Calendar;
            if (this.associatedCalendar != null)
            {
                this.associatedCalendar.MouseMove += AssociatedCalendar_MouseMove;
            }
        }        

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.associatedCalendar != null)
            {
                this.associatedCalendar.MouseMove -= this.AssociatedCalendar_MouseMove;
            }
        }

        private void AssociatedCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Mouse.GetPosition(this.associatedCalendar);
            if (p.X < 0 || p.Y < 0 || p.X > this.associatedCalendar.ActualWidth || p.Y > this.associatedCalendar.ActualHeight)
            {
                if (this.associatedCalendar.IsMouseCaptureWithin)
                {
                    if (e.LeftButton == MouseButtonState.Released)
                    {
                        Mouse.Capture(null);
                    }
                }
            } 
        }
    }
}
