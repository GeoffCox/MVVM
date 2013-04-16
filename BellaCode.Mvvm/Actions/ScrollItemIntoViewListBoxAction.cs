namespace BellaCode.Mvvm.Actions
{
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using BellaCode.Mvvm;

    /// <summary>
    /// Calls ScrollIntoView for the item (parameter) on the associated list box.
    /// </summary>
    public class ScrollItemIntoViewListBoxAction : TriggerAction<ListBox>
    {
        protected override void Invoke(object parameter)
        {
            var eventArgs = (InteractionEventArgs)parameter;

            var item = eventArgs.Data;

            if (item != null)
            {
                this.AssociatedObject.ScrollIntoView(item);
            }
        }      
    }
}
