using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace CrawlerNameSpace.StorageSystem
{
    class SettingsReader
    {
        private static String defaultConnection = 
            @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\netproject\Documents\CrawlerDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        
        public static String getConnectionString()
        {
            string baseURI = "";
            try
            {
                string dllPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SettingsReader)).CodeBase);
                XmlTextReader reader = new XmlTextReader(dllPath + "\\Settings.config");
                baseURI = reader.BaseURI;
                bool b_configuration = false, b_connectionString = false;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("configuration")) b_configuration = true;
                            if (b_configuration && reader.Name.Equals("connectionString")) b_connectionString = true;
                            break;
                        case XmlNodeType.Text:
                            if (b_connectionString)
                            {
                                return reader.Value.Trim();
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (b_configuration && reader.Name.Equals("connectionString")) b_connectionString = false;
                            if (reader.Name.Equals("configuration")) b_configuration = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                StreamWriter stream = new
                StreamWriter("LOGGER.ERR", true);
                stream.WriteLine(" EXCEPT WHILE TRYING TO REACH DATABASE SETTINGS FILE AT: ");
                stream.WriteLine(baseURI + "\n");
                stream.WriteLine(" USING THE DEAFAULT DATABASE ADDRESS: C:\\Users\\netproject\\Documents\\CrawlerDB.mdf\n");
                stream.Close();

                return defaultConnection;
            }
            return defaultConnection;
        }
    }
}
