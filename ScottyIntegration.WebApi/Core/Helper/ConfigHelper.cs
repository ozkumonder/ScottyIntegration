using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;
using ScottyIntegration.WebApi.Models.Global;

namespace ScottyIntegration.WebApi.Core.Helper
{

    #region -- Configuration Class --
    /// <summary>
    /// This Configuration class is basically just a set of 
    /// properties with a couple of static methods to manage
    /// the serialization to and deserialization from a
    /// simple XML file.
    /// </summary>
    [Serializable]
    public class ConfigHelper
    {
        public ConfigHelper()
        {

        }
        public static string ConfigFileName => "ConnectionCFG";
        public static string ReadPath => string.Concat(AppContext.BaseDirectory, "\\ConnectionCFG.config");
        public static string WritePath => AppContext.BaseDirectory + "\\";
        public static string ConfigFilePath(string configFileName)
        {
            return ReadPath;
        }
        public static string ConnectionStringWithConfigXml(string file)
        {
            var config = DeserializeDatabaseConfiguration(file);

            var connectionString = string.Concat(@"data source=", config.ServerName, ";initial catalog=",
                config.DatabaseName, ";user id=", config.DbUserName, ";password=", config.DbPassword.DecryptIt(),
                ";MultipleActiveResultSets=True;App=EntityFramework providerName=System.Data.SqlClient");
            return connectionString;
        }
        public static string WebConfigReadConnectionString(string webConfigFileName)
        {
            string str;
            try
            {
                var xmlDocument = new XmlDocument();
                //var startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                //string[] files = Directory.GetFiles(startDirectory, "*" + webConfigFileName + "*", SearchOption.AllDirectories);
                //foreach (string file in files)
                //{
                //    Console.WriteLine(file);

                //    xmlDocument.Load(file);
                //}
                xmlDocument.Load(ReadPath);
                str = xmlDocument.SelectSingleNode(string.Concat("configuration/connectionStrings/add/@connectionString"))?.Value.ToString();

            }
            catch (Exception exception)
            {
                str = exception.Message;
            }
            return str;
        }

        #region Serialize - Deserialize Operation

        public static string SerializeDatabaseConfiguration(string file, ConfigSettings settings)
        {
            var result = string.Empty;
            try
            {
                FileIOPermission perm = new FileIOPermission(FileIOPermissionAccess.Write, file);
                perm.Demand();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                var xs = new System.Xml.Serialization.XmlSerializer(settings.GetType());
                var writer = File.CreateText(string.Concat(file, "ConnectionCFG.config"));
                xs.Serialize(writer, settings, ns);
                writer.Flush();
                writer.Close();
                result = "OK";
            }
            catch (Exception e)
            {
                result = e.Message;
                //Logger.LogError(e.Message);
            }
            return result;
        }
        public static string SerializeDatabaseConfiguration(string file, object settings)
        {
            var result = string.Empty;
            try
            {
                var xs = new System.Xml.Serialization.XmlSerializer(settings.GetType());
                var writer = File.CreateText(string.Concat(file, "\\ConnectionCFG.config"));
                xs.Serialize(writer, settings);
                writer.Flush();
                writer.Close();
                result = "OK";
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        public static ConfigSettings DeserializeDatabaseConfiguration(string file)
        {
            ConfigSettings c = null;
            if (File.Exists(file))
            {
                var xs = new System.Xml.Serialization.XmlSerializer(
                    typeof(ConfigSettings));
                var reader = File.OpenText(file);
                c = (ConfigSettings)xs.Deserialize(reader);
                reader.Close();

            }
            return c;
        }

        #endregion


    }
    #endregion

}

