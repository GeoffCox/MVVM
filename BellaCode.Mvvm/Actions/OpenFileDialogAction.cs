namespace BellaCode.Mvvm.Actions
{
    using BellaCode.Mvvm;
    using Microsoft.Win32;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Displays the open file dialog.
    /// </summary>
    public class OpenFileDialogAction : TriggerAction<UIElement>
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(OpenFileDialogAction), new FrameworkPropertyMetadata("Save"));

        public string FilesFilter
        {
            get { return (string)GetValue(FilesFilterProperty); }
            set { SetValue(FilesFilterProperty, value); }
        }

        public static readonly DependencyProperty FilesFilterProperty = DependencyProperty.Register("FilesFilter", typeof(string), typeof(OpenFileDialogAction), new FrameworkPropertyMetadata("All Files(*.*)|*.*"));       

        public string DefaultExtension
        {
            get { return (string)GetValue(DefaultExtensionProperty); }
            set { SetValue(DefaultExtensionProperty, value); }
        }

        public static readonly DependencyProperty DefaultExtensionProperty = DependencyProperty.Register("DefaultExtension", typeof(string), typeof(OpenFileDialogAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            var eventArgs = (InteractionEventArgs)parameter;            

            var dialog = new OpenFileDialog();
            dialog.Title = this.Title;                        
            dialog.Filter = this.FilesFilter; //"Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            dialog.DefaultExt = this.DefaultExtension;

            eventArgs.Result = (dialog.ShowDialog() == true) ? dialog.FileName : null;
        }
    }
}
