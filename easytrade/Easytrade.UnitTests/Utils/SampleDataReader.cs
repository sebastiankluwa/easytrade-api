namespace Easytrade.UnitTests.Utils
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    internal static class SampleDataReader
    {
        internal static T GetTestObjectFromResource<T>(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string jsonInTextFile = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(jsonInTextFile);
                }
            }
        }
    }
}
