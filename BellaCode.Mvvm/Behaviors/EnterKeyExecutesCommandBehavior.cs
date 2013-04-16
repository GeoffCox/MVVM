namespace BellaCode.Mvvm.Behaviors
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Executes a command when the enter key (without modifiers) is pressed in a text box.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EnterKeyExecutesCommandBehavior : Behavior<TextBox>
    {
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(EnterKeyExecutesCommandBehavior), new FrameworkPropertyMetadata(true));

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
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EnterKeyExecutesCommandBehavior), new UIPropertyMetadata(null));

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
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(EnterKeyExecutesCommandBehavior), new UIPropertyMetadata(null));

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
        public static readonly DependencyProperty AlwaysPassTextAsCommandParameterProperty = DependencyProperty.Register("AlwaysPassTextAsCommandParameter", typeof(bool), typeof(EnterKeyExecutesCommandBehavior), new UIPropertyMetadata(null));

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.PreviewKeyDown += this.AssociatedObject_PreviewKeyDown;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.PreviewKeyDown -= this.AssociatedObject_PreviewKeyDown;
            }

            base.OnDetaching();
        }

        private void AssociatedObject_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((this.IsEnabled) && (this.Command != null) && (e.Key == Key.Enter) && (Keyboard.Modifiers == ModifierKeys.None))
            {
                object commandParameter = this.CommandParameter;

                if (this.AlwaysPassTextAsCommandParameter)
                {
                    commandParameter = this.AssociatedObject.Text;
                }

                if (this.Command.CanExecute(commandParameter))
                {
                    this.Command.Execute(commandParameter);
                }
            }

            // I don't set e.Handled to true because I don't want to stop any other effects of this keystroke.
        }
    }
}

