namespace BellaCode.Mvvm.Behaviors
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// Shows the specified watermark control when the textbox is empty and does not have focus.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class WatermarkBehavior : Behavior<TextBox>
    {
        public UIElement Watermark
        {
            get { return (UIElement)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(UIElement), typeof(WatermarkBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((WatermarkBehavior)d).OnWatermarkChanged((UIElement)e.OldValue, (UIElement)e.NewValue)));

        public bool IsVisibleWhenFocusedAndEmpty { get; set; }

        private void OnWatermarkChanged(UIElement oldValue, UIElement newValue)
        {
            if (oldValue != null)
            {
                oldValue.Visibility = Visibility.Collapsed;
            }

            this.UpdateWatermarkVisibility();
        }

        private void AssociatedObject_Changed(object sender, RoutedEventArgs e)
        {
            this.UpdateWatermarkVisibility();
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.GotFocus += this.AssociatedObject_Changed;
                this.AssociatedObject.LostFocus += this.AssociatedObject_Changed;
                this.AssociatedObject.TextChanged += this.AssociatedObject_Changed;
            }
        }

        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.GotFocus -= this.AssociatedObject_Changed;
                this.AssociatedObject.LostFocus -= this.AssociatedObject_Changed;
                this.AssociatedObject.TextChanged -= this.AssociatedObject_Changed;
            }

            base.OnDetaching();
        }

        private void UpdateWatermarkVisibility()
        {
            if (this.Watermark != null && this.AssociatedObject != null)
            {
                if (!string.IsNullOrEmpty(this.AssociatedObject.Text)) // I hide the watermark if there is text
                {
                    this.Watermark.Visibility = Visibility.Hidden;
                }
                else if (this.AssociatedObject.IsFocused && !this.IsVisibleWhenFocusedAndEmpty) // I hide if focused and empty unless set to show
                {
                    this.Watermark.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Watermark.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

