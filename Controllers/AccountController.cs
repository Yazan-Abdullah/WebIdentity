using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebIdentity.ViewModel;
namespace WebIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult List()
        {
            var UsersList = new RegesterListViewModel
            {
                Users = _userManager.Users.ToList()
            };
            return View(UsersList);
        }
        public IActionResult Regesters()
        {
                  
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Regesters(RegesterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
              var result=  await _userManager.CreateAsync(user, model.PassWorld);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors) // مشان اذا فيه ايور بالتسجيل الدخول
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string? Id)
        {
            var User = await _userManager.FindByIdAsync(Id);
            if(User == null)
            {
                return NotFound();
            }
            var UserEdit = new RegesterEditViewModel
            {
                Id = User.Id,
                Email = User.Email
            };
            return View(UserEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegesterEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(model.Id);
                User.Email = model.Email;
                User.Id = model.Id;
                var result = await _userManager.UpdateAsync(User);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Account");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string? Id)
        {
            var User = await _userManager.FindByIdAsync(Id);
            if (User == null)
            {
                return NotFound();
            }
            var UserEdit = new RegesterEditViewModel
            {
                Id = User.Id,
                Email = User.Email
            };
            return View(UserEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RegesterEditViewModel model)
        {
          
                var User = _userManager.Users.FirstOrDefault(x => x.Id == model.Id);
                var result = await _userManager.DeleteAsync(User);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Account");
                }
            return View(model);
        }
    }
}
