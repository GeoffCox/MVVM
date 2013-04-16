namespace BellaCode.Mvvm.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Registers KeyGesture InputBindings at application scope.
    /// </summary>
    /// <remarks>
    /// This sets the associated object as the command target automatically.
    /// This only calls the command when the associated object is visible.
    /// This only supports Gestures that include modifier keys (Ctrl, Alt, Shift)
    /// This passes the InputBinding.CommandParameter when executing the command.
    /// </remarks>
    public class RegisterAppShortcutKeysBehavior : Behavior<FrameworkElement>
    {
        private FrameworkElement frameworkElement;

        private static Dictionary<Window, List<FrameworkElement>> associatedWindows = new Dictionary<Window, List<FrameworkElement>>();
        private static object associatedWindowsLock = new object();
        private static List<InputBinding> appInputBindings = new List<InputBinding>();
        private static InputBinding[] readOnlyAppInputBindings = new InputBinding[0];
        private static object appInputBindingsLock = new object();

        private static HashSet<Key> shortcutKeys = new HashSet<Key>();

        static RegisterAppShortcutKeysBehavior()
        {
            shortcutKeys.Add(Key.F1);
            shortcutKeys.Add(Key.F2);
            shortcutKeys.Add(Key.F3);
            shortcutKeys.Add(Key.F4);
            shortcutKeys.Add(Key.F5);
            shortcutKeys.Add(Key.F6);
            shortcutKeys.Add(Key.F7);
            shortcutKeys.Add(Key.F8);
            shortcutKeys.Add(Key.F9);
            shortcutKeys.Add(Key.F10);
            shortcutKeys.Add(Key.F11);
            shortcutKeys.Add(Key.F12);
            shortcutKeys.Add(Key.F13);
            shortcutKeys.Add(Key.F14);
            shortcutKeys.Add(Key.F15);
            shortcutKeys.Add(Key.F16);
            shortcutKeys.Add(Key.F17);
            shortcutKeys.Add(Key.F18);
            shortcutKeys.Add(Key.F19);
            shortcutKeys.Add(Key.F20);
            shortcutKeys.Add(Key.F21);
            shortcutKeys.Add(Key.F22);
            shortcutKeys.Add(Key.F23);
            shortcutKeys.Add(Key.F24);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterAppShortcutKeysBehavior"/> class.
        /// </summary>
        public RegisterAppShortcutKeysBehavior()
        {
            this.InputBindings = new InputBindingCollection();
        }

        /// <summary>
        /// Gets or sets the input bindings.
        /// </summary>
        /// <value>The input bindings.</value>
        public InputBindingCollection InputBindings
        {
            get { return (InputBindingCollection)GetValue(InputBindingsProperty); }
            set { SetValue(InputBindingsProperty, value); }
        }

        public static readonly DependencyProperty InputBindingsProperty = DependencyProperty.Register("InputBindings", typeof(InputBindingCollection), typeof(RegisterAppShortcutKeysBehavior), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Registers the specified keyboard input bindings for the specified command target.
        /// </summary>
        /// <param name="inputBindings">The input bindings.</param>
        /// <param name="commandTarget">The command target.</param>
        /// <remarks>
        /// It is OK if the command target has not been loaded yet.
        /// Bindings are automatically removed when the command target is unloaded.
        /// </remarks>
        public static void Register(InputBindingCollection inputBindings, FrameworkElement commandTarget)
        {
            if (inputBindings == null)
            {
                throw new ArgumentNullException("inputBindings");
            }

            if (commandTarget == null)
            {
                throw new ArgumentNullException("commandTarget");
            }

            RegisterBindings(inputBindings, commandTarget);

            // I register the window if the target has been loaded, otherwise I wait until it is visible.
            if (!RegisterAssociatedWindow(commandTarget))
            {
                commandTarget.Loaded += new RoutedEventHandler(CommandTarget_Loaded);
            }

            commandTarget.Unloaded += new RoutedEventHandler(CommandTarget_Unloaded);
        }

        /// <summary>
        /// Unregisters all keyboard input binding for the specified command target.
        /// </summary>
        /// <param name="commandTarget">The command target.</param>
        public static void Unregister(FrameworkElement commandTarget)
        {
            if (commandTarget == null)
            {
                throw new ArgumentNullException("commandTarget");
            }

            UnregisterAssociatedWindow(commandTarget);
            UnregisterBindings(commandTarget);
        }

        private static void CommandTarget_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement commandTarget = sender as FrameworkElement;
            if (commandTarget != null)
            {
                commandTarget.Loaded -= CommandTarget_Loaded;
                RegisterAssociatedWindow(commandTarget);
            }
        }

        private static void CommandTarget_Unloaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement commandTarget = sender as FrameworkElement;
            if (commandTarget != null)
            {
                UnregisterAssociatedWindow(commandTarget);
                UnregisterBindings(commandTarget);
            }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.frameworkElement = this.AssociatedObject as FrameworkElement;

            if (this.frameworkElement != null)
            {
                if (this.InputBindings != null)
                {
                    RegisterBindings(this.InputBindings, this.frameworkElement);
                }
                RegisterAssociatedWindow(this.frameworkElement);
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (this.frameworkElement != null)
            {
                UnregisterAssociatedWindow(this.frameworkElement);

            }

            base.OnDetaching();
        }

        #region Registration Helper Methods

        private static void RegisterBindings(InputBindingCollection inputBindings, FrameworkElement commandTarget)
        {
            Debug.Assert(inputBindings != null);
            Debug.Assert(commandTarget != null);

            lock (appInputBindingsLock)
            {
                foreach (InputBinding inputBinding in inputBindings)
                {
                    // I only add input bindings that are KeyGestures and have a command.
                    if ((inputBinding.Gesture is KeyGesture) && (inputBinding.Command != null))
                    {
                        inputBinding.CommandTarget = commandTarget;
                        appInputBindings.Add(inputBinding);
                    }
                }

                readOnlyAppInputBindings = appInputBindings.ToArray();
            }
        }

        private static void UnregisterBindings(InputBindingCollection inputBindings)
        {
            Debug.Assert(inputBindings != null);

            lock (appInputBindingsLock)
            {
                foreach (InputBinding inputBinding in inputBindings)
                {
                    appInputBindings.Remove(inputBinding);
                }

                readOnlyAppInputBindings = appInputBindings.ToArray();
            }
        }

        private static void UnregisterBindings(object commandTarget)
        {
            Debug.Assert(commandTarget != null);

            lock (appInputBindingsLock)
            {
                foreach (InputBinding inputBinding in appInputBindings)
                {
                    if (object.ReferenceEquals(inputBinding.CommandTarget, commandTarget))
                    {
                        appInputBindings.Remove(inputBinding);
                    }
                }

                readOnlyAppInputBindings = appInputBindings.ToArray();
            }
        }

        private static bool RegisterAssociatedWindow(FrameworkElement commandTarget)
        {
            Debug.Assert(commandTarget != null);

            Window window = Window.GetWindow(commandTarget);
            if (window != null)
            {
                lock (associatedWindowsLock)
                {
                    // I reference count the windows to prevent double subscribing to PreviewKeyDown
                    List<FrameworkElement> frameworkElements;
                    if (!associatedWindows.TryGetValue(window, out frameworkElements))
                    {
                        frameworkElements = new List<FrameworkElement>();
                        associatedWindows[window] = frameworkElements;
                        window.PreviewKeyDown += Window_PreviewKeyDown;
                    }

                    if (!frameworkElements.Contains(commandTarget))
                    {
                        frameworkElements.Add(commandTarget);
                    }
                }
            }

            return (window != null);
        }

        private static void UnregisterAssociatedWindow(FrameworkElement commandTarget)
        {
            Debug.Assert(commandTarget != null);

            Window window = Window.GetWindow(commandTarget);
            if (window != null)
            {
                lock (associatedWindowsLock)
                {
                    List<FrameworkElement> frameworkElements;
                    if (associatedWindows.TryGetValue(window, out frameworkElements))
                    {
                        frameworkElements.Remove(commandTarget);
                        if (frameworkElements.Count == 0)
                        {
                            associatedWindows.Remove(window);
                            window.PreviewKeyDown -= Window_PreviewKeyDown;
                        }
                    }
                }
            }
        }

        #endregion

        private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isShortcutKeystroke = (Keyboard.Modifiers != ModifierKeys.None) || shortcutKeys.Contains(e.Key);

            if (isShortcutKeystroke)
            {
                Window window = sender as Window;
                if (window != null)
                {
                    // I hold the lock for the shortest time possible
                    InputBinding[] inputBindings;
                    lock (appInputBindingsLock)
                    {
                        inputBindings = readOnlyAppInputBindings;
                    }

                    foreach (InputBinding inputBinding in inputBindings)
                    {
                        if (inputBinding.Gesture.Matches(inputBinding.CommandTarget, e))
                        {
                            if (InvokeCommandIfTargetVisible(inputBinding))
                            {
                                e.Handled = true;
                            }
                        }
                    }
                }
            }
        }

        private static bool InvokeCommandIfTargetVisible(InputBinding inputBinding)
        {
            Debug.Assert(inputBinding != null);

            FrameworkElement frameworkElement = inputBinding.CommandTarget as FrameworkElement;

            // I only execute when the command target is visible.
            if ((frameworkElement != null) && (frameworkElement.IsVisible))
            {
                return InvokeCommand(inputBinding);
            }

            return false;
        }

        private static bool InvokeCommand(InputBinding inputBinding)
        {
            Debug.Assert(inputBinding != null);

            // Routed commands are static, so we need to provide the command target
            // otherwise the default is to use the focused element.
            RoutedCommand routedCommand = inputBinding.Command as RoutedCommand;
            if (routedCommand != null)
            {
                if (routedCommand.CanExecute(inputBinding.CommandParameter, inputBinding.CommandTarget))
                {
                    routedCommand.Execute(inputBinding.CommandParameter, inputBinding.CommandTarget);
                    return true;
                }
            }
            else
            {
                // Other commands are instance commands and can be called directly.
                if (inputBinding.Command.CanExecute(inputBinding.CommandParameter))
                {
                    inputBinding.Command.Execute(inputBinding.CommandParameter);
                    return true;
                }
            }

            return false;
        }
    }
}
