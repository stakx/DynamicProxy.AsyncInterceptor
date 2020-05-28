// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/DynamicProxy.AsyncInterceptor/blob/master/LICENSE.md.

using System;

using Xunit;

namespace stakx.DynamicProxy.Tests
{
    public class ClockTaskFactoryTests
    {
        [Fact]
        public void CreateCompletingTask_task_is_initially_not_completed()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateCompletingTask(1);

            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void CreateCompletingTask_task_completes_successfully_when_clock_reaches_precise_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateCompletingTask(1);
            clock.AdvanceTo(1);

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public void CreateCompletingTask_task_completes_successfully_when_clock_reaches_later_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateCompletingTask(1);
            clock.AdvanceTo(10);

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public void CreateCompletingTask_task_completes_successfully_with_correct_result_when_clock_reaches_precise_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateCompletingTask(1, "42");
            clock.AdvanceTo(1);

            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal("42", task.Result);
        }

        [Fact]
        public void CreateCompletingTask_task_completes_successfully_with_correct_result_when_clock_reaches_later_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateCompletingTask(1, "42");
            clock.AdvanceTo(10);

            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal("42", task.Result);
        }

        [Fact]
        public void CreateFaultingTask_task_is_initially_not_completed_and_not_faulted()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateFaultingTask(1, new Exception("bad"));

            Assert.False(task.IsCompleted);
            Assert.False(task.IsFaulted);
        }

        [Fact]
        public void CreateFaultingTask_task_faults_when_clock_reaches_precise_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateFaultingTask(1, new Exception("bad"));
            clock.AdvanceTo(1);

            Assert.True(task.IsCompleted);
            Assert.True(task.IsFaulted);
        }

        [Fact]
        public void CreateFaultingTask_task_faults_when_clock_reaches_later_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateFaultingTask(1, new Exception("bad"));
            clock.AdvanceTo(10);

            Assert.True(task.IsCompleted);
            Assert.True(task.IsFaulted);
        }

        [Fact]
        public void CreateFaultingTask_task_faults_with_correct_exception_when_clock_reaches_precise_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateFaultingTask(1, new Exception("bad"));
            clock.AdvanceTo(1);

            var exception = Assert.Throws<Exception>(() => task.GetAwaiter().GetResult());
            Assert.Equal("bad", exception.Message);
        }

        [Fact]
        public void CreateFaultingTask_task_faults_with_correct_exception_when_clock_reaches_later_time()
        {
            var clock = new Clock();
            var task = clock.TaskFactory.CreateFaultingTask(1, new Exception("bad"));
            clock.AdvanceTo(10);

            var exception = Assert.Throws<Exception>(()=> task.GetAwaiter().GetResult());
            Assert.Equal("bad", exception.Message);
        }
    }
}
