using System.ComponentModel.DataAnnotations;

namespace E_Commarce_Website.Models
{
    public class Registration
    {
        [Key]
        public int cust_id { get; set; }
        [Required]
        public string cust_name { get; set; }
        [Required]
        public string cust_phone { get; set; }
        [Required,EmailAddress]
        public string cust_email { get; set; }
        [Required]
        public string cust_Password { get; set; }
        [Required]
        public string cust_country { get; set; }
        [Required]

        public string cust_city { get; set; }
        [Required]
        public string cust_address { get; set; }
        [Required]
        public string cust_gender { get; set; }
       


    }
}
