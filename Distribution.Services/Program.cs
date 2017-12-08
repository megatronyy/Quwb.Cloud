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
            shopInfo.GetDistributionShopList();
        }
    }
}
