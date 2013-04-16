namespace BellaCode.Mvvm.Actions
{
    using BellaCode.Mvvm;
    using Microsoft.Win32;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Displays the save file dialog.
    /// </summary>
    public class SaveFileDialogAction : TriggerAction<UIElement>
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SaveFileDialogAction), new FrameworkPropertyMetadata("Save"));

        public string FilesFilter
        {
            get { return (string)GetValue(FilesFilterProperty); }
            set { SetValue(FilesFilterProperty, value); }
        }

        public static readonly DependencyProperty FilesFilterProperty = DependencyProperty.Register("FilesFilter", typeof(string), typeof(SaveFileDialogAction), new FrameworkPropertyMetadata("All Files(*.*)|*.*"));

        public string DefaultFileName
        {
            get { return (string)GetValue(DefaultFileNameProperty); }
            set { SetValue(DefaultFileNameProperty, value); }
        }

        public static readonly DependencyProperty DefaultFileNameProperty = DependencyProperty.Register("DefaultFileName", typeof(string), typeof(SaveFileDialogAction), new FrameworkPropertyMetadata(null));

        public string DefaultExtension
        {
            get { return (string)GetValue(DefaultExtensionProperty); }
            set { SetValue(DefaultExtensionProperty, value); }
        }

        public static readonly DependencyProperty DefaultExtensionProperty = DependencyProperty.Register("DefaultExtension", typeof(string), typeof(SaveFileDialogAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            var eventArgs = (InteractionEventArgs)parameter;

            string initialFileName = eventArgs.Data as string;

            if (string.IsNullOrEmpty(initialFileName))
            {
                initialFileName = this.DefaultFileName;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = this.Title;            
            dialog.FileName = initialFileName;
            dialog.Filter = this.FilesFilter; //"Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            dialog.DefaultExt = this.DefaultExtension;

            eventArgs.Result = (dialog.ShowDialog() == true) ? dialog.FileName : null;
        }
    }
}
