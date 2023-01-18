namespace back_end.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public float VAT { get; set; }
        public Country()
        {
            Name = "";
            Region = "";
        }
        public Country(string name, string region, float vat)
        {
            Name = name;
            Region = region;
            VAT = vat;
        }
    }
}
