using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.HelperModules
{
    public static class EntityDumperHelper
    {
        public static string Dump<T>(T entity)
        {
            return new TypeSerializer<T>().SerializeToString(entity);
        }

        public static T LoadBack<T>(string dump)
        {
            return new TypeSerializer<T>().DeserializeFromString(dump);
        }
    }
}
