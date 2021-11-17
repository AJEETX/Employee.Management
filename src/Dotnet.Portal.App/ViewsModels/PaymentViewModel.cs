using Dotnet.Portal.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Portal.App.ViewsModels
{
    public class PaymentViewModel
    {
        public PaymentViewModel() { }

        public PaymentViewModel(Payment payment)
        {
            Id = payment.Id;
            Date = payment.Date;
            Amount = payment.Amount;
            PaymentType = (PaymentTypeViewModel)payment.Type;
            MemberId = payment.MemberId;
            Member = new EmployeeViewModel(payment.Member);
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The {0} field is required.")]
        public DateTime? Date { get; set; }
        
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "The {0} field is required.")]
        public decimal Amount { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public int Type { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public PaymentTypeViewModel PaymentType { get; set; }

        [Display(Name = "Member")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public Guid MemberId { get; set; }

        public EmployeeViewModel Member { get; set; }
        
        //public IEnumerable<MemberViewModel> Members { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
    }
}
