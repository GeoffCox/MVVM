namespace BellaCode.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A decorator that sets that DataContext of the child to be a view model.
    /// The view model receives the previous DataContext as its model.
    /// </summary>    
    public class ViewModelScope : Decorator
    {
        static ViewModelScope()
        {
            ViewModelFactory = new DefaultViewModelFactory();
        }

        private IViewModel _viewModel;

        public ViewModelScope()
        {
            this.Loaded += this.ViewModelScope_Loaded;
            this.Unloaded += this.ViewModelScope_Unloaded;
        }

        /// <summary>
        /// Gets or sets the type of the view model instance to create.
        /// </summary>      
        public Type ViewModelType { get; set; }

        /// <summary>
        /// Gets or sets the context to pass to the ViewModelFactory.Create method.
        /// </summary>
        /// <remarks>        
        /// This allows the view model factory to create the correct view model in cases where additional context from the UI is required.
        /// </remarks>
        [Category("Common Properties")]
        [Description("An optional value to pass to the ViewModelFactory when creating the view model within this scope.")]
        public object FactoryContext
        {
            get { return (object)GetValue(FactoryContextProperty); }
            set { SetValue(FactoryContextProperty, value); }
        }

        public static readonly DependencyProperty FactoryContextProperty = DependencyProperty.Register("FactoryContext", typeof(object), typeof(ViewModelScope), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the singleton factory for creating view models.
        /// </summary>
        /// <remarks>
        /// This factory allows for the Inversion of Control (IoC) pattern with dependency injection containers.
        /// The default is the DefaultViewModelFactory implementation
        /// </remarks>
        public static IViewModelFactory ViewModelFactory { get; set; }

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (base.Child != value)
                {
                    UIElement oldChild = base.Child;
                    base.Child = value;
                    this.OnChildChanged(oldChild, base.Child);
                }
            }
        }

        private void OnChildChanged(UIElement oldValue, UIElement newValue)
        {
            FrameworkElement oldChildElement = oldValue as FrameworkElement;
            if (oldChildElement != null)
            {
                oldChildElement.DataContext = null;
            }

            this.AttachViewModel();
        }

        private void ViewModelScope_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged += this.ViewModelScope_DataContextChanged;
            this.AttachViewModel();
        }

        void ViewModelScope_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged -= this.ViewModelScope_DataContextChanged;
            this.DetachViewModel();
            this._viewModel = null;
        }

        private void ViewModelScope_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateModel();
        }

        private void AttachViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            if (this.ViewModelType != null && this._viewModel == null)
            {
                this._viewModel = ViewModelFactory.Create(this.ViewModelType, this.FactoryContext);
            }

            if (this._viewModel != null)
            {
                this._viewModel.Model = this.DataContext;
                this._viewModel.View = this.Child;
            }

            FrameworkElement childElement = this.Child as FrameworkElement;
            if (childElement != null)
            {
                childElement.DataContext = this._viewModel;
            }
        }

        private void DetachViewModel()
        {
            FrameworkElement childElement = this.Child as FrameworkElement;
            if (childElement != null)
            {
                childElement.DataContext = null;
            }

            if (this._viewModel != null)
            {
                this._viewModel.View = null;
                this._viewModel.Model = null;
            }
        }

        private void UpdateModel()
        {
            if (this._viewModel != null)
            {
                // I handle a special case where the DataContext becomes {DisconnectedItem}
                // http://social.msdn.microsoft.com/Forums/en/wpf/thread/e6643abc-4457-44aa-a3ee-dd389c88bd86
                // https://connect.microsoft.com/VisualStudio/feedback/details/619658/wpf-virtualized-control-disconnecteditem-reference-when-datacontext-switch
                if (this.DataContext != null && this.DataContext.GetType().FullName == "MS.Internal.NamedObject")
                {
                    this._viewModel.Model = null;
                }
                else
                {
                    this._viewModel.Model = this.DataContext;
                }
            }
        }
    }
}
