using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Represents the current and next available states of an Item</summary>
    public class NextStatesDTO
    {
        public StateDTO CurrentState { get; set; }
        public List<StateDTO> NextAvailableStates { get; set; }
    }
}