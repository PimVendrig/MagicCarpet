namespace MagicCarpetWebApp.Models
{
    public class Station
    {

        public string code { get; set; }
        public Namen namen { get; set; }

        public override string ToString()
        {
            return namen != null ? namen.lang : base.ToString();
        }

    }
}