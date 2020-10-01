using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public interface IFile
    {
        void SaveToFile(Stream stream, Dictionary<string, string> nameAndIds);
        Dictionary<string, string> LoadFromFile(Stream stream);
    }
}
