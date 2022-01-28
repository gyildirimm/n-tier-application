using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public interface IApplicationCoreModule
    {
        void Load(IApplicationBuilder applicationBuilder);
    }
}
