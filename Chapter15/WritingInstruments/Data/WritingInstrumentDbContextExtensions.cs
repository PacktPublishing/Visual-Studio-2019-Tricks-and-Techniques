using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WritingExample.Data
{
    public static class WritingInstrumentDbContextExtensions
    {
        public static void EnsureSeedData(this WritingInstrumentDbContext context, bool IsDevelopment = false)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Crayons.Any())
                {
                    context.Crayons.Add(new Crayon { HTMLColor = "#df1515" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#151fdf" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#15df7b" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#dfdc15" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#df4315" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#df15cd" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#15df2f" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#47435f" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#efa215" });
                    context.Crayons.Add(new Crayon { HTMLColor = "#e915ef" });
                    context.SaveChanges();
                }
            }
        }

        public static bool AllMigrationsApplied(this WritingInstrumentDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}