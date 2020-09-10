namespace CountriesInfo
{
    internal class Country
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Capital { get; set; }
        public decimal Area { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }
    }
}