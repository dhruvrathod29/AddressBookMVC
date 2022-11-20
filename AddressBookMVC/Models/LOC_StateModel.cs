using System;

namespace AddressBookMVC.Models
{
    #region LOC_StateModel
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
    #endregion

    #region LOC_State_SelectForDropDown
    public class LOC_State_SelectForDropDownModel
    {
        public int StateID { get; set; }
        public String StateName { get; set; }
    }
    #endregion

}
