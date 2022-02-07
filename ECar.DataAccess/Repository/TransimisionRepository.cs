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
    public class TransimisionRepository : Repository<Transimision>, ITransimisionRepository
    {
        private readonly ApplicationDbContext _db;
        public TransimisionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Transimision obj)
        {
            _db.Transimisions.Update(obj);
        }
    }
}
