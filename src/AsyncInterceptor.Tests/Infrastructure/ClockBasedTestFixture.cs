// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using Castle.DynamicProxy;

namespace stakx.DynamicProxy.Tests
{
    public abstract class ClockBasedTextFixture
    {
        private readonly Clock clock;
        private readonly ProxyGenerator generator;

        protected ClockBasedTextFixture()
        {
            this.clock = new Clock();
            this.generator = new ProxyGenerator();
        }

        protected Clock Clock => this.clock;

        protected T CreateInterfaceProxy<T>(params IInterceptor[] interceptors)
            where T : class
        {
            return this.generator.CreateInterfaceProxyWithoutTarget<T>(interceptors);
        }
    }
}
