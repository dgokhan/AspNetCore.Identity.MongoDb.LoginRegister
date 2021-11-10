using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Identity.MongoDb.LoginRegister.Models.RequestModels.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [StringLength(100, ErrorMessage = "Minumum 6 karakter olmak zorunda", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
