namespace BellaCode.Mvvm.Behaviors
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Refreshes a dependency property after the associated object has loaded.
    /// </summary>
    /// <remarks>
    /// This can be very useful with PropertyChangedTriggers where the initial state is already set, so the trigger doesn't fire.
    /// The default property to refresh is the entire DataContext.
    /// </remarks>
    public class RefreshBindingAfterLoadBehavior : Behavior<FrameworkElement>
    {
        public RefreshBindingAfterLoadBehavior()
        {
            this.PropertyToRefresh = FrameworkElement.DataContextProperty;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Loaded += this.AssociatedObject_Loaded;
        }      

        /// <summary>
        /// Gets or sets the property to refresh.  The default is FrameworkElement.DataContextProperty
        /// </summary>
        /// <value>
        /// The property to refresh.
        /// </value>
        public DependencyProperty PropertyToRefresh { get; set; }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded;

            if (this.PropertyToRefresh != null)
            {
                var value = this.AssociatedObject.GetValue(this.PropertyToRefresh);

                var foregroundTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                // I asynchronously delay to set to null and to the value.
                // This allows the associated object to finish loading before the property is refreshed.
                Task.Factory.StartNew(() => { System.Threading.Thread.Sleep(10); })
                    .ContinueWith(t =>
                        {
                            this.AssociatedObject.SetCurrentValue(this.PropertyToRefresh, null);
                            this.AssociatedObject.SetCurrentValue(this.PropertyToRefresh, value);
                        }, foregroundTaskScheduler);
            }
        }
    }
}
