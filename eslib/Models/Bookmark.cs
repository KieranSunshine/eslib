using System;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Bookmark
    {
        public Bookmark(
            int bookmarkId,
            DateTime dateCreated,
            int creatorId,
            string label,
            int locationId,
            string notes
        )
        {
            BookmarkId = bookmarkId;
            Created = dateCreated;
            CreatorId = creatorId;
            Label = label;
            LocationId = locationId;
            Notes = notes;            
        }

        [JsonPropertyName("bookmark_id")]
        public int BookmarkId { get; set; }

        [JsonPropertyName("coordinates")]
        public Position? Coordinates { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("creator_id")]
        public int CreatorId { get; set; }

        [JsonPropertyName("folder_id")]
        public int? FolderId { get; set; }

        [JsonPropertyName("item")]
        public Item? Item { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("location_id")]
        public int LocationId { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }
    }
}