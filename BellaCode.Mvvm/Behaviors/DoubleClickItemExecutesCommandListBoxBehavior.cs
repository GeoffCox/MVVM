namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Executes the specified command when a ListBoxItem is double-clicked.
    /// </summary>
    /// <remarks>
    /// The DataContext of the ListBoxItem is passed as the parameter to the command.
    /// </remarks>
    public class DoubleClickItemExecutesCommandListBoxBehavior : Behavior<ListBox>
    {    
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(DoubleClickItemExecutesCommandListBoxBehavior), new UIPropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(DoubleClickItemExecutesCommandListBoxBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewMouseDoubleClick += this.AssociatedObject_PreviewMouseDoubleClick;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewMouseDoubleClick -= this.AssociatedObject_PreviewMouseDoubleClick;
            base.OnDetaching();
        }

        private void AssociatedObject_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = VisualTreeHelperT.FindVisualParentOrSelf<ListBoxItem>(e.OriginalSource);
            if (item != null && (this.Command != null))
            {
                object commandParameter = this.CommandParameter;

                if (commandParameter == null)
                {
                    commandParameter = item.DataContext;
                }

                if (this.Command.CanExecute(commandParameter))
                {
                    this.Command.Execute(commandParameter);
                }
            }
        }
    }
}
