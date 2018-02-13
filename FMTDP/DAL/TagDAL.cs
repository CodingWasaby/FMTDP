using FMTDP.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.DAL
{
    public class TagDAL : BaseDAL
    {
        public TagDAL(IConfiguration configuration) : base(configuration) { }

        public int AddTag(Tag tag, SqlTransaction tran = null)
        {
            return GetSingle<int>(@" INSERT  dbo.Tag
                                            ( TagName )
                                    VALUES  ( @TagName )
                                    SELECT  @@IDENTITY ", tag, tran);
        }

        public bool AddTags(List<Tag> tags, SqlTransaction tran = null)
        {
            return BUlkToDB(tags, "Tag", tran);
        }

        public bool AddNexus(TagNexus tagNexus, SqlTransaction tran = null)
        {
            return Execute(@" INSERT  dbo.TagNexus
                            ( TagID, DataID, DataType )
                    VALUES  ( @TagID, @DataID, @DataType ) ", tagNexus, tran);
        }

        public bool DeleteNexus(int dataID, string dataType, int tagID = 0, SqlTransaction tran = null)
        {
            string sql = @" DELETE  dbo.TagNexus
                            WHERE   DataID = @DataID
                                    AND DataType = @DataType ";
            if (tagID > 0)
                sql += " AND TagID = @TagID ";
            return Execute(sql, new { DataID = dataID, DataType = dataType, TagID = tagID }, tran);
        }
    }
}
