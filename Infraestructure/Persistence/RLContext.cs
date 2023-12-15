using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Infraestructure.Persistence
{
    public class RLContext : DbContext
    {
        public RLContext(DbContextOptions<RLContext> options) : base(options)
        {
        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}