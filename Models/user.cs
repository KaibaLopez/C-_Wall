using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Wall.Models
{
    public class User
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int uId { get; set; }

        //[Required]
        //[MinLength(2)]
        //[Display(Name = "First Name:")]
        public string first_name { get; set; }

        //[Required]
        //[MinLength(2)]
        //[Display(Name = "Last Name:")]
        public string last_name { get; set; }

        //[Required]
        //[EmailAddress]
        public string email { get; set; }

        //[Required]
        //[MinLength(8)]
        //[DataType(DataType.Password)]
        public string password { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        //// Will not be mapped to your users table!
        //[NotMapped]
        //[Compare("password")]
        //[DataType(DataType.Password)]
        //public string confirm { get; set; }

        public List<Message> messages { get; set; }
        public List<Comment> comments { get; set; }

    }
    

}