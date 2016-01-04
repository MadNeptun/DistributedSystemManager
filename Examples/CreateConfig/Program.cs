using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MadNeptun.DistributedSystemManager.Service;

namespace CreateConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Neighbour> data = new List<Neighbour>();
            
            data.Add(new Neighbour { Address = "10.0.2.4", Id = 2, Port = 9090 });
            data.Add(new Neighbour { Address = "10.0.2.5", Id = 3, Port = 9090 });

            using (var stream = new FileStream("C:\\Temp\\configuration.xml", FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(stream,data);
                stream.Close();
            }
        }
    }
}
