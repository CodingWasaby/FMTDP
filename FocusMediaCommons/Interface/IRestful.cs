using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FocusMediaCommons.Interface
{
    public interface IRestful
    {
        TOut POST<TIn, TOut>(string url, TIn param);
        T POST<T>(string url, string data);
        T GET<T>(string url, List<Tuple<string, string>> queryStrings = null);
    }
}
