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
        private readonly XElement _rootElement;

        private readonly string _path;

        public XmlRepository(string path)
        {
            _path = path;
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
            XElement node = new("entry");

            var idAttrib = new XAttribute("id", entry.Id.ToString());
            node.Add(idAttrib);

            var startAttrib = new XAttribute("start", entry.Start.ToString());
            node.Add(startAttrib);

            var endAttrib = new XAttribute("end", entry.End.ToString());
            node.Add(endAttrib);

            var startKmAttrib = new XAttribute("startkm", entry.StartKM.ToString());
            node.Add(startKmAttrib);

            var endKmAttrib = new XAttribute("endkm", entry.EndKM.ToString());
            node.Add(endKmAttrib);

            var numberPlateAttrib = new XAttribute("numberplate", entry.NumberPlate.ToString());
            node.Add(numberPlateAttrib);

            var fromAttrib = new XAttribute("from", entry.From.ToString());
            node.Add(fromAttrib);

            var toAttrib = new XAttribute("to", entry.To.ToString());
            node.Add(toAttrib);

            node.Add(entry.Description.ToString());

            _rootElement.Add(node);

            return this.Save();
        }

        public bool Delete(Entry entry)
        {
            var itemsDel = from e in _rootElement.Descendants("entry")
                           where (string)e.Attribute("id") == entry.Id
                           select e;

            itemsDel.Remove();

            return this.Save();
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
            try
            {
                _rootElement.Save(_path);
                return true;
            }
            catch( Exception ex)
            {
                return false;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public bool Update(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
