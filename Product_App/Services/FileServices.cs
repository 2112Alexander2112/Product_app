using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Product_App.Services
{
    internal class FileServices
    {
        public string ReadFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void WriteFileContent(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}
