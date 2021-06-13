# Summary

This package provides an easy way to pipe an input to a list of functions and get back the results.

# Install

TBD: NuGet

# Use

To use Fan, call .Fan() on an input and pass the needed methods to it. All methods passed to .Fan() must accept the type of the input and return the same output type (even if that type is void).

When you pass an array of methods which return void, .Fan() will return the input variable.<br>
When you pass an array of methods with a non-void return type, .Fan() will return an IEnumerable of the return type of the functions.

For instance:

```csharp
// Each validation method does not return anything, so .Fan() will return the "order" variable.
public SalesOrder Validate(SalesOrder order)
    // Notice order.Fan() with the methods passed in.
    => order.Fan(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);
        
// In this case, each validation method returns a string, so .Fan() will return an IEnumerable<string>.
public IEnumerable<string> Validate(SalesOrder order)
    // Notice order.Fan() with the methods passed in.
    => order.Fan(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);
```

We can also call the functions in parallel:

```csharp
public SalesOrder ValidateParallel(SalesOrder order)
    => order.FanParallel(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);
```

Or asynchronously:

```csharp
public async Task ValidateAsync(SalesOrder order)
    => await order.FanAsync(
        QuantityGreaterThanMinimumAllowedAsync,
        CustomerRelatedToFactoryAsync,
        ItemIsOnPriceListAsync,
        OrderIncrementMetAsync,
        PriceIsQuantityTimesOrderIncrementAsync);
```

Note about async: Because of the C# type system, if one method is asynchronous, every method must be asynchronous. You can always make a non-asynchronous method async like so:

```csharp
// Non-asynchronous version
private void Add(int num, int num2)
    => num + num2;
    
// Asynchronous version, just stick operation inside: "await Task.Run(() => YOUR_CODE);"
private async Task AddAsync(int num, int num2)
    => await Task.Run(() => num + num2);
```


# Justification

There are many advantages to using a method like .Fan in your code. Here are a few.

## Readability

There are times when you have a range of things which need to occur in any order. Validating a model, notifying a range of targets that something occurred, updating several tables in a database, etc. In these situations, you can use the semicolon. However, when you use the semicolon, anyone has the opportunity to create dependencies between calls. 

As a real-life example, here is a simplified validation method. Each of these methods will throw an InvalidOrderException if they find something wrong.

```csharp
public void Validate(SalesOrder order)
{
    QuantityGreaterThanMinimumAllowed(order);
    CustomerRelatedToFactory(order);
    ItemIsOnPriceList(order);
    OrderIncrementMet(order);
    PriceIsQuantityTimesOrderIncrement(order);
}
```

A few weeks after writing this, another developer diagnosed a bug and fixed it. Instead of following the pattern of letting each validation method handle its own logic, they added an if statement outside of the validation method.

```csharp
public void Validate(SalesOrder order)
{
    QuantityGreaterThanMinimumAllowed(order);
    CustomerRelatedToFactory(order);
    
    if (order.PriceListNumber > 0)
    {
        ItemIsOnPriceList(order);
        PriceIsQuantityTimesOrderIncrement(order);
    }

    OrderIncrementMet(order);
}
```

This reduces the readability, debugability, and testability of the code. When a ticket comes in to change ItemIsOnPriceList, the developer now has to consider whether the if statement needs to change and if that change is valid for both methods in scope. To fix a failing case, the developer now has to debug both the outside scope and the inside scope. When adding a new business rule, a developer will wonder whether to put the rule within that if statement or outside. Then wonder whether order matters between the calls.

When code aligns with a list of independent methods (much like a compound sentence), each method should be able to run without being effected by the outside scope. This allows the eye to travel down the list more easily, and even creates benefits for unit testing or debugging.

Here is the same code using .Fan(). As you can see, the code is reduced to one semicolon. This eliminates the dependencies between the method calls. It also allows the eye to focus exclusively on the business rules, as there are no variables passed manually to the functions.

```csharp
public void Validate(SalesOrder order)
    => order.Fan(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);
```

## Parallelism

Readability isn't the only advantage. Because these calls are independent of one another, we can change the *way* that we call them with one line of code. WinstonPuckett.Fan gives three options:

- Serial with .Fan()
- Parallel with .FanParallel()
- Async with .FanAsync()

If the functions are not naturally asynchronous, being able to test the speed difference between .Fan and .FanParallel is incredibly useful. 

Here is the same block of code in serial, parallel, and async. Notice that the linguistics between them are incredibly similar.

```csharp
public void Validate(SalesOrder order)
    => order.Fan(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);

public void ValidateParallel(SalesOrder order)
    => order.FanParallel(
        QuantityGreaterThanMinimumAllowed,
        CustomerRelatedToFactory,
        ItemIsOnPriceList,
        OrderIncrementMet,
        PriceIsQuantityTimesOrderIncrement);

public async Task ValidateAsync(SalesOrder order)
    => await order.FanAsync(
        QuantityGreaterThanMinimumAllowedAsync,
        CustomerRelatedToFactoryAsync,
        ItemIsOnPriceListAsync,
        OrderIncrementMetAsync,
        PriceIsQuantityTimesOrderIncrementAsync);
```

# This is not a unique idea. I just can't find a simple NuGet package for it.

Although the idea for this package originated from my roots in technical writing, I found out after making it that I am not the first person to think of this.

- [Simon Painter describing the same thing](https://youtu.be/v7WLC5As6g4?t=810)
- [Bash's FanAsync syntax (& operator)](https://datacadamia.com/lang/bash/process/ampersand#:~:text=Bash%20-%20Ampersand%20%28%26%29%20-%20%28Asynchronous%7CParallel%29%20control%20operator,3%204%20-%20Example%204%205%20-%20Process)
