using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Models
{
    public class EFRepository : IRepository
    {
        private Context context { get; set; }

        public EFRepository (Context temp)
        {
            context = temp;
        }
        public IQueryable<Crash> Crash => context.Crash;
    }
}
