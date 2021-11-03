using System.ComponentModel.DataAnnotations;

namespace Dotnet.Portal.App.ViewsModels
{
    public enum PaymentTypeViewModel
    {
        [Display(Name = "Salary")]
        Salary = 1,

        [Display(Name = "Allowance")]
        Allowance = 2
    }
}
