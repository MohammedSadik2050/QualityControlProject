using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Authorization;
using Abp.MultiTenancy;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;
using eConLab.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eConLab.LookupModel;
using eConLab.Enum;

namespace eConLab.EntityFrameworkCore.Seed.Host
{
  
    public class InitialLookupTypeCreator
    {
        private readonly eConLabDbContext _context;

        public InitialLookupTypeCreator(eConLabDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //var obj = _context.LookupApp.IgnoreQueryFilters().FirstOrDefault(r => r.LookupType == LookupTypes.Muncipilty);
            //if (obj == null)
            //{
            //    obj = _context.LookupApp.Add(new LookupApp() { Name ="العريجاء",LookupType= LookupTypes.Muncipilty }).Entity;
            //    _context.SaveChanges();
            //}
           
    }

   
    }
}
