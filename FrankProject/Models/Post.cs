using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrankProject.Models
{
    public class Post
    {
      
        public int PostId { get; set; }

        [Required(ErrorMessage = "Heading is required")]
        [StringLength(100)]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(550)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Url is required")]
        [Url]
        public string Url { get; set; }
        public string ÏmageUrl{ get; set; }
 
        public Category Category { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public Comment Comment { get; set; }






     //   public List<Comment> Comments { get; set; }


        public Post()
        { }
       public Post(string heading, string description, int categoryId)
        {
            Heading = heading;
            Description = description;
         
            CategoryId = categoryId;
        


        }

        internal Task<object> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
