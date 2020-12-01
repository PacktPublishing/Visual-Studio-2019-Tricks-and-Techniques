using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WritingExample.Data
{
    public class WritingInstrumentDbContext : IdentityDbContext
    {
        public WritingInstrumentDbContext(DbContextOptions<WritingInstrumentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Crayon> Crayons { get; set; }
    }
}
