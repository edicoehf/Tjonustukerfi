using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandtolvuApp.Models
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Service { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
       
        string barcode;
        public string Barcode 
        {
            get => barcode;
                    
            set
            {
                barcode = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Barcode));
            }
        }

        string json;
        public string Json 
        {
            get => json;

            set
            {
                json = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Json));
            }
        }
        public DateTime DateModified { get; set; }
    }
}
