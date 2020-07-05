using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrankProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }

        public Category() 
        { }

      public Category(int categoryId, string categoryName) 
        {
            CategoryId = categoryId;
            Name = categoryName;
        }
    }
}
