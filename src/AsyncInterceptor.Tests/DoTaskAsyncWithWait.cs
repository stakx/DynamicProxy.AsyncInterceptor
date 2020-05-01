// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using Xunit;

namespace stakx.DynamicProxy.Tests
{
    public class DoTaskAsyncWithWait : ClockBasedTextFixture
    {
        [Fact]
        public void DoTaskAsync_that_completes_at_time_1_is_initially_not_completed()
        {
            var proxy = this.CreateInterfaceProxy<IDo>(new WaitUntil(this.Clock, 1));

            var task = proxy.DoTaskAsync();

            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void DoTaskAsync_that_completes_at_time_1_is_successfully_completed_at_time_1()
        {
            var proxy = this.CreateInterfaceProxy<IDo>(new WaitUntil(this.Clock, 1));
            var task = proxy.DoTaskAsync();

            this.Clock.AdvanceTo(1);

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
