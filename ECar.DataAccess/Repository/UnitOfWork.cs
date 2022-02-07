using ECar.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECar.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Condition = new ConditionRepository(_db);
            Gas = new GasRepository(_db);
            Location = new LocationRepository(_db);
            Manufacturer = new ManufacturerRepository(_db);
            Transimision = new TransimisionRepository(_db);
            Product = new ProductRepository(_db);
        }
        public IConditionRepository Condition { get; private set; }
        public IGasRepository Gas { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IManufacturerRepository Manufacturer { get; private set; }
        public ITransimisionRepository Transimision { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
