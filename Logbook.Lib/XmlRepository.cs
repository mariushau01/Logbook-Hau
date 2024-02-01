    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logbook.Lib
{
    public class XmlRepository : IRepository
    {
        private XElement _rootElement;

        public XmlRepository(string path)
        {
            if(File.Exists(path))
            {
                _rootElement = XElement.Load(path);
            }
            else
            {
                _rootElement = new XElement("entries");
            }
        }

        public bool Add(Entry entry)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Entry entry)
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetAll()
        {
            var entries = from entry in this._rootElement.Descendants("entry")
                          select entry;

            // TODO:
            // - Object erstellen
            // - Liste zurückgeben
            throw new NotImplementedException();
            //return entries.ToList();
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
