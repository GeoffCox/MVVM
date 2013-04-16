namespace BellaCode.Mvvm
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Provides helpful methods for finding parents and children within the visual tree.
    /// </summary>
    public static class VisualTreeHelperT
    {
        public static T FindVisualChild<T>(object obj) where T : DependencyObject
        {
            var current = obj as DependencyObject;

            if (current != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child != null && child is T)
                        return (T)child;
                    else
                    {
                        T childOfChild = FindVisualChild<T>(child);
                        if (childOfChild != null)
                            return childOfChild;
                    }
                }
            }
            return null;
        }

        public static T FindVisualParent<T>(object obj) where T : DependencyObject
        {
            var current = obj as DependencyObject;            

            while (current != null)
            {
                var item = current as T;
                if (item != null && current != obj)
                {
                    return item;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static T FindVisualParentOrSelf<T>(object obj) where T : DependencyObject
        {
            var current = obj as DependencyObject;

            while (current != null)
            {
                var item = current as T;
                if (item != null)
                {
                    return item;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }
    }
}
