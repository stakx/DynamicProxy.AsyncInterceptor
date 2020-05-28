// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/DynamicProxy.AsyncInterceptor/blob/master/LICENSE.md.

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace stakx.DynamicProxy
{
    public abstract partial class AsyncInterceptor : IInterceptor
    {
        void IInterceptor.Intercept(IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;
            var builder = AsyncMethodBuilder.TryCreate(returnType);
            if (builder != null)
            {
                var asyncInvocation = new AsyncInvocation(invocation);
                var stateMachine = new AsyncStateMachine(asyncInvocation, builder, task: this.InterceptAsync(asyncInvocation));
                builder.Start(stateMachine);
                invocation.ReturnValue = builder.Task();
            }
            else
            {
                this.Intercept(invocation);
            }
        }

        protected abstract void Intercept(IInvocation invocation);

        protected abstract ValueTask InterceptAsync(IAsyncInvocation invocation);
    }
}
