namespace BellaCode.Mvvm.Actions
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Sets the title of the window for the associated control to the specified new title.
    /// </summary>
    public class SetWindowTitleAction : TriggerAction<UIElement>
    {
        public string NewWindowTitle
        {
            get { return (string)GetValue(NewWindowTitleProperty); }
            set { SetValue(NewWindowTitleProperty, value); }
        }

        public static readonly DependencyProperty NewWindowTitleProperty = DependencyProperty.Register("NewWindowTitle", typeof(string), typeof(SetWindowTitleAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject != null)
            {
                var window = Window.GetWindow(this.AssociatedObject);
                if (window != null)
                {
                    string title = parameter as string;
                    if (string.IsNullOrEmpty(title))
                    {
                        title = this.NewWindowTitle;
                    }

                    window.Title = title ?? string.Empty;
                }
            }
        }
    }
}
