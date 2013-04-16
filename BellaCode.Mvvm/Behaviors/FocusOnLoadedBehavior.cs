namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Focuses on the associated element when it is loaded.
    /// </summary>
    public class FocusOnLoadedBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Loaded += this.AssociatedObject_Loaded; 
        }                                

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded; 

            base.OnDetaching();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.Focus();
        }        
    }
}
