using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpelVagtplan.Models;

namespace SimpelVagtplan.Data
{
    public class SimpelVagtplanContext : DbContext
    {
        public SimpelVagtplanContext (DbContextOptions<SimpelVagtplanContext> options)
            : base(options)
        {
        }

        public DbSet<SimpelVagtplan.Models.Medarbejder> Medarbejder { get; set; } = default!;

        public DbSet<SimpelVagtplan.Models.Opgave> Opgave { get; set; }
    }
}
