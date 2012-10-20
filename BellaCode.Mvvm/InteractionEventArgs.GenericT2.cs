namespace BellaCode.Mvvm
{
    /// <summary>
    /// Provides data for interaction events where a view model provides data and the view returns a result.
    /// </summary>    
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <remarks>
    /// While developers may sometimes need to define custom event arguments, many interactions between the view model and view follow this common pattern.
    /// </remarks>
    public class InteractionEventArgs<TData, TResult> : InteractionEventArgs
    {
        public InteractionEventArgs(TData data)
            : base(data)
        {

        }

        public new TData Data
        {
            get
            {
                return (TData)base.Data;
            }
        }

        public new TResult Result
        {
            get
            {
                return (TResult)base.Result;
            }
            set
            {
                base.Result = value;
            }
        }
    }


}
