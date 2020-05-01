// Copyright (c) 2020 stakx
// License available at https://github.com/stakx/AsyncInterceptor/blob/master/LICENSE.md.

using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace stakx.DynamicProxy
{
    public interface IAsyncInvocation
    {
        IReadOnlyList<object> Arguments { get; }
        MethodInfo Method { get; }
        object Result { get; set; }
        ValueTask ProceedAsync();
    }
}
