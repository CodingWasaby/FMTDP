using FMTDP.Interface.DAL;
using FMTDP.Models;
using FMTDP.Models.API;
using FMTDP.Models.Entities;
using FocusMediaCommons.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Domain
{
    public class UserDom : BaseDom
    {
        private IUserDAL _UserDAL;
        public UserDom(IUserDAL userDAL, IRestful restful) : base(restful)
        {
            _UserDAL = userDAL;
        }

        public ResultModel Login(string workNo, string pass, out Users user)
        {
            user = null;
            var em = _Restful.GET<Employee>("http://172.29.3.2:10002/FUMAuth/LoginFUM/" + workNo + "/" + pass);
            if (em.Result)
            {
                user = IsExist(workNo);
                if (user == null)
                {
                    user.CompanyName = em.CompanyName;
                    user.DeptName = em.DeptName;
                    user.Email = em.Email;
                    user.Sex = em.Sex;
                    user.UserName = em.UserName;
                    user.WorkCity = em.WorkCity;
                    user.WorkNo = em.WorkNo;
                    return new ResultModel { Code = 101, Message = "" };
                }
                else
                {
                    return new ResultModel { Code = 0, Message = "" };
                }
            }
            else
            {
                return new ResultModel { Code = 102, Message = em.Message };
            }
        }

        private Users IsExist(string workNo)
        {
            return _UserDAL.IsExist(workNo);
        }

        public bool Regist(Users user)
        {
            try
            {
                if (_UserDAL.AddUser(user))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
