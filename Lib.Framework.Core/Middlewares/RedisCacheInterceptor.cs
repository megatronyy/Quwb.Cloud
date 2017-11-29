﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dora.Interception;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Lib.Framework.Core.Extensions;
using Lib.Framework.Core.Helpers;

namespace Lib.Framework.Core.Middlewares
{
    public class RedisCacheInterceptor
    {
        private readonly InterceptDelegate _next;
        public RedisCacheInterceptor(InterceptDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(InvocationContext context, IDistributedCache cache, IOptions<DistributedCacheEntryOptions> optionsAccessor)
        {
            //判断Method是否包含ref/out参数
            if (!context.Method.GetParameters().All(it => it.IsIn))
            {
                await _next(context);
            }

            var key = new CacheKey(context.Method, context.Arguments).GetHashCode().ToString();
            var value = cache.Get(key);
            if (value!=null)
            {
                context.ReturnValue = value.ToObject();
            }
            else
            {
                await _next(context);
                cache.Set(key, context.ReturnValue.ToBytes(), optionsAccessor.Value);
            }
        }
    }
}
