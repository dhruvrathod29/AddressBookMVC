using System;

namespace AddressBookMVC.Models
{
    public class LOC_StateModel
    {
        public int? StateID{ get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; } 
        public string StateName { get; set; }   
        public string StateCode { get; set; }   
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }  



    }

    public class LOC_State_SelectForDropDownModel
    {
        public int StateID { get; set; }
        public String StateName { get; set; }
    }

}
