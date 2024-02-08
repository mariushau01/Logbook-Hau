using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Lib
{
    public class CsvRepository : IRepository
    {
        public CsvRepository(string path)
        {

        }
        public bool Add(Entry entry)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Entry entry)
        {
            throw new NotImplementedException();
        }

        public Entry? find(string id)
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
