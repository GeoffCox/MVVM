namespace BellaCode.Mvvm.Behaviors
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Executes the specified command when the associated object's window is closing.
    /// </summary>
    /// <remarks>
    /// This does not support allowing the command to cancel closing the window.
    /// </remarks>
    public class ClosingWindowExecutesCommandBehavior: Behavior<DependencyObject>
    {
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
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ClosingWindowExecutesCommandBehavior), new UIPropertyMetadata(null));
      
        protected override void OnAttached()
        {
            base.OnAttached();

            var window = GetAssociatedObjectWindow();
            if (window != null)
            {
                window.Closing += this.AssociatedObject_Window_Closing;
            }
        }

        protected override void OnDetaching()
        {
            var window = GetAssociatedObjectWindow();
            if (window != null)
            {
                window.Closing -= this.AssociatedObject_Window_Closing;
            }            

            base.OnDetaching();
        }

        private void AssociatedObject_Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.Command != null)
            {                
                // I route the command to the start and the AssociatedObject.
                var routedCommand = this.Command as RoutedCommand;
                var inputElement = this.AssociatedObject as IInputElement;
                if (routedCommand != null && inputElement != null)
                {
                    if (routedCommand.CanExecute(e, inputElement))
                    {
                        routedCommand.Execute(e, inputElement);
                    }
                }
                else
                {
                    if (this.Command.CanExecute(e))
                    {
                        this.Command.Execute(e);
                    }
                }
            }              
        }

        private Window GetAssociatedObjectWindow()
        {
            var window = this.AssociatedObject as Window;
            if (window == null)
            {
                DependencyObject dependencyObject = this.AssociatedObject as DependencyObject;
                if (dependencyObject != null)
                {
                    window = Window.GetWindow(dependencyObject);
                }
            }

            return window;
        }
    }
}
