using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpelVagtplan.Data;
using System;
using System.Linq;

namespace SimpelVagtplan.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SimpelVagtplanContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SimpelVagtplanContext>>()))
            {
                SeedMedarbejdere(context);
                SeedOpgaver(context);
                
            }
        }

        private static void SeedOpgaver(SimpelVagtplanContext context)
        {
            if (context.Opgave.Any())
            {
                return;
            }
            context.Opgave.AddRange(
                new Opgave
                {
                    Title = "Lav opgave",
                    AgeLimit = 25
                },

                new Opgave
                {
                    Title = "Aflever opgave",
                    AgeLimit = 99
                }

            );
            context.SaveChanges();
        }

        public static void SeedMedarbejdere(SimpelVagtplanContext context)
        {
            if (context.Medarbejder.Any())
            {
                return;
            }
            context.Medarbejder.AddRange(
                new Medarbejder
                {
                    Name = "Magnus Jensen",
                    Age = 25
                },

                new Medarbejder
                {
                    Name = "Morten Clausen",
                    Age = 99
                },

                new Medarbejder
                {
                    Name = "Claus Padkær",
                    Age = 18
                }
            );
            context.SaveChanges();
        }
    }
}
