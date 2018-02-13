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
    public class LibraryDAL : BaseDAL
    {
        public LibraryDAL(IConfiguration configuration) : base(configuration) { }

        public int AddLibrary(Library library, SqlTransaction tran = null)
        {
            return GetSingle<int>(@" INSERT  dbo.Library
                                            ( LibraryName ,
                                              LibraryType ,
                                              Description ,
                                              DeleteFlag
                                            )
                                    VALUES  ( @LibraryName ,
                                              @LibraryType ,
                                              @Description ,
                                              0
                                            )
                                    SELECT  @@IDENTITY ", library, tran);
        }

        public bool UpDateLibrary(Library library, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.Library
                                SET     LibraryName = @LibraryName ,
                                        LibraryType = @LibraryType ,
                                        Description = @Description
                                WHERE   LibraryCode = @LibraryCode ", library, tran);
        }

        public bool DeleteLibrary(int code, SqlTransaction tran = null)
        {
            return Execute(@"  UPDATE  Library
                                SET     DeleteFlag = 1
                                WHERE   LibraryCode = @LibraryCode ", new { LibraryCode = code }, tran);
        }


        public List<LibraryLog> GetLibrary()
        {
            return GetList<LibraryLog>(@" SELECT  l.* ,
                                            cl.Creator ,
                                            cl.CreateDate
                                    FROM    dbo.Library l ( NOLOCK )
                                            JOIN dbo.CreateLog cl ( NOLOCK ) ON l.LibraryCode = cl.DataID
                                            AND cl.OperateType = 'Create'
                                    WHERE   cl.DataType = @DataType ", new { DataType = DataType.Library });
        }
    }
}
