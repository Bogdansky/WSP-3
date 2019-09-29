using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ПWS_3.Helpers
{
    public class ObjectSerialize : IXmlSerializable
    {
        public List<object> ObjectList { get; set; }
        public XmlSchema GetSchema()
        {
            return new XmlSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var obj in ObjectList)
            {
                //Provide elements for object item
                writer.WriteStartElement("Student");
                var properties = obj.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    //Provide elements for per property
                    writer.WriteElementString(propertyInfo.Name, propertyInfo.GetValue(obj).ToString());
                }
                writer.WriteEndElement();
            }
        }
    }
}