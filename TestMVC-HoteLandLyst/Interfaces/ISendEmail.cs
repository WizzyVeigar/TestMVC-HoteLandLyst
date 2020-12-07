using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Entities;

namespace TestMVC_HoteLandLyst.Interfaces
{
    interface ISendEmail
    {
        void SendEmail(Email email);
    }
}
