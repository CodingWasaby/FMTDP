using FMTDP.Models.Entities;
using FMTDP.Models.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.DAL
{
    public class APIDAL : BaseDAL
    {
        public APIDAL(IConfiguration configuration) : base(configuration) { }

        public int AddAPI(API api, SqlTransaction tran = null)
        {
            return GetSingle<int>(@" INSERT  dbo.API
                                    ( APIName ,
                                      APIIntranetUrl ,
                                      APIInternetUrl ,
                                      APIType ,
                                      NeedApply ,
                                      FMSystemCode ,
                                      Description ,
                                      DeleteFlag
                                    )
                            VALUES  ( @APIName ,
                                      @APIIntranetUrl ,
                                      @APIInternetUrl ,
                                      @APIType ,
                                      @NeedApply ,
                                      @FMSystemCode ,
                                      @Description ,
                                      0
                                    ) ", api, tran);
        }

        public bool UpdateAPI(API api, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.API
                                SET     APIName = @APIName ,
                                        APIIntranetUrl = @APIIntranetUrl ,
                                        APIInternetUrl = @APIInternetUrl ,
                                        APIType = @APIType ,
                                        NeedApply = @NeedApply ,
                                        FMSystemCode = @FMSystemCode ,
                                        Description = @Description
                                WHERE   APICode = @APICode ", api, tran);
        }

        public bool DeleteAPI(int apiCode, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.API
                                SET     DeleteFlag = 1
                                WHERE   APICode = @APICode ", new { APICode = apiCode }, tran);
        }

        public List<APILog> GetAPIList()
        {
            return GetList<APILog>(@" SELECT  a.* ,
                                                cl.Creator ,
                                                cl.CreateDate
                                        FROM    dbo.API a ( NOLOCK )
                                                JOIN dbo.CreateLog cl ( NOLOCK ) ON a.APICode = cl.DataID
                                                                                    AND cl.OperateType = 'Create'
                                        WHERE   cl.DataType = @DataType ", new { DataType = DataType.API });
        }
    }
}
