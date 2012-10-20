namespace BellaCode.Mvvm
{
    /// <summary>
    /// Provides data for Interaction events where a view model provides data.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <remarks>
    /// This class is commonly used when the view receives data, but does not provide a result.
    /// </remarks>
    public class InteractionEventArgs<TData> : InteractionEventArgs<TData, object>
    {
        public InteractionEventArgs(TData data)
            : base(data)
        {
        }
    }
}
