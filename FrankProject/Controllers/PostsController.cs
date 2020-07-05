using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrankProject.Data;
using FrankProject.Models;

namespace FrankProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PostsController(ApplicationDbContext context)
        {
            _context = context;
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ViewBag.userName = userName;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {

            var avgerages = _context.Rating
                  .Where(c => c.Creativity != null)
                  .GroupBy(g => g.PostId, c => c.Creativity)
                  .Select(g => new 
                  {
                      PostId = g.Key,
                      Average = g.Average()
                  }).ToString().ToList();
            ViewBag.vat = avgerages;


         //   var avg = _context.Rating.Average(m => m.Design);
         //   var avg2 = _context.Rating.Average(m => m.Creativity);
         //   var avg3 = _context.Rating.Average(m => m.Usability);


           var avg = _context.Rating.Average(m => m.Design);
           var avg2 = _context.Rating.Average(m => m.Creativity);
          var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
         ViewBag.avg3 = avg3;


            var applicationDbContext = _context.Post.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ViewBag.userName = userName;
            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);

            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;


            var post = await _context.Post
                .Include(p => p.Category)
               
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId");
       

            var cat = (from c in _context.Set<Category>()
                       select new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.CategoryId.ToString()
                       });
            ViewBag.Cats = new SelectList(cat, "Value", "Text");


            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Heading,Description,Url,ÏmageUrl,CategoryId,UserId,Creativity,Usability,Design")] Post post
        
            )
        {
            if (ModelState.IsValid)
            {

                _context.Add(post);
              
              
           
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", post.CategoryId);
   

            var cat = (from c in _context.Set<Category>()
                       select new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.CategoryId.ToString()
                       });
            ViewBag.Cats = new SelectList(cat, "Value", "Text");
            var mat = (from c in _context.Set<Post>()
                       select new SelectListItem
                       {
                           Text = c.Heading,
                           Value = c.PostId.ToString()
                       });
            ViewBag.Mats = new SelectList(mat, "Value", "Text");
            var postId = (from c in _context.Set<Post>()
                          select new SelectListItem
                          {
                              Text = c.Heading,
                              Value = c.PostId.ToString()
                          });
            ViewBag.Posti = new SelectList(postId, "Value", "Text");


            return View(post);
         
        }

   
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", post.CategoryId);
     
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Heading,Description,Url,ÏmageUrl,CategoryId")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", post.CategoryId);
         
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
            
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Blog(int? id)
        {

       

       var applicationDbContext = await _context.Post.FindAsync(id);

            var blogs = from b in _context.Post
                        where b.CategoryId.Equals(3)
                        select b;
            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;



            return View(blogs);
        }
        public async Task<IActionResult> Edu(int? id)
        {

            

            var applicationDbContext = await _context.Post.FindAsync(id);

            var blogs = from b in _context.Post
                        where b.CategoryId.Equals(6)
                        select b;
            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;



            return View(blogs);
        }
        public async Task<IActionResult> Port(int? id)
        {

       

            var applicationDbContext = await _context.Post.FindAsync(id);

            var blogs = from b in _context.Post
                        where b.CategoryId.Equals(5)
                        select b;



            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;



            return View(blogs);
        }
        public async Task<IActionResult> Social(int? id)
        {

            id = 31;

            var applicationDbContext = await _context.Post.FindAsync(id);

            var blogs = from b in _context.Post
                        where b.CategoryId.Equals(4)
                        select b;
            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;



            return View(blogs);
        }




        public async Task<IActionResult> RateIt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ViewBag.userName = userName;
            var avg = _context.Rating.Average(m => m.Design);
            var avg2 = _context.Rating.Average(m => m.Creativity);
            var avg3 = _context.Rating.Average(m => m.Usability);
            ViewBag.avg = avg;
            ViewBag.avg2 = avg2;
            ViewBag.avg3 = avg3;


            var post = await _context.Post
                .Include(p => p.Category)
             
                .FirstOrDefaultAsync(m => m.PostId == id);
          
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        public IActionResult RatingSubmitted()
        {
            return View();
        }


        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
