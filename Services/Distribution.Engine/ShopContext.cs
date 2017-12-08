using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Engine
{
    [ProtoContract]
    public class ShopContext
    {
        [ProtoMember(1)]
        public long ShopId { get; set; }
        [ProtoMember(2)]
        public double ShopPrice { get; set; }
        [ProtoMember(3)]
        public int ShopArea { get; set; }
        [ProtoMember(4)]
        public int BusinessId { get; set; }
        [ProtoMember(5)]
        public string AreaName { get; set; }
        [ProtoMember(6)]
        public string BurgName { get; set; }
        [ProtoMember(7)]
        public IList<int> Users { get; set; }
    }
}
