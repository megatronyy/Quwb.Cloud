using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Engine.Criteria
{
    /// <summary>
    /// 规则
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// 过滤出满足当前规则的用户列表
        /// </summary>
        /// <param name="dealerInfoContextList"></param>
        /// <returns></returns>
        IEnumerable<ShopContext> MeetCriteria(IEnumerable<ShopContext> shopContextList);
    }
}
