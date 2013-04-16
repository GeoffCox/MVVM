namespace BellaCode.Mvvm.Behaviors
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Selects all the text in a textbox when the text box gets focus.
    /// </summary>
    [ExcludeFromCodeCoverage]    
    public class SelectAllOnFocusTextBoxBehavior : Behavior<TextBoxBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.GotFocus += this.AssociatedObject_GotFocus;
            this.AssociatedObject.PreviewMouseUp += this.AssociatedObject_PreviewMouseUp;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.GotFocus -= this.AssociatedObject_GotFocus;
            this.AssociatedObject.PreviewMouseUp -= this.AssociatedObject_PreviewMouseUp;
            base.OnDetaching();
        }

        private bool _isSelectAllDeferred = false;

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            // I defer the select all until the mouse up as the left mouse down is trying to set caret position.
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this._isSelectAllDeferred = true;
            }
            else
            {
                this.AssociatedObject.SelectAll();
            }
        }

        private void AssociatedObject_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this._isSelectAllDeferred)
            {
                var foregroundTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                // I set focus on a async call to allow for the mouse event to complete
                Task.Factory.StartNew(() =>
                {
                })
                .ContinueWith((t) =>
                {
                    this.AssociatedObject.SelectAll();
                }, foregroundTaskScheduler);

                this._isSelectAllDeferred = false;
            }
        }
    }
}

