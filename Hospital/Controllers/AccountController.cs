using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private bool isPersistent;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _unitOfWork = unitOfWork; ;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Name, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password); 
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent = false);
                    return RedirectToAction("Index","Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        public async Task<IActionResult> DoctorRegistration()
        {
            var specializations = _unitOfWork.specializationRepository.GetSpecializations();
            var model = new DoctorFormViewModel();
            foreach(var Specialization in specializations)
            {
                model.SpecializationList.Add(new SelectListItem()
                {
                    Value = Specialization.Id.ToString(),

                    Text =  Specialization.Name

                }
                    
                    );
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRegistration(DoctorFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.registerViewModel.Email,
                    Email = model.registerViewModel.Email,
                    IsActive = true

                };
                var result = await userManager.CreateAsync(user, model.registerViewModel.Password);

                if(result.Succeeded)
                {
                    var doctor = new Doctor()
                    {
                        Address = model.Address,
                        Phone = model.Phone,
                        SpecializationId = model.Specialization,
                        Name = model.Name,
                        IsAvailable = true,

                    };
                    _unitOfWork.doctorRepository.Add(doctor);
                    _unitOfWork.Complete();
                }
            }
            return View();
        }

    }
}