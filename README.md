BellaCode.Mvvm
====
BellaCode.Mvvm lightweight library for following the Model-View-ViewModel (M-V-VM) pattern.

Features
----

- Use a single XAML element to connect your View-ViewModel-Model triad.
- Write Plain Old CLR Object (POCO) view models.
- Bind commands directly to methods (no more writing ICommand delegation code!)
- Easily create interactions between your view and view model using InteractionEventArgs.
- Use dependency injection (Ninject, Unity, MEF) to instantiate view models.
- The BellaCode.Mvvm library only does MVVM and nothing more.

Easy to Adopt
----

The included ReferenceDemo provides example code along with a comment tour through the code.  

It covers basic view models, top-level view models, commands, events, interactions, and dependency injection.

Core Classes
----
These are the classes you'll use most of the time.

**ViewModel<TModel>**

    This is a base class for your view model classes.

**ViewModelScope**

    This is a Decorator class (like Border) that you wrap around existing elements.  
    Just set the ViewModelType property to the type of your view model.

Alternative Classes
----
These are classes you'll use for special cases.

**ViewModel**

    This is a weakly-typed base classes. Derive your view models classes from it when you don't need the view or the model.

**ViewModel<TView, TModel>**

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

**IViewModel<TView,TModel>**

    The strongly typed interface implemented by ViewModel<TView, TModel>.

**DefaultViewModelFactory**

    The the default class for creating view models. 
    It uses the Activator to create instances of types loaded in the application domain.
