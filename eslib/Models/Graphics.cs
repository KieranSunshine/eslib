namespace eslib.Models
{
    public class Graphics
    {
        public Graphics(
            string graphic,
            int layer,
            int part)
        {
            Graphic = graphic;
            Layer = layer;
            Part = part;
        }
        public int? Color { get; set; }
        public string Graphic { get; set; }
        public int Layer { get; set; }
        public int Part { get; set; }
    }
}