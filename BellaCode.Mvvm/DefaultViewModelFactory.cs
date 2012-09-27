namespace BellaCode.Mvvm
{
    using System;

    /// <summary>
    /// A default view model factory that load types from the application domain.
    /// </summary>
    public class DefaultViewModelFactory : IViewModelFactory
    {
        /// <summary>
        /// Creates the view model of the specified type.
        /// </summary>
        /// <param name="viewModelType">Type of the view model to create.</param>
        /// <param name="factoryContext">Additional context information.</param>
        /// <returns>
        /// A new instance of the view model.
        /// </returns>
        public IViewModel Create(Type viewModelType, object factoryContext)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException("viewModelType");
            }

            return (IViewModel)Activator.CreateInstance(viewModelType);
        }
    }
}
