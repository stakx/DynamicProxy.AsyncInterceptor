# AsyncInterceptor

This small library provides an abstract base class `AsyncInterceptor` for Castle DynamicProxy which allows you to use .NET languages' `async`/`await` facilities during interception of `await`-able methods.

This is currently in draft stage.


![.NET Core](https://github.com/stakx/AsyncInterceptor/workflows/.NET%20Core/badge.svg?branch=master)


## Usage example

```csharp
class Delay : AsyncInterceptor
{
    private readonly int milliseconds;

    public Delay(int milliseconds)
    {
        this.milliseconds = milliseconds;
    }

    // This gets called when a non-awaitable method is intercepted:
    protected override void Intercept(IInvocation invocation)
    {
        Thread.Sleep(this.milliseconds);
        invocation.Proceed();
    }

    // Or this gets called when an awaitable method is intercepted:
    protected override async ValueTask InterceptAsync(IAsyncInvocation invocation)
    {
        await Task.Delay(this.milliseconds);
        await invocation.ProceedAsync();
    }
}


class SetReturnValue : AsyncInterceptor
{
    private readonly object returnValue;

    public SetReturnValue(object  returnValue)
    {
        this.returnValue = returnValue;
    }

    protected override void Intercept(IInvocation invocation)
    {
        invocation.ReturnValue = this.returnValue;
    }

    protected override ValueTask InterceptAsync(IAsyncInvocation invocation)
    {
        // The property being set is called `Result` rather than `ReturnValue`.
        // This is a hint that its value doesn't have to be wrapped up as a task-like object:
        invocation.Result = this.returnValue;
        return default;
    }
}


public interface ICalculator
{
    int GetResult();
    Task<int> GetResultAsync();
}


var generator = new ProxyGenerator();

var proxy = generator.CreateInterfaceProxyWithoutTarget<ICalculator>(
    new Delay(2500),
    new SetReturnValue(42));

Assert.Equal(42, proxy.GetResult());
Assert.Equal(42, await proxy.GetResultAsync());
