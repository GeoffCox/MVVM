namespace BellaCodeAir
{
    using System;
    using BellaCode.Mvvm;
    using Ninject;

    // TODO: #09 - A view model factory that uses Ninject to create view model instances.
    // --------------------------------------------------------------------------------
    /*
     * The factory delegate view model instance creation to the Ninject dependency injection container.
    */

    public class NinjectViewModelFactory : IViewModelFactory
    {
        private IKernel _kernel;

        public NinjectViewModelFactory(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            this._kernel = kernel;
        }

        public IViewModel Create(Type viewModelType, object factoryContext)
        {
            return this._kernel.Get(viewModelType) as IViewModel;
        }
    }


}
