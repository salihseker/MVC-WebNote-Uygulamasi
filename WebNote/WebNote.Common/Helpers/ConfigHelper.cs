using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WebNote.Common.Helpers
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}
