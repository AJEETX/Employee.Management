using System.ComponentModel.DataAnnotations;

namespace Dotnet.Portal.App.ViewsModels
{
    public enum PaymentTypeViewModel
    {
        [Display(Name = "Donation")]
        Donation = 1,

        [Display(Name = "Tithing")]
        Tithing = 2
    }
}
