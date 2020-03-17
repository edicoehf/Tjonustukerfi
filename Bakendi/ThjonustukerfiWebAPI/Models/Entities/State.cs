namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class State
    {
        public long Id { get; set; }
        public string Name { get; set; }    // í vinnslu, Kælir 1, kælir 2, frystir, búin
        public string JSON { get; set; }    // Hillunúmer
    }
}