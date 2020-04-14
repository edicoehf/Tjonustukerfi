namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class ItemStateChangeInputModel
    {
        public long ItemId { get; set; }
        public long StateChangeTo { get; set; }
    }
}