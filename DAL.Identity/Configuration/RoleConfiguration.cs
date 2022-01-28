using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            List<string> roles = Enum.GetNames(typeof(eRoleType)).ToList();
            List<ApplicationUserRole> seedRoles = new List<ApplicationUserRole>();
            
            foreach (var role in roles)
            {
                seedRoles.Add(new ApplicationUserRole() { Name = role, NormalizedName = role.ToUpper() });
            }

            builder.HasData(seedRoles);
        }
    }
}
