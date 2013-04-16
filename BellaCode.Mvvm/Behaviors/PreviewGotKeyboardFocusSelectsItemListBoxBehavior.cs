namespace BellaCode.Mvvm.Behaviors
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Helps cListBox to select the current item when the ListBox takes keyboard focus.
    /// </summary>
    public class PreviewGotKeyboardFocusSelectsItemListBoxBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewGotKeyboardFocus += AssociatedObject_PreviewGotKeyboardFocus;
        }             

        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewGotKeyboardFocus -= AssociatedObject_PreviewGotKeyboardFocus;
            base.OnDetaching();
        }     

        private void AssociatedObject_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                return;
            }

            var item = VisualTreeHelperT.FindVisualParentOrSelf<ListBoxItem>(e.OriginalSource);
            if (item != null)
            {
                if (this.AssociatedObject.SelectionMode == SelectionMode.Single)
                {
                    item.IsSelected = true;
                }
                else
                {
                    this.AssociatedObject.SelectedItems.Clear();
                    item.IsSelected = true;
                }
            }
        }       
    }
}
