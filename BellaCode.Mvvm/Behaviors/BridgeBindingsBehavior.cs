namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    /// <summary>
    /// Binds a property on the associated FrameworkElement to another property in the logical tree.    
    /// </summary>
    /// <remarks>
    /// This behavior is necessary when a view model property needs to bound to a value that is not part of the model (DataContext) or directly within the current logical tree.
    /// For example, a view model may need to know if a filter value is set within a toolbar control that is a sibling to the view:
    /// &lt;BridgeBind
    /// </remarks>
    public class BridgeBindingsBehavior : Behavior<FrameworkElement>
    {
        [Category("Common Properties")]
        [Description("The source value to bridge to the target.")]
        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(BridgeBindingsBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BridgeBindingsBehavior)d).OnSourceChanged((object)e.OldValue, (object)e.NewValue)));

        private void OnSourceChanged(object oldValue, object newValue)
        {
            this.UpdateTarget();
        }

        [Category("Common Properties")]
        [Description("The target value to bridge from the source.")]
        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(object), typeof(BridgeBindingsBehavior), new FrameworkPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += this.AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded;
            base.OnDetaching();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateTarget();
        }

        private void UpdateTarget()
        {
            var targetBinding = BindingOperations.GetBinding(this, TargetProperty);
            if (targetBinding.Mode != BindingMode.OneWayToSource)
            {
                throw new InvalidOperationException("The BridgeBindingsBehavior.Target BindingModel is not OneWayToSource.");
            }

            this.Target = this.Source;
        }
    }
}
