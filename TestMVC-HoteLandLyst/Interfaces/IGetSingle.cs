using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Interfaces
{
    interface IGetSingle<T>
    {
        /// <summary>
        /// Gets a single object of the specified type
        /// </summary>
        /// <returns>returns the newly made object of type <see cref="T"/></returns>
        public T GetSingle(int id);
    }
}
