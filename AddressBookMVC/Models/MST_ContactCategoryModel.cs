using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookMVC.Models
{
    #region MST_ContactCategoryModel
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }


        [Required(ErrorMessage = "Please enter Contact Category")]
        [DisplayName("Contact Category")]
        [StringLength(10, MinimumLength = 3)]
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
