BellaCode.Mvvm
====
BellaCode.Mvvm lightweight library for following the Model-View-ViewModel (M-V-VM) pattern.

Now available via NuGet! PM> Install-Package BellaCode.Mvvm 
  Supports .NET 4.0, .NET 4.0 client, and .NET 4.5.

Licensed under the MIT license: http://opensource.org/licenses/MIT

Features
----

- Use a single XAML element to connect your View-ViewModel-Model triad.
- Write Plain Old CLR Object (POCO) view models.
- Bind commands directly to methods (no more writing ICommand delegation code!)
- Easily create interactions between your view and view model using InteractionEventArgs.
- Use dependency injection (Ninject, Unity, MEF) to instantiate view models.
- The BellaCode.Mvvm library provides some useful actions, behaviors, and converters (optional).

Easy to Adopt
----

The included ReferenceDemo provides example code along with a comment tour through the code.  

It covers basic view models, top-level view models, commands, events, interactions, and dependency injection.

Core Classes
----
These are the classes you'll use most of the time.

**ViewModel&lt;TModel&gt;**

    This is a base class for your view model classes.

**ViewModelScope**

    This is a Decorator class (like Border) that you wrap around existing elements.  
    Just set the ViewModelType property to the type of your view model.

Alternative Classes
----
These are classes you'll use for special cases.

**ViewModel**

    This is a weakly-typed base classes. Derive your view models classes from it when you don't need the view or the model.

**ViewModel&lt;TView, TModel&gt;**

    This is a base class for when your view model needs access to the view.

Advanced Classes - Dependency Injection
----
These are interfaces and classes you can implement to integrate dependency injection containers like Ninject, Unity, or MEF.

**IViewModelFactory**

    Implement this interface to create view model types using your DI container.  
    Then set the ViewModelScope.ViewModelFactory static to your implementation when your application starts.

Infrastruture Classes
----
Interfaces you generally don't need to implement and classes you generally don't need to use.

**IViewModel**

    The general purpose base interface used by the ViewModelScope class.

**IViewModel&lt;TView,TModel&gt;**

    The strongly typed interface implemented by ViewModel<TView, TModel>.

**DefaultViewModelFactory**

    The the default class for creating view models. 
    It uses the Activator to create instances of types loaded in the application domain.

Actions
----

**AskQuestionAction**

    Displays the specified Yes/No question and optional caption and returns the dialog result.

**CloseWindowAction**

    Closes the window containing the associated UI element with the specified dialog result.

**OpenFileDialogAction**

    Displays the open file dialog.

**SaveFileDialogAction**

    Displays the save file dialog.

**ScrollItemIntoViewListBoxAction**

    Calls ScrollIntoView for the item (parameter) on the associated list box.

**SetFocusAction**

    Sets the focus to the specified Target.

**SetWindowTitleAction**

    Sets the title of the window for the associated control to the specified new title.

**ShowMessageAction**

    Displays the specified message with the optional caption.

Behaviors
----

**BindCalendarSelectedDatesBehavior**

    Provides (fast) binding to a Calendar's SelectDates property.

**BindCommandBehavior**

    Binds a command to a method for executing the command and a property or method for determining if the command can execute.

**BindIsVisibleViewBehavior**

    Binds the associated UIElement's IsVisible property to the IsVisible dependency property (via OneWayToSource binding).

**BindPasswordBoxTextBehavior**

    Provides binding to a PasswordBox Password property.

**BindSelectedItemsBehavior**

    Provides (fast) binding to a any Selector derived class (e.g. ListBox, ListView, etc.) SelectedItems property.

**BridgeBindingsBehavior**

    Binds a property on the associated FrameworkElement to another property in the logical tree.    

**ClosingWindowExecutesCommandBehavior**

    Executes the specified command when the associated object's window is closing.

**DoubleClickItemExecutesCommandListBoxBehavior**

    Executes the specified command when a ListBoxItem is double-clicked.

**EnterKeyExecutesCommandBehavior**

    Executes a command when the enter key (without modifiers) is pressed in a text box.

**EnterKeyUpdatesSourceBehavior**

    Updates the binding source of a TextBox when the enter key (without modifiers) is pressed.

**FixCalendarMouseCaptureBehavior**

    Fixes a bug with the Calendar that takes over mouse capture.

**FocusOnLoadedBehavior**

    Focuses on the associated element when it is loaded.

**FocusOnMouseDownBehavior**

    Helps controls like ListBox get focus when the mouse is down.

**IdleTypingExecutesCommandBehavior**

    Executes a command for a TextBox when the user is typing and pauses for 500 ms.

**IdleTypingUpdatesSourceBehavior**

    Updates the binding source of a TextBox when the user is typing and pauses for 500 ms.

**PreviewGotKeyboardFocusSelectsItemListBoxBehavior**

    Helps ListBox to select the current item when the ListBox takes keyboard focus.

**RefreshBindingAfterLoadBehavior**

    Refreshes a dependency property after the associated object has loaded.

**RegisterAppShortcutKeysBehavior**

    Registers KeyGesture InputBindings at application scope.

**ResettableGridSplitterBehavior**

    Extends the behavior of a GridSplitter to reset it to initial position when double-clicked.

**SelectAllOnFocusTextBoxBehavior**

    Selects all the text in a textbox when the text box gets focus.

**WatermarkBehavior**

    Shows the specified watermark control when the textbox is empty and does not have focus.

Converters
----

**AbsoluteValueConverter**

    Converts a number to its absolute value.

**BooleanToKnownValueConverter**

    Converts a boolean to a known value based on if the boolean is null, false, or true.

**BooleanVisibilityConverter**

    Converts a boolean to a Visibility based on if the boolen is null, false, or true.

**CardinalityToVisibilityConverter**

    Converts a number to a Visibility based on if the number is null, zero, one, or greater than one. 

**EnumBooleanConverter**

    Converts an enum value to a boolean by comparing the enum value to the converter parameter value.

**EnumVisibilityConverter**

    Converts an enum value to a Visibility by comparing the value to the converter parameter value.

**InvertBooleanConverter**

    Converts a boolean to its inverted value.

**NoOpConverter**

    A no-op value converter useful ensure the value conversion/coersion stage runs for certain bindings.

**NumericRangeVisibilityConverter**

    Converts a number to oa Visibility based on if the number is null, inside the range, or outside the range.

**NumericSignToVisibilityConverter**

    Converts a number to a Visibility based on if the number is null, negative, zero, or positive.

**ObjectRefToBooleanConverter**

    Converts an object reference to a boolean based on if the object is null or not null.

**ObjectRefVisibilityConverter**

    Converts an object reference to a Visibility based on if the object is null or not null.

**PercentToDegreesConverter**

    Converts a number from a percentage (0-100) to a number of degrees (0-360).

**StringVisibilityConverter**

    Converts a string to a Visibility based on if the string is null/empty, or not empty.

**ZeroToOneBasedIndexConverter**

    Converts a number from a zero-based indexing to a one-based indexing.

Helpers
----

**VisualTreeHelperT**

    Provides helpful methods for finding parents and children within the visual tree.