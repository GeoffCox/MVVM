namespace BellaCode.Mvvm
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    /// <summary>
    /// Binds the associated UIElement's IsVisible property to the IsVisible dependency property (via OneWayToSource binding).
    /// This allows a property on the view model to be set.   
    /// </summary>
    /// <remarks>
    /// In this example, the view model's IsActive property is updated:
    /// <code>&lt;bellacode:BindIsVisibleViewBehavior IsVisible="{Binding IsActive, Mode=OneWayToSource}"/&gt;</code>
    /// </remarks>
    public class BindIsVisibleViewBehavior : Behavior<UIElement>
    {
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool), typeof(BindIsVisibleViewBehavior), new FrameworkPropertyMetadata(false));

        protected override void OnAttached()
        {
            base.OnAttached();

            var binding = BindingOperations.GetBinding(this, IsVisibleProperty);
            if (binding.Mode != BindingMode.OneWayToSource)
            {
                throw new InvalidOperationException("The BindIsVisibleViewBehavior.IsVisible BindingMode is not OneWayToSource.");
            }

            this.AssociatedObject.IsVisibleChanged += this.AssociatedObject_IsVisibleChanged;
            this.UpdateIsViewVisible();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.IsVisibleChanged -= this.AssociatedObject_IsVisibleChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateIsViewVisible();
        }

        private void UpdateIsViewVisible()
        {
            if (this.AssociatedObject != null)
            {
                this.IsVisible = this.AssociatedObject.IsVisible;
            }
            else
            {
                this.IsVisible = false;
            }
        }
    }
}
