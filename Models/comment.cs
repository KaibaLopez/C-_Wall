using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Wall.Models
{
    public class Comment
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int cId { get; set; }

        public int uId { get; set; }
        public User user { get; set; }

        public int mId { get; set; }
        public Message Message { get; set; }

        [Required]
        [Display(Name = "Post a Comment:")]
        public string text { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

}