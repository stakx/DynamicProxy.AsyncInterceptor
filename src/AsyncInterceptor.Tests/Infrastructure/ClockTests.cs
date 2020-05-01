// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using Xunit;

namespace stakx.DynamicProxy.Tests
{
    public class ClockTests
    {
        [Fact]
        public void Time_is_initially_zero()
        {
            var clock = new Clock();
            Assert.Equal(0, clock.Time);
        }

        [Fact]
        public void AdvanceTo_changes_time_to_specified_value()
        {
            var clock = new Clock();

            clock.AdvanceTo(10);
            Assert.Equal(10, clock.Time);
        }

        [Fact]
        public void AdvanceTo_argument_is_absolute_time_value_not_relative()
        {
            var clock = new Clock();
            clock.AdvanceTo(1);

            clock.AdvanceTo(10);
            Assert.Equal(10, clock.Time);
        }
    }
}
