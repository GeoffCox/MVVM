namespace BellaCode.Mvvm
{
    using System;

    /// <summary>
    /// A factory for creating view models. Used by the ViewModelDataContext.
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// Creates the view model of the specified type.
        /// </summary>
        /// <param name="viewModelType">The type of the view model to create.</param>
        /// <param name="factoryContext">Additional context information.</param>
        /// <returns>
        /// A new instance of the view model.
        /// </returns>
        IViewModel Create(Type viewModelType, object factoryContext);        
    }
}
