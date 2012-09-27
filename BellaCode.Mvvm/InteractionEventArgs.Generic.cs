namespace BellaCode.Mvvm
{
    /// <summary>
    /// Provides data for Interaction events where a view model provides data and the view optionally provides a result.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class InteractionEventArgs<TData, TResult> : InteractionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionEventArgs&lt;TData, TResult&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public InteractionEventArgs(TData data)
            : base(data)
        {

        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public new TData Data
        {
            get
            {
                return (TData)base.Data;
            }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
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

    /// <summary>
    /// Provides data for Interaction events where a view model provides data and the view optionally provides a result.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class InteractionEventArgs<TData> : InteractionEventArgs<TData, object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionEventArgs&lt;TData&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public InteractionEventArgs(TData data)
            : base(data)
        {

        }        
    }
}
