namespace BellaCode.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Diagnostics;
    using System.Collections.Generic;

    /// <summary>
    /// Substitutes a view model for the DataContext on the decorated control.
    /// </summary>
    public class ViewModelScope : Decorator
    {
        /// <summary>
        /// Initializes the <see cref="ViewModelScope"/> class.
        /// </summary>
        static ViewModelScope()
        {
            ViewModelFactory = new DefaultViewModelFactory();
        }

        private IViewModel viewModel;

        public ViewModelScope()
        {
            this.Loaded += this.ViewModelScope_Loaded;
            this.Unloaded += this.ViewModelScope_Unloaded;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the factory for creating view models.
        /// </summary>
        /// <remarks>
        /// The default is the DefaultViewModelFactory implementation.
        /// </remarks>
        public static IViewModelFactory ViewModelFactory { get; set; }

        /// <summary>
        /// Gets or sets the type of the view model to use.
        /// </summary>      
        public Type ViewModelType { get; set; }

        /// <summary>
        /// Gets or sets the single child element of a <see cref="T:System.Windows.Controls.Decorator"/>.
        /// </summary>
        /// <returns>The single child element of a <see cref="T:System.Windows.Controls.Decorator"/>.</returns>
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

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the context to pass to the ViewModelFactory.Create method.
        /// </summary>
        /// <value>
        /// The factory context.
        /// </value>
        [Category("Common Properties")]
        [Description("An optional additional value to pass to the ViewModelFactory when creating IViewModel instances")]
        public object FactoryContext
        {
            get { return (object)GetValue(FactoryContextProperty); }
            set { SetValue(FactoryContextProperty, value); }
        }

        public static readonly DependencyProperty FactoryContextProperty = DependencyProperty.Register("FactoryContext", typeof(object), typeof(ViewModelScope), new FrameworkPropertyMetadata(null));

        #endregion

        #region UI Event Handlers

        private void ViewModelScope_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged += this.ViewModelScope_DataContextChanged;
            this.AttachViewModel();
        }

        void ViewModelScope_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged -= this.ViewModelScope_DataContextChanged;
            this.DetachViewModel();
            this.viewModel = null;
        }

        private void ViewModelScope_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateModel();
        }

        #endregion

        #region Methods

        private void OnChildChanged(UIElement oldValue, UIElement newValue)
        {
            FrameworkElement oldChildElement = oldValue as FrameworkElement;
            if (oldChildElement != null)
            {
                oldChildElement.DataContext = null;
            }

            this.AttachViewModel();
        }

        private void AttachViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            if (this.ViewModelType != null && this.viewModel == null)
            {
                this.viewModel = ViewModelFactory.Create(this.ViewModelType, this.FactoryContext);
            }

            if (this.viewModel != null)
            {
                this.viewModel.Model = this.DataContext;
                this.viewModel.View = this.Child;
            }

            FrameworkElement childElement = this.Child as FrameworkElement;
            if (childElement != null)
            {
                childElement.DataContext = this.viewModel;
            }
        }

        private void DetachViewModel()
        {
            FrameworkElement childElement = this.Child as FrameworkElement;
            if (childElement != null)
            {
                childElement.DataContext = null;
            }

            if (this.viewModel != null)
            {
                this.viewModel.View = null;
                this.viewModel.Model = null;
            }
        }

        private void UpdateModel()
        {
            if (this.viewModel != null)
            {
                // I handle a special case where the DataContext becomes {DisconnectedItem}
                // http://social.msdn.microsoft.com/Forums/en/wpf/thread/e6643abc-4457-44aa-a3ee-dd389c88bd86
                // https://connect.microsoft.com/VisualStudio/feedback/details/619658/wpf-virtualized-control-disconnecteditem-reference-when-datacontext-switch
                if (this.DataContext != null && this.DataContext.GetType().FullName == "MS.Internal.NamedObject")
                {
                    this.viewModel.Model = null;
                }
                else
                {
                    this.viewModel.Model = this.DataContext;
                }
            }
        }

        #endregion
    }
}
