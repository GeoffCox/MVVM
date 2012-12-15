namespace BellaCodeAir.Actions
{
    using System.Windows;
    using System.Windows.Interactivity;
    using BellaCode.Mvvm;
    using BellaCodeAir.Models;

    // TODO: #07 - An action using InteractionEventArgs
    // --------------------------------------------------------------------------------
    /*
     * ConfirmDeleteFlightAction uses the InteractionEventArgs passed in the command parameter to ask for confirmation.
     * The InteractionEventArgs Data is the Flight to be deleted.
     * The InteractionEventArgs Result is the true/false result of the confirmation.
    */

    public class ConfirmDeleteFlightAction : TriggerAction<UIElement>
    {
        protected override void Invoke(object parameter)
        {
            var eventArgs = (InteractionEventArgs<Flight, bool>)parameter;

            string message = "Do you want to delete the selected flight?";
            var flight = eventArgs.Data;
            if (flight != null && flight.Airline != null)
            {
                message = string.Format("Do you want to delete flight '{0}{1:d4}'?", flight.Airline.Code, flight.Number);
            }
            eventArgs.Result = MessageBox.Show(message, "Delete Flight?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes;
        }
    }
}
