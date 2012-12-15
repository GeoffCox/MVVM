namespace BellaCodeAir.Commands
{
    using System.Windows.Input;

    public class MainCommands
    {
        public static RoutedCommand Reset = new RoutedCommand("Reset", typeof(MainCommands));
    }
}
