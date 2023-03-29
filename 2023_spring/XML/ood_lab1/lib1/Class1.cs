using Library;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace lib1
{ 
    public static class gardenReader
    {
        public static garden? ReadGarden(string path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            // Validator settings
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            // Here we add xsd files to namespaces we want to validate
            // (It's like XML -> Schemas setting in Visual Studio)
            settings.Schemas.Add("http://www.example.org/garden", "C:\\Users\\zhicongl\\source\\repos\\ood_lab1\\lib1\\exercise.xsd");

            // Processing XSI Schema Location attribute
            // (Disabled by default as it is a security risk). 
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            // A function delegate that will be called when 
            // validation error or warning occurs
            settings.ValidationEventHandler += ValidationHandler;

            FileStream fs = new FileStream(path, FileMode.Open);

            XmlReader reader = XmlReader.Create(fs, settings);
            int i = 0;
            while (reader.Read())
            {
                //Console.WriteLine(i);
                //++i;
            }
            fs.Close();
            fs = new FileStream(path, FileMode.Open);

            //reader.Close();



            XmlSerializer xmlSerializer= new XmlSerializer(typeof(garden));
            if (fs != null)
            {
                var result = (garden?)xmlSerializer.Deserialize(fs);
                fs.Close();
                return result;
            }
            return null;
        }

        private static void ValidationHandler(Object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("Warning: {0}", args.Message);
            else
                Console.WriteLine("Error: {0}", args.Message);
        }
    }


}

//namespace Library
//{
//    public partial class library
//    {
//        public List<author> get_authors()
//        {
//            var result = new List<author>();
//            foreach (var a in author){
//                result.Add(a);
//            }
//            return result;
//        }
//    }
//}