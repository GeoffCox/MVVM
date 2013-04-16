namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Threading;

    /// <summary>
    /// Updates the binding source of a TextBox when the user is typing and pauses for 500ms
    /// </summary>  
    [ExcludeFromCodeCoverage]
    public class IdleTypingUpdatesSourceBehavior : Behavior<TextBox>
    {
        private DispatcherTimer timer;

        private DateTime? lastTypingTime;

        private TimeSpan delayThreshold = new TimeSpan(0, 0, 0, 0, 300);

        private bool updatingSource = false;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.GotKeyboardFocus += this.AssociatedObject_GotKeyboardFocus;
            this.AssociatedObject.LostKeyboardFocus += this.AssociatedObject_LostKeyboardFocus;
            this.AssociatedObject.TextChanged += this.AssociatedObject_TextChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.GotKeyboardFocus -= this.AssociatedObject_GotKeyboardFocus;
            this.AssociatedObject.LostKeyboardFocus -= this.AssociatedObject_LostKeyboardFocus;
            this.AssociatedObject.TextChanged -= this.AssociatedObject_TextChanged;

            this.RemoveTimer();
        }

        private void AssociatedObject_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.StartTimer();

            // I don't set e.Handled to true because I don't want to stop any other effects of this event.
        }


        private void AssociatedObject_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.StopTimer();

            // I don't set e.Handled to true because I don't want to stop any other effects of this event.
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lastTypingTime = DateTime.Now;
        }

        private void StartTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 100), DispatcherPriority.ApplicationIdle, this.Timer_Tick, this.AssociatedObject.Dispatcher);
            }
            timer.Start();
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void RemoveTimer()
        {
            if (timer != null)
            {
                timer.Tick -= this.Timer_Tick;
                timer = null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.updatingSource && this.lastTypingTime.HasValue && (DateTime.Now - this.lastTypingTime.Value > delayThreshold))
            {
                this.updatingSource = true;
                BindingExpression bindingExpression = BindingOperations.GetBindingExpression(this.AssociatedObject, TextBox.TextProperty);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                }
                this.updatingSource = false;
            }
        }
    }
}

