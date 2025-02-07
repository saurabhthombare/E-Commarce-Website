using System.ComponentModel.DataAnnotations;

namespace E_Commarce_Website.Models
{
    public class Registration
    {
        [Key]
        public int cust_id { get; set; }
        public string cust_name { get; set; }
        public string cust_phone { get; set; }
        public string cust_email { get; set; }
        public string cust_Password { get; set; }
        public string cust_country { get; set; }

        public string cust_city { get; set; }
        public string cust_address { get; set; }
        public string cust_gender { get; set; }
        public string cust_image { get; set; }


    }
}
