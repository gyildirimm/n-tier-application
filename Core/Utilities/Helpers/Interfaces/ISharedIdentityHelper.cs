using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.Interfaces
{
    public interface ISharedIdentityHelper
    {
        public string GetUserId { get; }
        public List<string> GetUserRoles { get; }
    }
}
