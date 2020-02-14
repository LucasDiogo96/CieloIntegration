using Domain.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class CieloResponse
    {

        [JsonExtensionData]
        private IDictionary<string, object> _response;

        public CieloResponse()
        {
            _response = new Dictionary<string, object>();
        }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Status = EnumExtensions.ToEnum<Status>(_response["Status"]?.ToString());
            ReturnCode = EnumExtensions.ToEnum<ReturnCode>(_response["ReturnCode"]?.ToString());
            ReturnMessage = _response["ReturnMessage"]?.ToString();
        }

        public Status Status { get; private set; }
        public ReturnCode ReturnCode { get; private set; }
        public string ReturnMessage { get; private set; }

    }
}
