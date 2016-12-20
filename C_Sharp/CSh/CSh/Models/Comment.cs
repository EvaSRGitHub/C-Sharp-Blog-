using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSh.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string CommentContent { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public bool IsComentator(string name)
        {
            return this.User.UserName.Equals(name);
        }
    }
}