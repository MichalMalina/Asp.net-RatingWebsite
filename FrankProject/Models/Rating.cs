using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrankProject.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
       
        public Post Post { get; set; }
        public int PostId { get; set; }
        public int Creativity { get; set; }
        public int Usability { get; set; }
        public int Design { get; set; }


        public Rating()
        { }
        public Rating(int postid, int creativity, int design, int usability)
        {
          
            PostId = postid;
            Creativity = creativity;
            Design = design;
            Usability = usability;


        }


    }
}
