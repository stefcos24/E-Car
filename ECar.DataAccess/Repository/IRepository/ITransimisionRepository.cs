﻿using ECar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECar.DataAccess.Repository.IRepository
{
    public interface ITransimisionRepository : IRepository<Transimision>
    {
        void Update(Transimision obj);
        void Save();
    }
}
