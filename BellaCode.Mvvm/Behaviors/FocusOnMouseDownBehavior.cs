namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Helps controls like ListBox get focus when the mouse is down.
    /// These controls only take focus when the the mouse is down over an active item.  
    /// </summary>
    public class FocusOnMouseDownBehavior : Behavior<UIElement>
    {
        private UIElement associatedControl;                
        
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.associatedControl = this.AssociatedObject as UIElement;

            if (this.associatedControl != null)
            {
                this.associatedControl.MouseDown += AssociatedControl_MouseDown;                
            }
        }                        

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.associatedControl != null)
            {
                this.associatedControl.MouseDown -= AssociatedControl_MouseDown;
            }

            this.associatedControl = null;
        }

        void AssociatedControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.associatedControl != null)
            {
                // For multiple-select ListBox controls, I set focus to the last selected item to preserve selection behavior.
                ListBox listBox = this.associatedControl as ListBox;
                if ((listBox != null) && (listBox.SelectionMode != SelectionMode.Single))
                {
                    if ((listBox.SelectedItems != null) && (listBox.SelectedItems.Count > 0))
                    {
                        FrameworkElement element = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItems[listBox.SelectedItems.Count-1]) as FrameworkElement;
                        if (element != null)
                        {
                            element.Focus();
                            return;
                        }                    
                    }                    
                }

                // For Selector controls, I set focus to the currently selected to preserve selection behavior.
                Selector selector = this.associatedControl as Selector;
                if ((selector != null) && (selector.SelectedIndex >= 0))
                {
                    FrameworkElement element = selector.ItemContainerGenerator.ContainerFromIndex(selector.SelectedIndex) as FrameworkElement;
                    if (element != null)
                    {
                        element.Focus();
                        return;
                    }                    
                }
                
                // I default to just setting focus to the control itself.
                this.associatedControl.Focus();
                
            }
        }
    }
}
