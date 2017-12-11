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
        private IDbConnection conn; //= new SqlConnection(WebConfig.ShopHouseRW);

        public IEnumerable<ShopInfo> GetDistributionShopList()
        {
            const string strSql = "SELECT ShopID FROM dbo.ShopInfo (NOLOCK)";
            using (conn = new SqlConnection(WebConfig.ShopHouseRW))
            {
                IEnumerable<ShopInfo>  list = conn.Query<ShopInfo>(strSql);
                return list;
            }
        }

        public int ExecDistribution(int shopId)
        {
            const string strSp = "P_DistributionService";
            var para = new DynamicParameters();
            para.Add("@ShopID", shopId);

            using (conn = new SqlConnection(WebConfig.ShopHouseRW))
            {
                return conn.Execute(strSp, para, null, null, CommandType.StoredProcedure);
            }
        }
    }
}
