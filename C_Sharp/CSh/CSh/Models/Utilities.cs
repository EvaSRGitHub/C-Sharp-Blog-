using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSh.Models
{
    public static class Utilities
    {
        public static string ArticlePeace(string content)
        {
            if(content == null || content.Length < 500)
            {
                return content;
            }
            else
            {
                int pos = content.LastIndexOf(" ", 500);
                return content.Substring(0, pos)+" ...";
            }
           
        }
    }
}