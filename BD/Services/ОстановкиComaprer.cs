using BD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Services
{
    class ОстановкиComaprer : IEqualityComparer<Остановки>
    {
        public bool Equals(Остановки x, Остановки y)
        {
            return x.id_остановки == y.id_остановки;
        }

        public int GetHashCode(Остановки obj)
        {
            return obj.id_остановки ^ obj.id_улицы;
        }
    }
}
