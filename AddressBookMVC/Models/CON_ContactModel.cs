using Microsoft.AspNetCore.Http;
using System;

namespace AddressBookMVC.Models
{
    #region CON_ContactModel
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public int ContactCategoryID { get; set; }
        public string ContactName { get; set; }
        public string CountryName { get; set; }
        public String StateName { get; set; }
        public String CityName { get; set; }    
        public string ContactCategory { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string MobileNo { get; set; }
        public string AlternetContact { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string Insta { get; set; }
        public string Gender { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }


    }
    #endregion

}
