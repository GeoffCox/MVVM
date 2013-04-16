namespace BellaCode.Mvvm.Actions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Sets the focus to the specified Target.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SetFocusAction : TargetedTriggerAction<UIElement>
    {
        protected override void Invoke(object parameter)
        {
            if (this.Target != null)
            {
                this.Target.Focus();
            }
        }
    }
}

