using FocusMediaCommons.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Domain
{
    public class BaseDom
    {
        protected IRestful _Restful;
        public BaseDom(IRestful restful)
        {
            _Restful = restful;
        }
    }
}
