using Distribution.Services.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Distribution.Services.Model;

namespace Distribution.Services.Repositories
{
    public class ShopInfoRepository
    {
        private IDbConnection conn = new SqlConnection(WebConfig.ShopHouseRW);

        public IEnumerable<ShopInfo> GetDistributionShopList()
        {
            const string strSql = "SELECT ShopID FROM dbo.ShopInfo (NOLOCK)";
            using (conn)
            {
                IEnumerable<ShopInfo>  list = conn.Query<ShopInfo>(strSql);
                return list;
            }
        }
    }
}
