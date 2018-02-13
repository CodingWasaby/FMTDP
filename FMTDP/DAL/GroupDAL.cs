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
    public class GroupDAL : BaseDAL
    {
        public GroupDAL(IConfiguration configuration) : base(configuration) { }

        public int AddGroup(Group group, SqlTransaction tran = null)
        {
            return GetSingle<int>(@" INSERT  dbo.[Group]
                                            ( GroupName ,
                                              OwnerCode ,
                                              Description ,
                                              Icon ,
                                              GroupType ,
                                              BelongCode ,
                                              DeleteFlag
                                            )
                                    VALUES  ( @GroupName ,
                                              @OwnerCode ,
                                              @Description ,
                                              @Icon ,
                                              @GroupType ,
                                              @BelongCode ,
                                              0
                                            )
                                    SELECT  @@IDENTITY ", group, tran);
        }

        public bool UpdateGroup(Group group, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.[Group]
                                SET     GroupName = @GroupName ,
                                        OwnerCode = @OwnerCode ,
                                        Description = @Description ,
                                        Icon = @Icon ,
                                        GroupType = @GroupType ,
                                        BelongCode = @BelongCode
                                WHERE   GroupCode = @GroupCode ", group, tran);
        }

        public bool DissolutionGroup(string groupCode, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.[Group]
                                SET     DeleteFlah = 1
                                WHERE   GroupCode = @GroupCode ", new { GroupCode = groupCode }, tran);
        }

        public List<GroupLog> GetGroups()
        {
            return GetList<GroupLog>(@"   SELECT  GroupName ,
                                                    OwnerCode ,
                                                    Description ,
                                                    Icon ,
                                                    GroupType ,
                                                    BelongCode ,
                                                    g.GroupCode ,
                                                    COUNT(m.WorkNo) GroupMembers ,
                                                    cl.Creator ,
                                                    cl.CreateDate
                                            FROM    dbo.[Group] g ( NOLOCK )
                                                    JOIN dbo.CreateLog cl ( NOLOCK ) ON g.GroupCode = cl.DataID
                                                                                        AND cl.OperateType = 'Create'
                                                    JOIN dbo.Members m ( NOLOCK ) ON m.GroupCode = g.GroupCode
                                            WHERE   g.DeleteFlag = 0
                                                    AND cl.DataType = @DataType
                                            GROUP BY g.GroupName ,
                                                    g.OwnerCode ,
                                                    g.Description ,
                                                    g.Icon ,
                                                    g.GroupType ,
                                                    g.BelongCode ,
                                                    g.GroupCode ,
                                                    cl.Creator ,
                                                    cl.CreateDate ", new { DataType = DataType.Group });
        }

        public bool AddMembers(List<Members> members, SqlTransaction tran = null)
        {
            return BUlkToDB(members, "Members", tran);
        }

        public bool QuitGroup(string workNo, string groupCode, SqlTransaction tran = null)
        {
            return Execute(@"   DELETE  dbo.Members
                                WHERE   WorkNo = @WorkNo
                                        AND GroupCode = @GroupCode ", new { WorkNo = workNo, GroupCode = groupCode }, tran);
        }
    }
}
