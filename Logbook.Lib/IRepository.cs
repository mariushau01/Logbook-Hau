using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Lib
{
    public interface IRepository
    {
        bool Add(Entry entry);

        bool Delete(Entry entry);

        bool Update(Entry entry);

        bool Save();

        List<Entry> GetAll();
    }
}
