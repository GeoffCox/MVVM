namespace BellaCodeAir
{
    using System.Windows;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectRoot.Initialize();
        }
    }
}
