using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todoApp.Data;
using Microsoft.EntityFrameworkCore; 

namespace YourProjectName.Controllers
{
    public class AuthController : Controller
    {

        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserMail == email);

            if (user == null)
            {
                
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı!";
                return View();
            }

           
            if (user.UserPassword == password)  
            {

                 HttpContext.Session.SetInt32("UserId", user.UserId); 
                
                return RedirectToAction("Main", "Auth");
            }
            else
            {
                
                ViewBag.ErrorMessage = "Yanlış şifre!";
                return View();
            }
        }

        public IActionResult Signup(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User model)
        {
            if (ModelState.IsValid)
            {
                
                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("Main", "Auth");
            }

            
            return View(model);
        }

    public async Task<IActionResult> Settings()
    {
        var users = await _context.Users.ToListAsync(); 
        return View(users); 
    }

    public IActionResult Upcoming()
    {
        return View();
    }

    public IActionResult Today()
    {
        return View();
    }

    public IActionResult Calendar()
    {
        return View();
    }

    public async Task<IActionResult> Main()
{
    
    var userId = GetLoggedInUserId();

    if (userId == null)
    {
        
        return RedirectToAction("Login", "Auth");
    }

    
    var userNotes = await _context.Notes
                                  .Where(n => n.UserId == userId)
                                  .ToListAsync();

    
    return View(userNotes);
}

 
    [HttpPost]
    public ActionResult Delete(int id)
    {
        
        var note = _context.Notes.FirstOrDefault(n => n.NoteId == id);

        if (note == null)
        {
            return NotFound(); 
        }

        // Notu sil
        _context.Notes.Remove(note);
        _context.SaveChanges(); 

        
        return RedirectToAction("Main"); 
    }

[HttpPost]
public async Task<IActionResult> AddNote(string title, string content, string color)
{
    
    var userId = GetLoggedInUserId();

    if (userId == null)
    {
        return RedirectToAction("Login", "Auth");
    }

    
    var newNote = new Note
    {
        Title = title,
        Content = content,
        CreatedAt = DateTime.Now,
        UserId = userId.Value, 
        Color = color  
    };

    
    _context.Notes.Add(newNote);
    await _context.SaveChangesAsync();

    
    return RedirectToAction("Main", "Auth");
}

private int? GetLoggedInUserId()
{
    return HttpContext.Session.GetInt32("UserId");
}


    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> DeleteAllUsers()
        {
            var users = _context.Users.ToList(); 
            _context.Users.RemoveRange(users); 
            await _context.SaveChangesAsync(); 

            return RedirectToAction("Settings"); 
        }
    }
}
