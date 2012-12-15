namespace BellaCodeAir.Commands
{
    using System.Windows.Input;

    public class ListCommands
    {
        public static RoutedCommand Add = new RoutedCommand("Add", typeof(ListCommands));

        public static RoutedCommand Delete = new RoutedCommand("Delete", typeof(ListCommands));
    }
}
