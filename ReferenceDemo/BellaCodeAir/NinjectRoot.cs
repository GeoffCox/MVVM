namespace BellaCodeAir
{
    using BellaCode.Mvvm;
    using Ninject;
    using NinjectAdapter;

    // TODO: #08 - A Ninject dependency injection container
    // --------------------------------------------------------------------------------
    /*
     * Ninject is one of several dependency injection containers available to help implement the Inversion of Control pattern (IoC).
     * The implementation corresponding to each interface are registered here.
     * The CommonServiceLocator allows classes not created by the DI container to use the DI container.
     * The ViewModelScope.ViewModelFactory allows view model types to be resolved from the DI container.
    */

    public class NinjectRoot
    {
        public static void Initialize()
        {
            var kernel = new StandardKernel();            

            kernel.Bind<IWorldClock>().To<WorldClock>().InSingletonScope();
            kernel.Bind<IFlightData>().To<FlightData>().InSingletonScope();

            RegisterWithCommonServiceLocator(kernel);
            RegisterNinjectViewModelFactory(kernel);
        }       

        private static void RegisterWithCommonServiceLocator(IKernel kernel)
        {
            kernel.Bind<NinjectServiceLocator>().ToMethod(c => new NinjectServiceLocator(kernel)).InSingletonScope();
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => kernel.Get<NinjectServiceLocator>());
        }

        private static void RegisterNinjectViewModelFactory(StandardKernel kernel)
        {
            ViewModelScope.ViewModelFactory = new NinjectViewModelFactory(kernel);
        }
    }
}
