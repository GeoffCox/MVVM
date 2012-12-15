namespace BellaCode.Mvvm
{
    /// <summary>
    /// The base class for a strongly typed implementation of IViewModel.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
	public class ViewModel<TModel> : ViewModel<object, TModel> where TModel: class
	{
	}
}
