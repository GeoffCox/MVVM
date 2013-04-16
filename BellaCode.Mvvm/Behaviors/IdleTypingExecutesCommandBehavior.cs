namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Threading;

    /// <summary>
    /// Executes a command for a TextBox when the user is typing and pauses for 500 ms.
    /// </summary>    
    [ExcludeFromCodeCoverage]
    public class IdleTypingExecutesCommandBehavior : Behavior<TextBox>
    {
        private TextBox associatedTextBox;
        private DispatcherTimer timer;

        /// <summary>
        /// Gets or sets the command to execute.
        /// </summary>
        /// <value>A command.</value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Identifies the Command property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(IdleTypingExecutesCommandBehavior), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command parameter to pass to the Command.
        /// </summary>
        /// <value>An object.</value>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Identifies the CommandParameter property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(IdleTypingExecutesCommandBehavior), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether to always pass TextBox.Text as the command parameter.
        /// </summary>
        /// <value>A boolean.</value>
        public bool AlwaysPassTextAsCommandParameter
        {
            get { return (bool)GetValue(AlwaysPassTextAsCommandParameterProperty); }
            set { SetValue(AlwaysPassTextAsCommandParameterProperty, value); }
        }

        /// <summary>
        /// Identifies the AlwaysPassTextAsCommandParameter property.
        /// </summary>
        public static readonly DependencyProperty AlwaysPassTextAsCommandParameterProperty = DependencyProperty.Register("AlwaysPassTextAsCommandParameter", typeof(bool), typeof(IdleTypingExecutesCommandBehavior), new UIPropertyMetadata(null));

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.associatedTextBox = this.AssociatedObject as TextBox;

            if (this.associatedTextBox != null)
            {
                this.associatedTextBox.GotKeyboardFocus += this.AssociatedTextBox_GotKeyboardFocus;
                this.associatedTextBox.LostKeyboardFocus += this.AssociatedTextBox_LostKeyboardFocus;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.associatedTextBox != null)
            {
                this.associatedTextBox.GotKeyboardFocus -= this.AssociatedTextBox_GotKeyboardFocus;
                this.associatedTextBox.LostKeyboardFocus -= this.AssociatedTextBox_LostKeyboardFocus;
            }

            this.RemoveTimer();

            this.associatedTextBox = null;
        }

        private void AssociatedTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.StartTimer();

            // I don't set e.Handled to true because I don't want to stop any other effects of this event.
        }

        private void AssociatedTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.StopTimer();

            // I don't set e.Handled to true because I don't want to stop any other effects of this event.
        }

        private void StartTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.ApplicationIdle, this.Timer_Tick, this.associatedTextBox.Dispatcher);
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
            if (this.Command != null)
            {
                object commandParameter = this.CommandParameter;

                if (this.AlwaysPassTextAsCommandParameter)
                {
                    commandParameter = this.associatedTextBox.Text;
                }

                if (this.Command.CanExecute(commandParameter))
                {
                    this.Command.Execute(commandParameter);
                }
            }
        }
    }
}

