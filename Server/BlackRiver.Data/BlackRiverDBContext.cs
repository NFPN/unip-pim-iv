using BlackRiver.Data.Models;
using System.Data.Entity;

namespace BlackRiver.Data
{
    public class BlackRiverDBContext : DbContext
    {
        public dbset<Hotel> Hotel { get; set; }

    }
}
