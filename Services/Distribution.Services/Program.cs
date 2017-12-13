using Distribution.Services.Model;
using Distribution.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Services
{
    class Program
    { 
        static void Main(string[] args)
        {
            ShopInfoRepository shopInfo = new ShopInfoRepository();
            var list = shopInfo.GetDistributionShopList();
            foreach (ShopInfo info in list)
            {
                shopInfo.ExecDistribution(info.ShopID);
            }
        }
    }
}
