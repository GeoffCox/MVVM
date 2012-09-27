namespace BellaCode.Mvvm
{
    /// <summary>
    /// A mediator between a view and a model.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>               
        /// <remarks>
        /// In the M-V-VM pattern, the view model should have no dependencies on the view.
        /// However, the view model may need to provide the view when raising events or calling other services.
        /// As well some situations interacting with the UI (through an interface) is required for performance reasons or existing control limitations.        
        /// </remarks>
        object View { get; set; }  

        /// <summary>
        /// Gets or sets the model.
        /// </summary>        
        object Model { get; set; }
    }   
}
