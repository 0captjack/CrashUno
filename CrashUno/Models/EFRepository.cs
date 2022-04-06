using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Models
{
    public class EFRepository : IRepository
    {
        private TrafficContext context { get; set; }
        public EFRepository (TrafficContext temp)
        {
            context = temp;
        }
        public IQueryable<Crash> Crash => context.Crash;

    }
}
