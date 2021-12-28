using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fondital.Client.Utils
{
    public static class JsonSerializerOpts
    {
        public static JsonSerializerOptions JsonOpts = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve };
    }
}