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
    public class ConditionRepository : Repository<Condition>, IConditionRepository
    {
        private readonly ApplicationDbContext _db;
        public ConditionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Condition obj)
        {
            _db.Conditions.Update(obj);
        }
    }
}
