using System;

namespace AddressBookMVC.Models
{
    #region LOC_CityModel
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        public int StateID { get; set; }    
        public string StateName { get; set; }   
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public DateTime CreationDate { get; set; }  
        public DateTime ModificationDate { get; set; }
    }
    #endregion

    #region LOC_City_SelectForDropDownModel
    public class LOC_City_SelectForDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
    #endregion

}
