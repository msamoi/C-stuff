using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class AdminStuffController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AdminStuffController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    // GET: AdminStuff/CreateRole
    [Authorize(Roles = "admin")]
    public IActionResult CreateRole()
    {
        return View();
    }

    // POST: AdminStuff/CreateRole
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateRole(AdminStuffViewModel AdminStuffVM)
    {
        if (ModelState.IsValid)
        {
            var res = await _roleManager.CreateAsync(new IdentityRole(AdminStuffVM.Role));
            if (res.Succeeded) Console.WriteLine("Role " + AdminStuffVM.Role + " Created.");
            else Console.WriteLine(res.ToString());
        }

        return View(AdminStuffVM);
    }

    [Authorize(Roles = "admin")]
    public IActionResult GetRole()
    {
        ViewData["Roles"] = new SelectList(_roleManager.Roles);
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetRole(AdminStuffGetRoleVM viewModel)
    {
        var appUser = await _userManager.GetUserAsync(User);
        if (ModelState.IsValid && appUser != null)
        {
            var idres = _userManager.AddToRoleAsync(appUser, viewModel.Role);
            Console.WriteLine(idres);
        }
        
        return View(viewModel);
    }

    public IActionResult BecomeAdmin()
    {
        IdentityRole? adminRole = null; 
        while (adminRole == null)
        {
            try
            {
                adminRole = _roleManager.Roles.First(c => c.Name == "admin");
            }
            catch (InvalidOperationException)
            {
                _roleManager.CreateAsync(new IdentityRole("admin"));
            }
        }

        var appUser = _userManager.GetUserAsync(User);
        if (appUser.Result != null)
        {
            var idres = _userManager.AddToRoleAsync(appUser.Result, "admin");
            Console.WriteLine(idres);
        }
        
        return View();
    } 

}