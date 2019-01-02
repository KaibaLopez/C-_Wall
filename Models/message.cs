using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Wall.Models
{
    public class Message
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int mId { get; set; }

        public int uId { get; set; }
        public User user { get; set; }

        [Required]
        [Display(Name = "Post a Message:")]
        public string text { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public List<Comment> comments { get; set; }

    }

}