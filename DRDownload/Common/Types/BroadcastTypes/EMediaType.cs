using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace DRDownload.Common.Types.BroadcastTypes
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EMediaType
    {
        radio, tv
    }
}

