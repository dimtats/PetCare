using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skylakias.Models;
using System.Data.Entity;
using System.Net;
using Skylakias.ViewModels;

namespace Skylakias.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }
        [HttpPost]
        public ActionResult Save(ApplicationUser user)
        {
            if (user.Id == null)
            {
                _context.Users.Add(user);
            }
            else
            {
                var userInDb = _context.Users.Single(u => u.Id == user.Id);

                userInDb.Name = user.Name;
                userInDb.MembershipTypeId = user.MembershipTypeId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        public ViewResult Index()
        {
            var customers = _context.Users.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(string id)
        {
            var customer = _context.Users.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(string id)
        {
            var customer = _context.Users.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerViewModel
            {
                User = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }





    }
}