using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Factories
{
    interface IFactory<T>
    {
        public IList<T> CreateAll();
    }
}
