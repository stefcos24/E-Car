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
    public class GasRepository : Repository<Gas>, IGasRepository
    {
        private readonly ApplicationDbContext _db;
        public GasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Gas obj)
        {
            _db.Gas.Update(obj);
        }
    }
}
