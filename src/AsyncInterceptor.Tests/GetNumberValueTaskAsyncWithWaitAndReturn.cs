// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using Xunit;

namespace Castle.DynamicProxy.Contrib.Tests
{
    public class GetNumberValueTaskAsyncWithWaitAndReturn : ClockBasedTextFixture
    {
        [Fact]
        public void GetNumberValueTaskAsync_that_completes_at_time_1_is_initially_not_completed()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new WaitUntil(this.Clock, 1));

            var task = proxy.GetNumberValueTaskAsync();

            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void GetNumberValueTaskAsync_that_completes_at_time_1_is_successfully_completed_at_time_1()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new WaitUntil(this.Clock, 1));
            var task = proxy.GetNumberValueTaskAsync();

            this.Clock.AdvanceTo(1);

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public void GetNumberValueTaskAsync_that_completes_at_time_1_completes_successfully_with_correct_result_at_time_1()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new WaitUntil(this.Clock, 1, proceed: true),
                                                              new Return(42));
            var task = proxy.GetNumberValueTaskAsync();

            this.Clock.AdvanceTo(1);

            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(42, task.Result);
        }
    }
}
