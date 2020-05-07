﻿using System;
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
using Microsoft.Extensions.Logging;

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private bool isPersistent;
        private readonly ILogger<AccountController> logger;   
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _unitOfWork = unitOfWork;
            this.logger = logger;
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
                var user = new IdentityUser {  UserName = model.Name, Email = model.Email };
             
                var result = await userManager.CreateAsync(user, model.Password); 
                if(result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail","Account", new { userid=user.Id, token=token},Request.Scheme);
                    logger.Log(LogLevel.Warning, confirmationLink);
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
            if (ModelState.IsValid)
            {
                /*                IdentityUser usermail;
                                if(model.Email.Contains('@'))
                                {
                                    usermail = await userManager.FindByEmailAsync(model.Email);
                                }
                                else
                                {
                                     usermail = await userManager.FindByNameAsync(model.Email);
                                }
                                */
                var user = await userManager.FindByNameAsync(model.Email) ?? await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed && await userManager.CheckPasswordAsync(user,model.Password))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed");
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
                var result = await signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null )
            {
                return RedirectToAction("Index","Home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"The user Id {userId} is not valid";
                return View("NotFound");
            }
            var result = await userManager.ConfirmEmailAsync(user,token);
            if(result.Succeeded)
            {
                return View();
            }
            return View();
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