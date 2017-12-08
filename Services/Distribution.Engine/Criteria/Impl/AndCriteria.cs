using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Engine.Criteria.Impl
{
    /// <summary>
    /// 规则与
    /// </summary>
    public class AndCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
            _otherCriteria = otherCriteria ?? throw new ArgumentNullException(nameof(otherCriteria));
        }

        public IEnumerable<ShopContext> MeetCriteria(IEnumerable<ShopContext> shopContextList)
        {
            var firstCriteriaDealerInfoContextList = _criteria.MeetCriteria(shopContextList);
            return _otherCriteria.MeetCriteria(firstCriteriaDealerInfoContextList);
        }
    }
}
