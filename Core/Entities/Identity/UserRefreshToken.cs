using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class UserRefreshToken : BaseEntity<string>
    {
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
