namespace BellaCode.Mvvm.Actions
{
    using BellaCode.Mvvm;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Displays the specified message with the optional caption.
    /// </summary>
    /// <remarks>
    /// If the parameter is InteractionEventArgs&lt;string,TResult&gt; 
    /// then it will be used as the message rather than the Message dependency property.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class ShowMessageAction : TriggerAction<UIElement>
    {
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ShowMessageAction), new FrameworkPropertyMetadata(null));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(ShowMessageAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionEventArgs<string>;
            string message = e != null ? e.Data : null;

            if (string.IsNullOrEmpty(message))
            {
                message = this.Message;
            }

            string caption = this.Caption ?? string.Empty;

            MessageBox.Show(message, caption);
        }
    }
}

