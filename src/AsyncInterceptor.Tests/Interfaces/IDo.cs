// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using System.Threading.Tasks;

namespace Castle.DynamicProxy.Contrib.Tests
{
    public interface IDo
    {
        void Do();
        Task DoTaskAsync();
        ValueTask DoValueTaskAsync();
    }
}
