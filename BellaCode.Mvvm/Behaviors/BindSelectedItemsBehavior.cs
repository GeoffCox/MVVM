namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    /// <summary>
    /// Provides (fast) binding to a any Selector derived class (e.g. ListBox, ListView, etc.) SelectedItems property.
    /// </summary>
    public class BindSelectedItemsBehavior : Behavior<Selector>
    {
        private ListBox associatedListBox;
        private MultiSelector associatedMultiSelector;

        private ListWrapper wrapper1 = new ListWrapper();
        private ListWrapper wrapper2 = new ListWrapper();

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList), typeof(BindSelectedItemsBehavior), new FrameworkPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();

            var binding = BindingOperations.GetBinding(this, SelectedItemsProperty);
            if (binding.Mode != BindingMode.OneWayToSource)
            {
                throw new InvalidOperationException("SelectedItems binding mode must be OneWayToSource");
            }

            this.associatedListBox = this.AssociatedObject as ListBox;
            this.associatedMultiSelector = this.AssociatedObject as MultiSelector;
            
            this.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
            this.UpdateSelectedItems();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= this.AssociatedObject_SelectionChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateSelectedItems();                                     
        }

        private void UpdateSelectedItems()
        {
            if (this.associatedListBox != null)
            {
                this.wrapper1.List = this.associatedListBox.SelectedItems;
                this.wrapper2.List = this.associatedListBox.SelectedItems;
            }
            else if (this.associatedMultiSelector != null)
            {
                this.wrapper1.List = this.associatedMultiSelector.SelectedItems;
                this.wrapper2.List = this.associatedMultiSelector.SelectedItems;
            }
            else
            {
                var selectedItemsList = new List<object>(new object[] { this.AssociatedObject.SelectedItem });
                this.wrapper1.List = selectedItemsList;
                this.wrapper2.List = selectedItemsList;
            }

            // I toggle between the two wrapper instances each time selection changes to ensure that
            // the target SelectedItems change notifications occur.
            if (object.ReferenceEquals(this.SelectedItems, this.wrapper1))
            {
                this.SelectedItems = wrapper2;
            }
            else
            {
                this.SelectedItems = wrapper1;
            }
        }

        private class ListWrapper : IList
        {
            public IList List {get; set;}

            #region IList (delegate to this.selectedItems)

            public int Count
            {
                get { return this.List.Count; }
            }

            public bool IsFixedSize
            {
                get { return this.List.IsFixedSize; }
            }

            public bool IsReadOnly
            {
                get { return this.List.IsReadOnly; }
            }

            public bool IsSynchronized
            {
                get { return this.List.IsSynchronized; }
            }

            public object SyncRoot
            {
                get { return this.List.SyncRoot; }
            }

            public int Add(object value)
            {
                return this.List.Add(value);
            }

            public void Clear()
            {
                this.List.Clear();
            }

            public bool Contains(object value)
            {
                return this.List.Contains(value);
            }

            public int IndexOf(object value)
            {
                return this.List.IndexOf(value);
            }

            public void Insert(int index, object value)
            {
                this.List.Insert(index, value);
            }

            public void Remove(object value)
            {
                this.List.Remove(value);
            }

            public void RemoveAt(int index)
            {
                this.List.RemoveAt(index);
            }

            public object this[int index]
            {
                get
                {
                    return this.List[index];
                }
                set
                {
                    this.List[index] = value;
                }
            }

            public void CopyTo(System.Array array, int index)
            {
                this.List.CopyTo(array, index);
            }

            public IEnumerator GetEnumerator()
            {
                return this.List.GetEnumerator();
            }

            #endregion
        }
    }
}
