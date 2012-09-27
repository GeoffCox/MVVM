namespace BellaCode.Mvvm
{
    using System;

    /// <summary>
    /// Provides data for Interaction events where a view model provides data and the view optionally provides a result.
    /// </summary>
    public class InteractionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionEventArgs"/> class.
        /// </summary>
        public InteractionEventArgs()
        {    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionEventArgs"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public InteractionEventArgs(object data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public object Result { get; set; }
    }
}
