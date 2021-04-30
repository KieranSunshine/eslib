using System.Text.Json.Serialization;

namespace Eslib.Models
{
    public class Position
    {
        public Position(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

        [JsonPropertyName("z")]
        public double Z { get; set; }
    }
}
