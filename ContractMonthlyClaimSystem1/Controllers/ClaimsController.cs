using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Linq;


using ContractMonthlyClaimSystem1.Models; // or whatever namespace your Claim model is in

namespace ContractMonthlyClaimSystem1.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Save the file
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                    var filePath = Path.Combine(uploadsFolder, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    claim.DocumentPath = filePath;
                }
                claim.Status = "Pending";
                claim.SubmissionDate = DateTime.Now;
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        public IActionResult PendingClaims()
        {
            var claims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(claims);
        }

        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PendingClaims");
        }

        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PendingClaims");
        }
    }
}

