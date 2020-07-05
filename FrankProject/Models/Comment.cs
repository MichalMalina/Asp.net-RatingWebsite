using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrankProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string Heading { get; set; }
        public int PostId { get; set; }
   
       

        public Comment() 
        
        { }
       public Comment( string content, string heading)
        {
         
            Content = content;
            Heading = heading;
        }
    }
}
