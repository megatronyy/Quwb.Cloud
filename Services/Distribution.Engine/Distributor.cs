using Distribution.Engine.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Engine
{
    public sealed class Distributor
    {
        //日志记录列表
        private IList<ICriteria> _criteriaList = new List<ICriteria>();
        private IList<IEnumerable<ShopContext>> _contextList = new List<IEnumerable<ShopContext>>();

        //执行的上下文
        private IEnumerable<ShopContext> _shopContext;

        private Distributor() { }

        public Distributor(IEnumerable<ShopContext> shopContext)
        {
            if (shopContext == null)
                throw new ArgumentNullException("ShopContext");

            _shopContext = shopContext;
        }

        public Distributor Run(ICriteria criteria)
        {
            if(criteria == null)
                throw new ArgumentNullException("ICriteria");

            _shopContext = criteria.MeetCriteria(_shopContext);
            _criteriaList.Add(criteria);
            _contextList.Add(_shopContext);

            return this;
        }
    }
}
