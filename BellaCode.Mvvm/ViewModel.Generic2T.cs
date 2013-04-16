namespace BellaCode.Mvvm
{
    using System.ComponentModel;
#if NET45
    using System.Runtime.CompilerServices;
#endif
    using System.Windows;

    /// <summary>
    /// The base class for a strongly typed implementation of IViewModel.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class ViewModel<TView, TModel> : IViewModel<TView, TModel>, INotifyPropertyChanged
        where TView : class
        where TModel : class
    {
        private TView view;
        private TModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel&lt;T&gt;"/> class.
        /// </summary>
        public ViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <remarks>
        /// In the M-V-VM pattern, the view model should have no dependencies on the view.
        /// However, the view model may need to provide the view when raising events or calling other services.
        /// As well some situations interacting with the UI (through an interface) is required for performance reasons or existing control limitations.        
        /// </remarks>
        public TView View
        {
            get
            {
                return this.view;
            }
            set
            {
                if (this.view != value)
                {
                    var oldValue = this.view;
                    this.view = value;
                    this.OnViewChanged(oldValue, this.view);
                }
            }
        }

        /// <summary>
        /// Called when the view changes.
        /// </summary>
        /// <param name="oldValue">The old view.</param>
        /// <param name="newValue">The new view.</param>
        protected virtual void OnViewChanged(TView oldValue, TView newValue)
        {
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <remarks>
        /// In the M-V-VM pattern, the view model should have no dependencies on the view.
        /// However, the view model may need to provide the view when raising events or calling other services.
        /// As well some situations interacting with the UI (through an interface) is required for performance reasons or existing control limitations.        
        /// </remarks>
        object IViewModel.View
        {
            get
            {
                return this.view;
            }
            set
            {
                this.View = (TView)value;
            }
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>       
        public TModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                if (this.model != value)
                {
                    var oldValue = this.model;
                    this.model = value;
                    this.OnModelChanged(oldValue, this.model);

                    // I invalidate all properties when the underlying model changes.
                    this.RaisePropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Called when the model changes.
        /// </summary>
        /// <param name="oldValue">The old model.</param>
        /// <param name="newValue">The new model.</param>
        protected virtual void OnModelChanged(TModel oldValue, TModel newValue)
        {
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>       
        object IViewModel.Model
        {
            get
            {
                return this.Model;
            }
            set
            {
                this.Model = (TModel)value;
            }
        }

        /// <summary>
        /// Raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

#if NET45
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
#else
        protected void RaisePropertyChanged(string propertyName)
        {
#endif
            if (this.PropertyChanged != null && propertyName != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
