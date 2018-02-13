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
    public class FMSystemDAL : BaseDAL
    {
        public FMSystemDAL(IConfiguration configuration) : base(configuration) { }

        public int AddFMSystem(FMSystem fms, SqlTransaction tran = null)
        {
            return GetSingle<int>(@" INSERT  dbo.FMSystem
                                            ( FMSystemName ,
                                              FMSystemEName ,
                                              Abbreviation ,
                                              DeleteFlag ,
                                              LogoURL ,
                                              Instructions
		                                    )
                                    VALUES  ( @FMSystemName ,
                                              @FMSystemEName ,
                                              @Abbreviation ,
                                              0 ,
                                              @LogoURL ,
                                              @Instructions
		                                    ) SELECT  @@IDENTITY ", fms, tran);
        }

        public List<FMSystemLog> GetFMSystem()
        {
            return GetList<FMSystemLog>(@" SELECT  fms.* ,
                                            cl.Creator ,
                                            cl.CreateDate
                                    FROM    dbo.FMSystem fms ( NOLOCK )
                                            JOIN dbo.CreateLog cl ( NOLOCK ) ON fms.FMSystemCode = cl.DataID
                                            AND cl.OperateType = 'Create'
                                    WHERE   cl.DataType = @DataType ", new { DataType = DataType.FMSystem });
        }
    }
}
