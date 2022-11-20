using System;

namespace AddressBookMVC.Models
{
    #region MST_ContactCategoryModel
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }
        public DateTime CreationDate { get; set; }  
        public DateTime ModificationDate { get; set; }

    }
    #endregion

    #region MST_ContactCategory_SelectForDropDownModel
    public class MST_ContactCategory_SelectForDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set;}          
    }
    #endregion

}
