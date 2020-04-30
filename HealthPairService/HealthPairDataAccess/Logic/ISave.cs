using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDataAccess.Logic
{
    public interface ISave
    {
        public Task SaveAsync();
    }
}
