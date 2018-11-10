using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy
{
    public class IdRequest<T>
    {
        public T Id { get; set; }
    }


    /// <summary>
    /// 通用request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IdsRequest<T>
    {
        public List<T> Ids { get; set; }
    }
}
