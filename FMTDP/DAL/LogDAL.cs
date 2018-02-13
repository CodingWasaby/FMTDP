using FMTDP.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.DAL
{
    public class LogDAL : BaseDAL
    {
        public LogDAL(IConfiguration configuration) : base(configuration) { }

        public void AddLog(CreateLog log)
        {
            Execute(@" INSERT  dbo.CreateLog
                                    ( Creator ,
                                      CreateDate ,
                                      DataID ,
                                      DataType ,
                                      OperateType
                                    )
                            VALUES  ( @Creator ,
                                      GETDATE() ,
                                      @DataID ,
                                      @DataType ,
                                      @OperateType
                                    ) ", log);
        }

        public async Task AddLogAsync(CreateLog log)
        {
            await Task.Factory.StartNew(() =>
            {
                AddLog(log);
            });
        }


    }
}
