using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TM.Desktop
{
    public static class AssemblyHelper
    {
        public static string getVersion(Version version,string format= "{0}.{1:00}.{2:00}.{3:00}")
        {
            if (format != null && format.Length > 0)
                return string.Format(format, version.Major, version.Minor, version.Build, version.Revision);
            else
                return version.ToString();
        }
    }
}
