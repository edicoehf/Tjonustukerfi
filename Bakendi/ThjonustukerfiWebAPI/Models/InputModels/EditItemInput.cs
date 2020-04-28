namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model to edit Item in the database.</summary>
    public class EditItemInput
    {
        public long? CategoryId { get; set; }   //  type not right
        public long? StateId { get; set; }      //  manually need to change state
        public long? ServiceID { get; set; }    //  service is not correct
        public long? OrderId { get; set; }      //  is not part of a correct order
        public bool? Sliced { get; set; }       // was marked wrong
        public bool? Filleted { get; set; }     // was marked wrong
        public string OtherCategory { get; set; }   // update other category
        public string OtherService { get; set; }    // update other service
        public string Details { get; set; }         // update item details
    }
}