﻿using System;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace AddressBookMVC.Models
{
    #region LOC_CountryModel
    public class LOC_CountryModel
    {
        #region CountryModel

       
        public int? CountryID { get; set; }


        [Required(ErrorMessage ="Please enter Country name")]
        [DisplayName("Country Name")]
        [StringLength(10,MinimumLength =3)]
        public string CountryName { get; set; }

        [Required(ErrorMessage ="Plaese enter Country Code")]
        [DisplayName("Country Code")]
        public string CountryCode { get; set; }


        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
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
