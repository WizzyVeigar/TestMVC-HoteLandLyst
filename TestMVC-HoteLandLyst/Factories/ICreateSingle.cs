using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Factories
{
    interface ICreateSingle<T>
    {
        /// <summary>
        /// Creates a single object of the specified type
        /// </summary>
        /// <returns>returns the newly made object</returns>
        public T CreateSingle(DataRow row);
    }
}
