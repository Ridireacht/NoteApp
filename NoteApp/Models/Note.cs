﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace NoteApp.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
