

using System.Collections.Generic;
using Newtonsoft.Json;

namespace OSRS.Application.Models.OpenAI.Model.Output
{
    public class ImageOutputModel
    {
        public string Url { get; set; }
    }
    
    public class DataModel
    {
        /// <summary>
        /// The url of the image result
        /// </summary>
        [JsonProperty("url")]

        public string Url { get; set; }

        /// <summary>
        /// The base64-encoded image data as returned by the API
        /// </summary>
        [JsonProperty("b64_json")]
        public string Base64Data { get; set; }

        /// <summary>
        /// The prompt that was used to generate the image, if there was any revision to the prompt.
        /// </summary>
        public string RevisedPrompt { get; set; }

    }
}