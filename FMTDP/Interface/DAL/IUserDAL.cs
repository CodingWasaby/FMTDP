using FMTDP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Interface.DAL
{
    public interface IUserDAL
    {
        bool AddUser(Users user, SqlTransaction tran = null);
        bool UpdateUserInfo(Users user, SqlTransaction tran = null);
        List<Users> GetUserList();
        Users IsExist(string workNo);
    }
}
