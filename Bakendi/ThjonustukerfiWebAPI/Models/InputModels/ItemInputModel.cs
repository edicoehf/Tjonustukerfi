namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model to add new Item to the database.</summary>
    public class ItemInputModel
    {
        public long TypeId { get; set; }
        public long ServiceId { get; set; }
    }
}