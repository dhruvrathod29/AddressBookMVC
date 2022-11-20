using System;

namespace AddressBookMVC.Models
{
    #region LOC_CountryModel
    public class LOC_CountryModel
    {
        #region CountryModel
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        #endregion

    }
    #endregion

    #region LOC_Country_SelectForDropDownModel
    public class LOC_Country_SelectForDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
    #endregion

}
