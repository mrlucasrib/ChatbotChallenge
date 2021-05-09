using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Api
{
    public class Github
    {
        [JsonPropertyName("name")]
        public string ImageUrl { get; set; }
        
        [JsonPropertyName("owner")]
        public Dictionary<string, object> Owners { get; set; }
        [JsonPropertyName("description")]
        public string RepositoryDescription { get; set; }
        [JsonPropertyName("html_url")]
        public string RepositoryUrl { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime RepositoryCreatedDatetime { get; set; }
        
        [JsonPropertyName("language")]
        public string RepositoryLanguage { get; set; }
    }
}