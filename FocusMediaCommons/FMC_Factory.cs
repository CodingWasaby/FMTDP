using FocusMediaCommons.Handler;
using FocusMediaCommons.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FocusMediaCommons
{
    public class FMC_Factory
    {
        private static IRestful _Restful;
        public static IRestful GetRestfulInstance()
        {
            if (_Restful == null)
                _Restful = new RestfulHandler();
            return _Restful;
        }
        public static IRestful GetRestful()
        {
            return new RestfulHandler();
        }
    }
}
