// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using System.Threading.Tasks;

namespace Castle.DynamicProxy.Contrib.Tests
{
    public sealed class Return : AsyncInterceptor
    {
        private readonly object value;

        public Return(object value)
        {
            this.value = value;
        }

        protected override void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = this.value;
        }

        protected override ValueTask InterceptAsync(IAsyncInvocation invocation)
        {
            invocation.Result = this.value;
            return default;
        }
    }
}
