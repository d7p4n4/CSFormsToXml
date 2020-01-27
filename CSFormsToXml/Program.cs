using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFormsToXml
{
    class Program
    {
        private static readonly log4net.ILog _naplo = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string ALKALMAZASPARAMETER_XMLsPath = ConfigurationManager.AppSettings["XMLsPath"];
        private string ALKALMAZASPARAMETER_XSLsPath = ConfigurationManager.AppSettings["XSLsPath"];
        private string ALKALMAZASPARAMETER_PDFsPath = ConfigurationManager.AppSettings["PDFsPath"];
        private string ALKALMAZASPARAMETER_BatchesPath = ConfigurationManager.AppSettings["BatchesPath"];
        private string ALKALMAZASPARAMETER_FOPPath = ConfigurationManager.AppSettings["FOPPath"];
        private string ALKALMAZASPARAMETER_FolderPath = ConfigurationManager.AppSettings["FolderPath"];

        static void Main(string[] args)
        {
            try
            {
                string[] files =
                     Directory.GetFiles(_inpath, "*.xml", SearchOption.TopDirectoryOnly);

                CSPersistentGeneratorLib.EntityGenerate.entityGenerateMethods(files, _defaultNamespace, _outpath, templatesFolder);
                CSRestApiGeneratorLib.Program.MainMethod(_inpath, _outpath, _defaultNamespace, templatesFolder);

                foreach (var _file in files)
                {
                    string _filename = Path.GetFileNameWithoutExtension(_file);
                    Console.WriteLine(_filename);

                    Ac4yClass ac4y = DeserialiseMethod.deser(_file);

                    GenerateClass.generateClass(ac4y, _outpath, files, _defaultNamespace, templatesFolder);

                }
            } catch(Exception exception)
            {
                _naplo.Error(exception.StackTrace);
            }
        }
    }
}
