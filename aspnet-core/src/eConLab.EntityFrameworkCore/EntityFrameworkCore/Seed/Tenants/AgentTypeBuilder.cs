using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using eConLab.Authorization;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;
using eConLab.Account;
using System.Xml.Linq;

namespace eConLab.EntityFrameworkCore.Seed.Tenants
{
    public class AgentTypeBuilder
    {
        private readonly eConLabDbContext _context; 

        public AgentTypeBuilder(eConLabDbContext context)
        {
            _context = context; 
        }

        public void Create()
        {
            CreateAgentTypes();
        }

        private void CreateAgentTypes()
        {
            // Admin role

            var typeAmanah = new AgencyType
            {
                Name = "أمانة منطقة المدينة المنورة",
                Description = "أمانة منطقة المدينة المنورة",
                CreationTime = System.DateTime.Now,
                CreatorUserId = 1,
            };

            var checkAmanah = _context.AgencyTypes.FirstOrDefault(s => s.Name == typeAmanah.Name);
            if (checkAmanah == null)
            {
                _context.AgencyTypes.Add(typeAmanah);
                _context.SaveChanges();
            }


             typeAmanah = new AgencyType
            {
                Name = "شركة مقاولات",
                Description = "شركة مقاولات",
                CreationTime = System.DateTime.Now,
                CreatorUserId = 1,
            };

             checkAmanah = _context.AgencyTypes.FirstOrDefault(s => s.Name == typeAmanah.Name);
            if (checkAmanah == null)
            {
                _context.AgencyTypes.Add(typeAmanah);
                _context.SaveChanges();
            }

            typeAmanah = new AgencyType
            {
                Name = "مكتب استشارات هندسية",
                Description = "مكتب استشارات هندسية",
                CreationTime = System.DateTime.Now,
                CreatorUserId = 1,
            };

            checkAmanah = _context.AgencyTypes.FirstOrDefault(s => s.Name == typeAmanah.Name);
            if (checkAmanah == null)
            {
                _context.AgencyTypes.Add(typeAmanah);
                _context.SaveChanges();
            }

            typeAmanah = new AgencyType
            {
                Name = "مختبرات المواد والبناء",
                Description = "مختبرات المواد والبناء",
                CreationTime = System.DateTime.Now,
                CreatorUserId = 1,
            };

            checkAmanah = _context.AgencyTypes.FirstOrDefault(s => s.Name == typeAmanah.Name);
            if (checkAmanah == null)
            {
                _context.AgencyTypes.Add(typeAmanah);
                _context.SaveChanges();
            }
        }
    }
}
