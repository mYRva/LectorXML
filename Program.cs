using System;
using System.Xml; 


namespace lectorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "../../comprobantesNomina2018";          
            string[] files = System.IO.Directory.GetFiles(dir, "*.xml");
            double sum = 0.0;
            double impuestoRet = 0.0;
            int count = 0;
            foreach(string elem in files)
            {
 
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(elem);         

                string rfcEmisor = xmlDoc.GetElementsByTagName("cfdi:Emisor")[0]
                    .Attributes["Rfc"].Value;
                string fechaTimbrado = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0]
                    .Attributes["FechaTimbrado"].Value;
                string monto = xmlDoc.GetElementsByTagName("nomina12:Percepcion")[0]
                    .Attributes["ImporteGravado"].Value;    
                string impuestoRetenido = xmlDoc.GetElementsByTagName("nomina12:Deducciones")[0]
                    .Attributes["TotalImpuestosRetenidos"].Value;                    
            
                DateTime FechaPivote = new DateTime(2018,01,01);                

                if(rfcEmisor == ""  && DateTime.Parse(fechaTimbrado) > FechaPivote )
                {
                    sum +=double.Parse(monto);    
                    impuestoRet+=double.Parse(impuestoRetenido);       
                    count++;           
                    Console.WriteLine(elem);
                }                         

            } 

             Console.WriteLine("Total comprobantes: " + count + " Total: " + sum.ToString("C2") + " Impuesto Retenido: " + impuestoRet.ToString("C2"));
        }
    }
}
