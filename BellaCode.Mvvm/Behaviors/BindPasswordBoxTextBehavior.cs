namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// Provides binding to a PasswordBox Password property.
    /// </summary>
    public class BindPasswordBoxTextBehavior : Behavior<PasswordBox>
    {
        private bool isSettingPasswordText;

        public string PasswordText
        {
            get { return (string)GetValue(PasswordTextProperty); }
            set { SetValue(PasswordTextProperty, value); }
        }

        public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.Register("PasswordText", typeof(string), typeof(BindPasswordBoxTextBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BindPasswordBoxTextBehavior)d).OnPasswordTextChanged((string)e.OldValue, (string)e.NewValue)));


        private void OnPasswordTextChanged(string oldValue, string newValue)
        {
            if (!this.isSettingPasswordText && this.AssociatedObject != null)
            {
                this.AssociatedObject.Password = this.PasswordText;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.UpdatePasswordText();
            this.AssociatedObject.PasswordChanged += this.AssociatedObject_PasswordChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PasswordChanged -= this.AssociatedObject_PasswordChanged;
            base.OnDetaching();
        }

        void AssociatedObject_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            this.UpdatePasswordText();
        }

        private void UpdatePasswordText()
        {
            this.isSettingPasswordText = true;

            this.PasswordText = this.AssociatedObject.Password;

            this.isSettingPasswordText = false;
        }
    }
}
