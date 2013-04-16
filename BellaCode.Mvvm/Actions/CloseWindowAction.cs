namespace BellaCode.Mvvm.Actions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Closes the window containing the associated UI element with the specified dialog result.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CloseWindowAction : TriggerAction<UIElement>
    {
        public bool? DialogResult
        {
            get { return (bool?)GetValue(DialogResultProperty); }
            set { SetValue(DialogResultProperty, value); }
        }

        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.Register("DialogResult", typeof(bool?), typeof(CloseWindowAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
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

            if (window != null)
            {
                if (this.DialogResult.HasValue)
                {
                    window.DialogResult = this.DialogResult;
                }

                window.Close();
            }
        }
    }
}

