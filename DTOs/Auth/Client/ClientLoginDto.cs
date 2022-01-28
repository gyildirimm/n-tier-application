using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Auth
{
    public class ClientLoginDto : IDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
