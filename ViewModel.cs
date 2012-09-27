namespace BellaCode.Mvvm
{
    using System.ComponentModel;

    /// <summary>
    /// The base class for an implementation of IViewModel.
    /// </summary>    
    public class ViewModel : ViewModel<object, object>, IViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public ViewModel()
            : base()
        {
        }
    }
}
