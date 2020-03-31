namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Messaging;
    using FoodForYou.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private readonly IRepository<ContactFormEntry> contactsRepository;

        private readonly IEmailSender emailSender;

        public ContactsController(IRepository<ContactFormEntry> contactsRepository, IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO: Extract to IP provider (service)
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var contactFormEntry = new ContactFormEntry
            {
                Name = model.Name,
                Email = model.Email,
                Title = model.Title,
                Content = model.Content,
                Ip = ip,
            };
            await this.contactsRepository.AddAsync(contactFormEntry);
            await this.contactsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                model.Email,
                model.Name,
                GlobalConstants.SystemEmail,
                model.Title,
                model.Content);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
