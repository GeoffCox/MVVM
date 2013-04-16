namespace BellaCode.Mvvm.Actions
{
    using BellaCode.Mvvm;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Displays the specified Yes/No question and optional caption and returns the dialog result.
    /// </summary>
    /// <remarks>
    /// To retrieve the result from the dialog, the parameter must be an InteractionEventArgs.
    /// If TData is a string, it will be used as the question rather than the Question dependency property.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class AskQuestionAction : TriggerAction<UIElement>
    {        
        public string Question
        {
            get { return (string)GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }

        public static readonly DependencyProperty QuestionProperty = DependencyProperty.Register("Question", typeof(string), typeof(AskQuestionAction), new FrameworkPropertyMetadata(null));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(AskQuestionAction), new FrameworkPropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionEventArgs<string>;
            string question = e != null ? e.Data : null;
            string caption = this.Caption ?? string.Empty;
            
            if (string.IsNullOrEmpty(question))
            {
                question = this.Question;
            }

            var messageBoxResult = MessageBox.Show(question, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            var result = parameter as InteractionEventArgs;
            if (result != null)
            {
                result.Result = messageBoxResult;
            }
        }
    }
}

