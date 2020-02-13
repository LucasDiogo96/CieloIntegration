using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain
{
    public class CieloResponse
    {
        #region private vars

        [JsonExtensionData]
        private IDictionary<string, object> _response;

        #endregion

        #region ctor

        public CieloResponse()
        {
            _response = new Dictionary<string, object>();
        }

        #endregion

        #region methods

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Status = EnumExtensions.ToEnum<Status>(_response["Status"]?.ToString());
            ReturnCode = EnumExtensions.ToEnum<ReturnCode>(_response["ReturnCode"]?.ToString());
            ReturnMessage = _response["ReturnMessage"]?.ToString();
        }

        #endregion

        #region properties

        public Status Status { get; private set; }
        public ReturnCode ReturnCode { get; private set; }
        public string ReturnMessage { get; private set; }

        #endregion
    }
}
