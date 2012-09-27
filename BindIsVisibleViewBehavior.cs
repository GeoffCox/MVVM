namespace BellaCode.Mvvm
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    /// <summary>
    /// Binds the associated UIElement's IsVisible property to the IsViewVisible property to allow the view model to be notified (via OneWayToSource binding).
    /// </summary>
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
                throw new InvalidOperationException("SelectedItems binding mode must be OneWayToSource");
            }

            this.AssociatedObject.IsVisibleChanged += this.AssociatedObject_IsVisibleChanged;
            this.UpdateIsViewVisible();
        }      

        protected override void OnDetaching()
        {
            this.AssociatedObject.IsVisibleChanged -= this.AssociatedObject_IsVisibleChanged;
            base.OnDetaching();
        }

        void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
