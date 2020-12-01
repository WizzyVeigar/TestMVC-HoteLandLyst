using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Interfaces
{
    public interface ICreateMultiple<T>
    {
        /// <summary>
        /// Creates a <see cref="IList{T}"/> of the specified type
        /// </summary>
        /// <returns></returns>
        IList<T> CreateAll();
    }
}
