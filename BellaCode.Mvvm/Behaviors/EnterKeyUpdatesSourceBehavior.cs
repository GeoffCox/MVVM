namespace BellaCode.Mvvm.Behaviors
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Updates the binding source of a TextBox when the enter key (without modifiers) is pressed.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EnterKeyUpdatesSourceBehavior : Behavior<TextBox>
    {
        private TextBox associatedTextBox;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.associatedTextBox = this.AssociatedObject as TextBox;

            if (this.associatedTextBox != null)
            {
                this.associatedTextBox.PreviewKeyDown += this.AssociatedTextBox_PreviewKeyDown;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (this.associatedTextBox != null)
            {
                this.associatedTextBox.PreviewKeyDown -= this.AssociatedTextBox_PreviewKeyDown;
            }

            this.associatedTextBox = null;

            base.OnDetaching();
        }

        private void AssociatedTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) && (Keyboard.Modifiers == ModifierKeys.None))
            {
                BindingExpression bindingExpression = BindingOperations.GetBindingExpression(this.AssociatedObject, TextBox.TextProperty);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                }
            }

            // I don't set e.Handled to true because I don't want to stop any other effects of this keystroke.
        }
    }
}

