namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input for editing items</summary>
    public class EditItemInput
    {
        public string Type { get; set; }    //  type not right
        public long? StateId { get; set; }   //  manually need to change state
        public long? ServiceID { get; set; } //  service is not correct
        public long? OrderId { get; set; }   //  is not part of a correct order
    }
}