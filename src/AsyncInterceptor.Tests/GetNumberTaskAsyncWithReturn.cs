// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using Xunit;

namespace Castle.DynamicProxy.Contrib.Tests
{
    public class GetNumberTaskAsyncWithReturn : ClockBasedTextFixture
    {
        [Fact]
        public void GetNumberTaskAsync_is_initially_completed()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new Return(42));

            var task = proxy.GetNumberTaskAsync();

            Assert.True(task.IsCompleted);
        }

        [Fact]
        public void GetNumberTaskAsync_is_completed_successfully_with_correct_result()
        {
            var proxy = this.CreateInterfaceProxy<IGetNumber>(new Return(42));
            var task = proxy.GetNumberTaskAsync();

            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(42, task.Result);
        }
    }
}
