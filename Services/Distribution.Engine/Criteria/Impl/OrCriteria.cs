using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Engine.Criteria.Impl
{
    /// <summary>
    /// 规则或
    /// </summary>
    public class OrCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria ?? throw new ArgumentNullException("criteria");
            _otherCriteria = otherCriteria ?? throw new ArgumentNullException("otherCriteria");
        }

        public IEnumerable<ShopContext> MeetCriteria(IEnumerable<ShopContext> shopContextList)
        {
            var firstCriteriaShopContextList = _criteria.MeetCriteria(shopContextList);
            var otherCriteriaShopContextList = _otherCriteria.MeetCriteria(shopContextList);

            return firstCriteriaShopContextList.Union(otherCriteriaShopContextList);
        }
    }
}
