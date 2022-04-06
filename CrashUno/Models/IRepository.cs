using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Models
{
    public interface IRepository
    {
        IQueryable<Crash> Crash { get; }
        IQueryable<Location> Location { get; }
    }
}
