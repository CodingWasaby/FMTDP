using FMTDP.Interface.DAL;
using FMTDP.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.DAL
{
    public class UserDAL : BaseDAL, IUserDAL
    {
        public UserDAL(IConfiguration configuration) : base(configuration) { }

        public bool AddUser(Users user, SqlTransaction tran = null)
        {
            return Execute(@" INSERT  dbo.Users
                                    ( UserName ,
                                      NickName ,
                                      HeadPic ,
                                      Email ,
                                      DeptName ,
                                      CompanyName ,
                                      WorkCity ,
                                      Sex ,
                                      WorkNo ,
                                      JoinDate
                                    )
                            VALUES  ( @UserName ,
                                      @NickName ,
                                      @HeadPic ,
                                      @Email ,
                                      @DeptName ,
                                      @CompanyName ,
                                      @WorkCity ,
                                      @Sex ,
                                      @WorkNo ,
                                      GETDATE()
                                    ) ", user, tran);
        }

        public bool UpdateUserInfo(Users user, SqlTransaction tran = null)
        {
            return Execute(@" UPDATE  dbo.Users
                                SET     NickName = @NickName ,
                                        HeadPic = @HeadPic
                                WHERE   WorkNo = @WorkNo ", user, tran);
        }

        public List<Users> GetUserList()
        {
            return GetList<Users>(@" SELECT * FROM  dbo.Users (NOLOCK) ", null);
        }

        public Users IsExist(string workNo)
        {
            return GetSingle<Users>(@" SELECT  *
                                        FROM    dbo.Users (NOLOCK)
                                        WHERE   WorkNo = @WorkNo ", new { WorkNo = workNo });
        }
    }
}
