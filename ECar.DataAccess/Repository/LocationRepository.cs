using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECar.DataAccess.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Location obj)
        {
            _db.Locations.Update(obj);
        }
    }
}
