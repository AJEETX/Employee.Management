using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dotnet.Portal.App.ViewsModels;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dotnet.Portal.App.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public PaymentController(IDonationRepository donationRepository, IMemberRepository memberRepository, IMapper mapper)
        {
            _donationRepository = donationRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
            DonationVM = new PaymentViewModel();
        }

        [BindProperty]
        public PaymentViewModel DonationVM { get; set; }

        // GET: Donation
        public IActionResult Index()
        {
            var donations = _donationRepository.GetDonations();
            var donationsVM = new List<PaymentViewModel>();
            foreach (var donation in donations)
            {
                donationsVM.Add(new PaymentViewModel(donation));
            }
            //donationsVM = _mapper.Map<IEnumerable<DonationViewModel>>(donations);
            return View(donationsVM);
        }

        // GET: Donation/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            Payment donation = await _donationRepository.GetDonation(id);

            if (donation == null)
                return NotFound();

            DonationVM = new PaymentViewModel(donation);
            InitializeDonation();
            return View(DonationVM);
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            InitializeDonation();
            return View(DonationVM);
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel donationViewModel)
        {
            if (!ModelState.IsValid)
                return View(donationViewModel);

            Payment donation = new Payment
            {
                Amount = donationViewModel.Amount,
                Type = (PaymentType)donationViewModel.DonationType,
                Date = donationViewModel.Date,
                //Member = await _memberRepository.GetMember(donationViewModel.MemberId),
                MemberId = donationViewModel.MemberId
            };

            try
            {
                await _donationRepository.CreateEntity(donation);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                InitializeDonation();
                return View(DonationVM);
            }
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            Payment donation = await _donationRepository.GetDonation(id);

            if (donation == null)
                return NotFound();

            DonationVM = new PaymentViewModel(donation);
            InitializeDonation();
            return View(DonationVM);
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentViewModel donationViewModel)
        {
            if (id != donationViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(donationViewModel);

            Payment donation = await _donationRepository.GetDonation(id);

            donation.Amount = donationViewModel.Amount;
            donation.Date = donationViewModel.Date;
            donation.Member = await _memberRepository.GetMember(donationViewModel.MemberId);
            donation.MemberId = donationViewModel.MemberId;
            donation.Type = (PaymentType)donationViewModel.DonationType;

            try
            {
                await _donationRepository.UpdateEntity(donation);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                /* DonationVM = new DonationViewModel(donation); */
                InitializeDonation();
                return View(DonationVM);
            }
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            Payment donation = await _donationRepository.GetDonation(id);

            if (donation == null)
                return NotFound();

            DonationVM = new PaymentViewModel(donation);
            return View(DonationVM);
        }

        // POST: Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Payment donation = await _donationRepository.GetDonation(id);

            if (donation == null)
                return NotFound();

            try
            {
                await _donationRepository.DeleteEntity(id);
                TempData["Success"] = "Donation successfully deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                DonationVM = new PaymentViewModel(donation);
                return View(DonationVM);
            }
        }

        private void InitializeDonation()
        {
            DonationVM.Members = _memberRepository.GetMembers()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
        }
    }
}
