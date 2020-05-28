// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/DynamicProxy.AsyncInterceptor/blob/master/LICENSE.md.

using Xunit;

namespace stakx.DynamicProxy.Tests
{
    public class GetNumberValueTaskAsyncWithReturn : ClockBasedTextFixture
    {
        [Fact]
        public void GetNumberValueTaskAsync_is_initially_completed()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new Return(42));

            var task = proxy.GetNumberValueTaskAsync();

            Assert.True(task.IsCompleted);
        }

        [Fact]
        public void GetNumberValueTaskAsync_is_completed_successfully_with_correct_result()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new Return(42));
            var task = proxy.GetNumberValueTaskAsync();

            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(42, task.Result);
        }
    }
}
