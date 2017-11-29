using System;
using System.Collections.Generic;
using System.Text;
using Lib.Framework.Core.IoC;

namespace Lib.Framework.Core.Extensions
{
    public static class IoCContainerExtensions
    {
        public static void AddAspect(this IoCContainer container)
        {
            if(container==null)
                throw new ArgumentNullException(nameof(container));
        }
    }
}
