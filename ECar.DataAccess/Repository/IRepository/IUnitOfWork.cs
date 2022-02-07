using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECar.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IConditionRepository Condition { get; }
        IGasRepository Gas { get; }
        ILocationRepository Location { get; }
        IManufacturerRepository Manufacturer { get; }
        ITransimisionRepository Transimision { get; }
        IProductRepository Product { get; }

        void Save();
    }
}
