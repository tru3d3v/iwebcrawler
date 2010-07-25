using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CrawlerNameSpace.StorageSystem
{
    class SettingsReader
    {
        private static String defaultConnection = 
            @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\netproject\Documents\CrawlerDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        
        public static String getConnectionString()
        {
            try
            {
                XmlTextReader reader = new XmlTextReader("../../../../Common/Settings.config");
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
                return defaultConnection;
            }
            return defaultConnection;
        }
    }
}
