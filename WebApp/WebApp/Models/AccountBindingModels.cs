using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApp.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatRodj { get; set; }
        public string Adresa { get; set; }
        public string TipKorisnika { get; set; }
        public string Url { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class GetUserBindingModel
    {
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatRodj { get; set; }
        public string Adresa { get; set; }
        public string TipKorisnika { get; set; }
        public string Status { get; set; }
        public string VrstaNaloga { get; set; }
        public string Url { get; set; }

        public GetUserBindingModel(ApplicationUser user)
        {
            Email = user.Email;
            Ime = user.Ime;
            Prezime = user.Prezime;
            DatRodj = user.DatRodj.ToString("yyyy-MM-dd");
            Adresa = user.Adresa;
            TipKorisnika = user.TipKorisnika;
            Status = user.Status;
            VrstaNaloga = user.VrstaNaloga;
            Url = user.Url;
        }
    }

    public class UpdateUserBindingModel
    {
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatRodj { get; set; }
        public string Adresa { get; set; }
        public string TipKorisnika { get; set; }

        public UpdateUserBindingModel() { }
    }
}
