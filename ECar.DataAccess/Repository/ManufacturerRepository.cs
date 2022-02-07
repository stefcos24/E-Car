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
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        private readonly ApplicationDbContext _db;
        public ManufacturerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Manufacturer obj)
        {
            _db.Manufacturers.Update(obj);
        }
    }
}
