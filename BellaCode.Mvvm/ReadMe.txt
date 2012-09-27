--------------------------------------------------------------------------------
BellaCode.Mvvm Library
--------------------------------------------------------------------------------
BellaCode.Mvvm lightweight library for following the Model-View-ViewModel (M-V-VM) pattern.

----------------------------------------
Features
----------------------------------------

- Bind against a model or view model anywhere in the XAML tree.
- Follows the typical development of XAML.
- Strong and weak typed view model interfaces and base classes.
- Support for integrating a dependency injection (DI) container to create view models.

----------------------------------------
Tenants
----------------------------------------

M-V-VM only
    The library has no dependencies outside WPF/.NET.  All the classes are related strictly to M-V-VM.

XAML development
    By wrapping an element in a decorator, the child element gets the view model as the DataContext.

No POCO glue
    The library does not provide commanding or property changed helpers or patterns.  Another library might, but not this one.

No additional patterns required
    The library can support dependency injection and view model locator, but nothing is required on behalf of the developer.

----------------------------------------
How it works
----------------------------------------

When the ViewModelScope element is loaded:

  1. It instantiates a view model using the ViewModelType property.
  2. It sets the view model's Model property = DataContext
  3. It sets the view model's View property = child control
  4. It sets the child control's DataContext = view model

----------------------------------------
Core Classes
----------------------------------------
These are the classes you'll use most of the time.

ViewModel<TModel>
    This is a base class for your view model classes.

ViewModelScope
    This is a Decorator class (like Border) that you wrap around existing elements.  
	Unlike Border, it doesn't display or make changes to the UI. 
	Just set the ViewModelType property to the type of your view model.

----------------------------------------
Alternative Classes
----------------------------------------
These are classes you'll use for special cases.

ViewModel
    This is a weakly-typed base classes. Derive your view models classes from it when you don't need the view or the model.

ViewModel<TView, TModel>
    This is a base class for when your view model needs access to the view. Prefer specifying and interface for TView.

----------------------------------------
Advanced Classes - Dependency Injection
----------------------------------------
These are interfaces and classes you can implement to integrate dependency injection containers like Ninject, Unity, or MEF.

IViewModelFactory
    Implement this interface to create view model types using your DI container.  
	Then set the ViewModelScope.ViewModelFactory static to your implementation when your application starts.

----------------------------------------
Infrastruture Classes
----------------------------------------
Interfaces you generally don't need to implement and classes you generally don't need to use.

IViewModel
    The general purpose base interface used by the ViewModelScope class.

IViewModel<TView,TModel>
    The strongly typed interface implemented by ViewModel<TView, TModel>.

DefaultViewModelFactory
    The the default class for creating view models. It uses the Activator to create instances of types loaded in the application domain.


