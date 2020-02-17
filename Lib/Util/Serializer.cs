using Newtonsoft.Json;


namespace Library.Util
{
    public class Serializer
    {

        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };


        public static string Serialize<T>(T arg)
        {
            var JsonString = JsonConvert.SerializeObject(arg, settings);
            return JsonString;
        }

        public static T Deserialize<T>(T type, string JsonString)
        {

            return JsonConvert.DeserializeObject<T>(JsonString, settings);
        }

    }
}
