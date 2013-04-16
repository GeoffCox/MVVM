namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// Extends the behavior of a GridSplitter to reset it to initial position when double-clicked.
    /// </summary>
    public class ResettableGridSplitterBehavior : Behavior<GridSplitter>
    {
        private Grid grid;
        private GridSplitter gridSplitter;
        private GridLength? length1;
        private GridLength? length2;

        private static Dictionary<GridSplitter, ResettableGridSplitterBehavior> behaviors = new Dictionary<GridSplitter, ResettableGridSplitterBehavior>();

        static ResettableGridSplitterBehavior()
        {
            // I register to know when the GridResizeDirection has changed
            GridSplitter.ResizeDirectionProperty.OverrideMetadata(typeof(ResettableGridSplitterBehavior),
                 new FrameworkPropertyMetadata((d, e) => OnResizeDirectionChanged((GridSplitter)d, (GridResizeDirection)e.OldValue, (GridResizeDirection)e.NewValue)));

            // I register to know when the GridResizeBehavior has changed
            GridSplitter.ResizeBehaviorProperty.OverrideMetadata(typeof(ResettableGridSplitterBehavior),
                 new FrameworkPropertyMetadata((d, e) => OnResizeBehaviorChanged((GridSplitter)d, (GridResizeBehavior)e.OldValue, (GridResizeBehavior)e.NewValue)));
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.gridSplitter = this.AssociatedObject as GridSplitter;

            if (this.gridSplitter != null)
            {
                behaviors[this.gridSplitter] = this;
                this.gridSplitter.Loaded += this.GridSplitter_Loaded;
                this.gridSplitter.MouseDoubleClick += this.GridSplitter_MouseDoubleClick;                
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (this.gridSplitter != null)
            {
                behaviors.Remove(this.gridSplitter);
            }

            base.OnDetaching();
        }

        #region Dependency Property Callbacks

        private static void OnResizeDirectionChanged(GridSplitter gridSplitter, GridResizeDirection oldValue, GridResizeDirection newValue)
        {
            ResettableGridSplitterBehavior behavior;
            if (behaviors.TryGetValue(gridSplitter, out behavior))
            {
                behavior.OnResizeDirectionChanged(oldValue, newValue);
            }
        }

        private void OnResizeDirectionChanged(GridResizeDirection oldValue, GridResizeDirection newValue)
        {
            this.SnapshotSplitterPosition();
        }

        private static void OnResizeBehaviorChanged(GridSplitter gridSplitter, GridResizeBehavior oldValue, GridResizeBehavior newValue)
        {
            ResettableGridSplitterBehavior behavior;
            if (behaviors.TryGetValue(gridSplitter, out behavior))
            {
                behavior.OnResizeBehaviorChanged(oldValue, newValue);
            }
        }

        private void OnResizeBehaviorChanged(GridResizeBehavior oldValue, GridResizeBehavior newValue)
        {
            this.SnapshotSplitterPosition();
        }

        #endregion

        #region Snapshot/Restore Methods

        private void SnapshotSplitterPosition()
        {
            if (DesignerProperties.GetIsInDesignMode(this.gridSplitter))
            {
                return;
            }

            GridResizeDirection effectiveResizeDirection = GetEffectiveResizeDirection();
            switch (effectiveResizeDirection)
            {
                case GridResizeDirection.Columns:
                    this.SnapshotColumnWidths();
                    break;
                case GridResizeDirection.Rows:
                    this.SnapshotRowHeights();
                    break;
            }
        }

        private void SnapshotColumnWidths()
        {
            this.length1 = null;
            this.length2 = null;

            if (this.grid != null)
            {
                var indices = this.GetDefinitionIndices();

                if (indices.Item1.HasValue)
                {
                    this.length1 = this.grid.ColumnDefinitions[indices.Item1.Value].Width;
                }

                if (indices.Item2.HasValue)
                {
                    this.length2 = this.grid.ColumnDefinitions[indices.Item2.Value].Width;
                }
            }
        }

        private void SnapshotRowHeights()
        {
            this.length1 = null;
            this.length2 = null;

            if (this.grid != null)
            {
                var indices = this.GetDefinitionIndices();

                if (indices.Item1.HasValue)
                {
                    this.length1 = grid.RowDefinitions[indices.Item1.Value].Height;
                }

                if (indices.Item2.HasValue)
                {
                    this.length2 = grid.RowDefinitions[indices.Item2.Value].Height;
                }
            }
        }

        private void RestoreSplitterPosition()
        {
            if (DesignerProperties.GetIsInDesignMode(this.gridSplitter))
            {
                return;
            }

            GridResizeDirection effectiveResizeDirection = GetEffectiveResizeDirection();
            switch (effectiveResizeDirection)
            {
                case GridResizeDirection.Columns:
                    this.RestoreColumnWidths();
                    break;
                case GridResizeDirection.Rows:
                    this.RestoreRowHeights();
                    break;
            }
        }

        private void RestoreColumnWidths()
        {
            if (this.grid != null)
            {
                var indices = this.GetDefinitionIndices();

                if ((indices.Item1 != null) && (this.length1.HasValue))
                {
                    grid.ColumnDefinitions[indices.Item1.Value].Width = this.length1.Value;
                }

                if ((indices.Item2 != null) && (this.length2.HasValue))
                {
                    grid.ColumnDefinitions[indices.Item2.Value].Width = this.length2.Value;
                }
            }
        }

        private void RestoreRowHeights()
        {
            if (this.grid != null)
            {
                var indices = this.GetDefinitionIndices();

                if ((indices.Item1.HasValue) && (this.length1.HasValue))
                {
                    grid.RowDefinitions[indices.Item1.Value].Height = this.length1.Value;
                }

                if ((indices.Item2.HasValue) && (this.length2.HasValue))
                {
                    grid.RowDefinitions[indices.Item2.Value].Height = this.length2.Value;
                }
            }
        }

        // Determines the indices for accessign ColumnDefinitions or RowDefinitions based on resize direction and behavior
        private Tuple<int?, int?> GetDefinitionIndices()
        {
            GridResizeDirection effectiveResizeDirection = this.GetEffectiveResizeDirection();
            GridResizeBehavior effectiveResizeBehavior = this.GetEffectiveResizeBehavior(effectiveResizeDirection);

            int currentIndex;
            int count;
            switch (effectiveResizeDirection)
            {
                case GridResizeDirection.Columns:
                    currentIndex = (int)this.gridSplitter.GetValue(Grid.ColumnProperty);
                    count = this.grid.ColumnDefinitions.Count;
                    break;
                case GridResizeDirection.Rows:
                    currentIndex = (int)this.gridSplitter.GetValue(Grid.RowProperty);
                    count = this.grid.RowDefinitions.Count;
                    break;
                default:
                    return new Tuple<int?, int?>(null, null);
            }

            int? length1Index = currentIndex;
            int? length2Index = currentIndex;

            switch (effectiveResizeBehavior)
            {
                case GridResizeBehavior.CurrentAndNext:
                    length2Index++;
                    break;
                case GridResizeBehavior.PreviousAndCurrent:
                    length1Index--;
                    break;
                case GridResizeBehavior.PreviousAndNext:
                    length2Index++;
                    length1Index--;
                    break;
            }

            if (length1Index < 0 || length1Index >= count)
            {
                length1Index = null;
            }

            if (length2Index < 0 || length2Index >= count)
            {
                length2Index = null;
            }

            return new Tuple<int?, int?>(length1Index, length2Index);
        }

        #endregion

        // I mimic the logic from the GridSplitter class that are hidden in a private method
        #region Direction/Behavior Methods

        private GridResizeDirection GetEffectiveResizeDirection()
        {
            GridResizeDirection resizeDirection = this.gridSplitter.ResizeDirection;
            if (resizeDirection != GridResizeDirection.Auto)
            {
                return resizeDirection;
            }
            if (this.gridSplitter.HorizontalAlignment != HorizontalAlignment.Stretch)
            {
                return GridResizeDirection.Columns;
            }
            if ((this.gridSplitter.VerticalAlignment == VerticalAlignment.Stretch) && (this.gridSplitter.ActualWidth <= this.gridSplitter.ActualHeight))
            {
                return GridResizeDirection.Columns;
            }
            return GridResizeDirection.Rows;
        }

        private GridResizeBehavior GetEffectiveResizeBehavior()
        {
            return GetEffectiveResizeBehavior(GetEffectiveResizeDirection());
        }

        private GridResizeBehavior GetEffectiveResizeBehavior(GridResizeDirection direction)
        {
            GridResizeBehavior resizeBehavior = this.gridSplitter.ResizeBehavior;
            if (resizeBehavior != GridResizeBehavior.BasedOnAlignment)
            {
                return resizeBehavior;
            }
            if (direction != GridResizeDirection.Columns)
            {
                switch (this.gridSplitter.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        return GridResizeBehavior.PreviousAndCurrent;

                    case VerticalAlignment.Center:
                        return GridResizeBehavior.PreviousAndNext;

                    case VerticalAlignment.Bottom:
                        return GridResizeBehavior.CurrentAndNext;
                }
            }
            else
            {
                switch (this.gridSplitter.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        return GridResizeBehavior.PreviousAndCurrent;

                    case HorizontalAlignment.Right:
                        return GridResizeBehavior.CurrentAndNext;
                }
                return GridResizeBehavior.PreviousAndNext;
            }

            return GridResizeBehavior.PreviousAndNext;
        }

        #endregion

        #region UI Event Handlers

        private void GridSplitter_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this.gridSplitter))
            {
                this.grid = this.gridSplitter.Parent as Grid;
                this.SnapshotSplitterPosition();
            }
        }

        private void GridSplitter_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this.gridSplitter))
            {
                this.RestoreSplitterPosition();
            }
        }

        #endregion
    }
}
