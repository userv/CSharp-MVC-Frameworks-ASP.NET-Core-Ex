﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PANDA.Data;
using PANDA.Models;

namespace PANDA.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PandaUser> signInManager;
        private readonly UserManager<PandaUser> userManager;
        private readonly ILogger<RegisterModel> logger;
       // private readonly IEmailSender _emailSender;
        private readonly PandaDbContext dbContext;

        public RegisterModel(
            UserManager<PandaUser> userManager,
            SignInManager<PandaUser> signInManager,
            ILogger<RegisterModel> logger,
          //  IEmailSender emailSender,
            PandaDbContext ctx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
          //  _emailSender = emailSender;
            this.dbContext = ctx;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            IdentityResult result;
            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var user = new PandaUser { UserName = this.Input.Email, Email = this.Input.Email };
                if (!this.userManager.Users.Any())
                {
                    result = await this.userManager.CreateAsync(user, this.Input.Password);
                    var role=new PandaUserRole("Admin");
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    result = await this.userManager.CreateAsync(user, this.Input.Password);
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                 //   await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                 //      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
