// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/DynamicProxy.AsyncInterceptor/blob/master/LICENSE.md.

using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace stakx.DynamicProxy.Tests
{
    public sealed class WaitUntil : AsyncInterceptor
    {
        private readonly Task task;
        private readonly bool proceed;

        public WaitUntil(Clock clock, int time, bool proceed = false)
        {
            this.task = clock.TaskFactory.CreateCompletingTask(time);
            this.proceed = proceed;
        }

        protected override void Intercept(IInvocation invocation)
        {
            this.task.Wait();

            if (this.proceed)
            {
                invocation.Proceed();
            }
        }

        protected override async ValueTask InterceptAsync(IAsyncInvocation invocation)
        {
            await this.task;

            if (this.proceed)
            {
                await invocation.ProceedAsync();
            }
        }
    }
}
