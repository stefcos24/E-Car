using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECar.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.ManufacturerId = obj.ManufacturerId;
                objFromDb.ConditionId = obj.ConditionId;
                objFromDb.LocationId = obj.LocationId;
                objFromDb.Year = obj.Year;
                objFromDb.Mileage = obj.Mileage;
                objFromDb.Kilowatt = obj.Kilowatt;
                objFromDb.Gas = obj.Gas;
                objFromDb.TransimisionId = obj.TransimisionId;
                objFromDb.Description = obj.Description;
                objFromDb.DatePublished = obj.DatePublished;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
