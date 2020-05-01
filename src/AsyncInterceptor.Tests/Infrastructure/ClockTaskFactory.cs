// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace stakx.DynamicProxy.Tests
{
    // Based on Jon Skeet's `TimeMachine` class described in:
    // https://codeblog.jonskeet.uk/2011/11/25/eduasync-part-17-unit-testing/
    public sealed class ClockTaskFactory
    {
        private Clock clock;

        public ClockTaskFactory(Clock clock)
        {
            Debug.Assert(clock != null);

            this.clock = clock;
        }

        public Task CreateCompletingTask(int time)
        {
            return this.CreateCompletingTask(time, true);
        }

        public Task<TResult> CreateCompletingTask<TResult>(int time, TResult result)
        {
            Debug.Assert(this.clock.Time < time);

            var tcs = new TaskCompletionSource<TResult>();

            Action<int> onAdvanced = default;
            onAdvanced = newTime =>
            {
                if (time <= newTime)
                {
                    tcs.SetResult(result);
                }

                this.clock.Advanced -= onAdvanced;
            };

            this.clock.Advanced += onAdvanced;

            return tcs.Task;
        }

        public Task CreateFaultingTask(int time, Exception exception)
        {
            return this.CreateFaultingTask<bool>(time, exception);
        }

        public Task<TResult> CreateFaultingTask<TResult>(int time, Exception exception)
        {
            Debug.Assert(this.clock.Time < time);

            var tcs = new TaskCompletionSource<TResult>();

            Action<int> onAdvanced = default;
            onAdvanced = newTime =>
            {
                if (time <= newTime)
                {
                    tcs.SetException(exception);
                }

                this.clock.Advanced -= onAdvanced;
            };

            this.clock.Advanced += onAdvanced;

            return tcs.Task;
        }
    }
}
