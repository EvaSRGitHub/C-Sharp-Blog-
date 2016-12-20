using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSh.Models
{
    public class CommentViewModels
    {
      
        public int Id { get; set; }

        public string CommentContent { get; set; }

        public string UserId { get; set; }
        
        public int ArticleId { get; set; }


    }
}