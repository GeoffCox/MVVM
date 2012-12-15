namespace BellaCode.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Media;

    /// <summary>
    /// Binds a command to a method for executing the command and a property or method for determining if the command can execute.
    /// </summary>
    /// <remarks>
    /// By convention, the methods and properties used are based on the command name.
    /// For example, if the command is named SaveCommand, then the behavior looks for a CanSave method/property and a Save method.
    /// For CanSave, the behavior looks for a matching method, then a matching property.
    /// For CanSave, if no property or method is found then the command is allowed to execute.
    /// </remarks>
    public class BindCommandBehavior : Behavior<FrameworkElement>
    {
        private CommandBinding _commandBinding;

        private INotifyPropertyChanged _notifyPropertyChanged;

        public BindCommandBehavior()
        {
        }

        [Category("Common Properties")]
        [Description("The command to bind to the methods in the data context.")]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(BindCommandBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BindCommandBehavior)d).OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue)));

        private void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            this.UpdateCommandBinding();
        }

        [Category("Common Properties")]
        [Description("The optional name of the execute method to call when the command Execute is called.")]
        public string ExecuteMethodName
        {
            get { return (string)GetValue(ExecuteMethodNameProperty); }
            set { SetValue(ExecuteMethodNameProperty, value); }
        }

        public static readonly DependencyProperty ExecuteMethodNameProperty = DependencyProperty.Register("ExecuteMethodName", typeof(string), typeof(BindCommandBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BindCommandBehavior)d).OnExecuteMethodNameChanged((string)e.OldValue, (string)e.NewValue)));

        private void OnExecuteMethodNameChanged(string oldValue, string newValue)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        [Category("Common Properties")]
        [Description("The optional name of the method to use when the command CanExecute is called.")]
        public string CanExecuteMethodName
        {
            get { return (string)GetValue(CanExecuteMethodNameProperty); }
            set { SetValue(CanExecuteMethodNameProperty, value); }
        }

        public static readonly DependencyProperty CanExecuteMethodNameProperty = DependencyProperty.Register("CanExecuteMethodName", typeof(string), typeof(BindCommandBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BindCommandBehavior)d).OnCanExecuteMethodNameChanged((string)e.OldValue, (string)e.NewValue)));

        private void OnCanExecuteMethodNameChanged(string oldValue, string newValue)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        [Category("Common Properties")]
        [Description("The optional name of the property to use when the command CanExecute is called.")]
        public string CanExecutePropertyName
        {
            get { return (string)GetValue(CanExecutePropertyNameProperty); }
            set { SetValue(CanExecutePropertyNameProperty, value); }
        }

        public static readonly DependencyProperty CanExecutePropertyNameProperty = DependencyProperty.Register("CanExecutePropertyName", typeof(string), typeof(BindCommandBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((BindCommandBehavior)d).OnCanExecutePropertyNameChanged((string)e.OldValue, (string)e.NewValue)));

        private void OnCanExecutePropertyNameChanged(string oldValue, string newValue)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.DataContextChanged += this.AssociatedObject_DataContextChanged;
            this.UpdateNotifyPropertyChanged();
            this.UpdateCommandBinding();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.DataContextChanged -= this.AssociatedObject_DataContextChanged;
            base.OnDetaching();
        }

        public void CanExecuteCommand(Object sender, CanExecuteRoutedEventArgs e)
        {
            var target = this.AssociatedObject.DataContext;
            if (target != null)
            {
                e.CanExecute = CanExecute(target, e.Parameter);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void ExecuteCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            var target = this.AssociatedObject.DataContext;
            if (target != null)
            {
                Execute(target, e.Parameter);
            }
            else
            {
                Debug.WriteLine("BindCommandBehavior could not execute the command. DataContext is null.");
            }
        }

        private bool CanExecute(object target, object parameter)
        {
            Debug.Assert(target != null);

            bool? returnValue = true;

            var methodInfo = GetCanExecuteMethodInfo(target);
            if (methodInfo != null)
            {
                returnValue = Invoke(methodInfo, target, parameter) as bool?;
            }
            else
            {
                var propertyInfo = GetCanExecutePropertyInfo(target);
                if (propertyInfo != null)
                {
                    returnValue = propertyInfo.GetValue(target, null) as bool?;
                }
            }

            return (returnValue == true);
        }

        private void Execute(object target, object parameter)
        {
            EndEditing();

            Debug.Assert(target != null);

            var methodInfo = GetExecuteMethodInfo(target);

            if (methodInfo != null)
            {
                Invoke(methodInfo, target, parameter);
            }
        }

        private void UpdateCommandBinding()
        {
            if (this._commandBinding != null)
            {
                this.AssociatedObject.CommandBindings.Remove(this._commandBinding);
            }

            if (this.Command != null && this.AssociatedObject != null)
            {
                this._commandBinding = new CommandBinding(this.Command, this.ExecuteCommand, this.CanExecuteCommand);
                this.AssociatedObject.CommandBindings.Add(this._commandBinding);
            }
            else
            {
                this._commandBinding = null;
            }
        }

        private void UpdateNotifyPropertyChanged()
        {
            if (this._notifyPropertyChanged != null)
            {
                _notifyPropertyChanged.PropertyChanged -= new PropertyChangedEventHandler(AssociatedObject_PropertyChanged);
            }

            this._notifyPropertyChanged = this.AssociatedObject.DataContext as INotifyPropertyChanged;

            if (this._notifyPropertyChanged != null)
            {
                _notifyPropertyChanged.PropertyChanged += new PropertyChangedEventHandler(AssociatedObject_PropertyChanged);
            }
        }

        private PropertyInfo GetCanExecutePropertyInfo(object target)
        {
            PropertyInfo propertyInfo = null;

            if (!string.IsNullOrEmpty(this.CanExecutePropertyName))
            {
                // I use the property name if provided
                propertyInfo = target.GetType().GetProperty(this.CanExecutePropertyName);
            }
            else if (!string.IsNullOrEmpty(this.ExecuteMethodName))
            {
                // I use a convention based on the execute method name
                propertyInfo = target.GetType().GetProperty("Can" + this.ExecuteMethodName);
            }
            else
            {
                // I use a convention based on the command name
                string commandName = GetCommandName();
                if (!string.IsNullOrEmpty(commandName))
                {
                    propertyInfo = target.GetType().GetProperty("Can" + commandName);
                }
            }

            return propertyInfo;
        }

        private MethodInfo GetCanExecuteMethodInfo(object target)
        {
            MethodInfo methodInfo = null;

            if (!string.IsNullOrEmpty(this.CanExecuteMethodName))
            {
                // I use the method name if provided
                methodInfo = target.GetType().GetMethod(this.CanExecuteMethodName);
            }
            else if (!string.IsNullOrEmpty(this.ExecuteMethodName))
            {
                // I use a convention based on the execute method name
                methodInfo = target.GetType().GetMethod("Can" + this.ExecuteMethodName);
            }
            else
            {
                // I use a convention based on the command name
                string commandName = GetCommandName();
                if (!string.IsNullOrEmpty(commandName))
                {
                    methodInfo = target.GetType().GetMethod("Can" + commandName);
                }
            }

            return methodInfo;
        }

        private MethodInfo GetExecuteMethodInfo(object target)
        {
            MethodInfo methodInfo = null;

            if (!string.IsNullOrEmpty(this.ExecuteMethodName))
            {
                // I use the method name if provided                
                methodInfo = target.GetType().GetMethod(this.ExecuteMethodName);
            }
            else
            {
                // I use a convention based on the command name
                string commandName = GetCommandName();
                if (!string.IsNullOrEmpty(commandName))
                {
                    methodInfo = target.GetType().GetMethod(commandName);
                }
            }

            return methodInfo;
        }

        private object Invoke(MethodInfo methodInfo, object target, object parameter)
        {
            Debug.Assert(methodInfo != null);
            Debug.Assert(target != null);

            // If the method takes one parameter, I try to pass the command parameter to it.
            var parameterInfos = methodInfo.GetParameters();
            if (parameterInfos.Length == 1)
            {
                if (parameter != null)
                {
                    object parameterValue = SoftConvert(parameter, parameterInfos[0].ParameterType);
                    return methodInfo.Invoke(target, new object[] { parameterValue });
                }
                else
                {
                    return methodInfo.Invoke(target, new object[] { null });
                }
            }
            else
            {
                return methodInfo.Invoke(target, null);
            }
        }

        private string GetCommandName()
        {
            RoutedCommand routedCommand = this.Command as RoutedCommand;
            if (routedCommand != null && !string.IsNullOrEmpty(routedCommand.Name))
            {
                return routedCommand.Name;
            }
            else
            {
                return null;
            }
        }

        // If try to convert the command parameter types to the method parameter types using this convert method.
        private object SoftConvert(object value, Type destinationType)
        {
            if (value == null || destinationType == null)
            {
                return value;
            }

            Type sourceType = value.GetType();

            if (!destinationType.IsAssignableFrom(sourceType))
            {
                // I try to use the standard converter.
                try
                {
                    return Convert.ChangeType(value, destinationType);
                }
                catch (InvalidCastException)
                {
                    // Failed to convert, continue.
                }
                catch (FormatException)
                {
                    // Failed to convert, continue.
                }
                catch (OverflowException)
                {
                    // Failed to convert, continue.
                }
            }

            return value;
        }

        private void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateNotifyPropertyChanged();
            CommandManager.InvalidateRequerySuggested();
        }

        private void AssociatedObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // If all properties changed, or property name matches CanExecute names, then requery
            if (string.IsNullOrEmpty(e.PropertyName) ||
                e.PropertyName == this.CanExecutePropertyName ||
                e.PropertyName == this.CanExecuteMethodName)
            {
                CommandManager.InvalidateRequerySuggested();
            }
            // property name matches Execute method name convention
            else if (!string.IsNullOrEmpty(this.ExecuteMethodName) &&
                e.PropertyName == "Can" + this.ExecuteMethodName)
            {
                CommandManager.InvalidateRequerySuggested();
            }
            // property name matches command name convention
            string commandName = this.GetCommandName();
            if (e.PropertyName == "Can" + commandName)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        // When shortcuts to commands are pressed, textboxes don't lose focus and don't update their sources.
        // This method updates the source.  
        // This should update the command parameter value as the parameter is passed in by reference.
        private static void EndEditing()
        {
            IInputElement focusedElement = Keyboard.FocusedElement;

            if ((focusedElement is TextBox) ||
                (focusedElement is RichTextBox) ||
                (focusedElement is PasswordBox))
            {
                DependencyObject dependencyObject = focusedElement as DependencyObject;
                if (dependencyObject != null)
                {
                    EndEditing(dependencyObject);
                }
            }
        }

        private static void EndEditing(DependencyObject dependencyObject)
        {
            Debug.Assert(dependencyObject != null);

            // When in custom control templates, the GetLocalValueEnumerator doesn't return bound values to the templated parent, so I iterate All properties.
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dependencyObject, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }))
            {
                DependencyPropertyDescriptor dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(propertyDescriptor);
                if (dependencyPropertyDescriptor != null)
                {
                    DependencyProperty dependencyProperty = dependencyPropertyDescriptor.DependencyProperty;

                    if (BindingOperations.IsDataBound(dependencyObject, dependencyProperty))
                    {
                        BindingExpression binding = BindingOperations.GetBindingExpression(dependencyObject, dependencyProperty);
                        if (binding != null)
                        {
                            binding.UpdateSource();
                        }
                    }
                }
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject childDependencyObject = VisualTreeHelper.GetChild(dependencyObject, i);
                EndEditing(childDependencyObject);
            }
        }
    }
}
