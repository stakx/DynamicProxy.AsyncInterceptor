// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using System;
using System.Diagnostics;

namespace stakx.DynamicProxy.Tests
{
    // Based on Jon Skeet's `TimeMachine` class described in:
    // https://codeblog.jonskeet.uk/2011/11/25/eduasync-part-17-unit-testing/
    public sealed class Clock
    {
        private readonly ClockTaskFactory taskFactory;
        private int time;

        public Clock()
        {
            this.time = 0;
            this.taskFactory = new ClockTaskFactory(this);
        }

        public event Action<int> Advanced;

        public ClockTaskFactory TaskFactory => this.taskFactory;

        public int Time => this.time;

        public void AdvanceTo(int time)
        {
            Debug.Assert(this.time < time);

            this.time = time;
            this.Advanced?.Invoke(this.time);
        }
    }
}
