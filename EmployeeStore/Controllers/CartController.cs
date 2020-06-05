using EmployeeStore.Models;
using EmployeeStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IEmployeeRepository _context;
        private Cart cart;

        public CartController(IEmployeeRepository context, Cart cartService)
        {
            _context = context;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        // If AddToCart parameter Id name don't equal CartLine and Employee Id name, we have exception
        public async Task<RedirectToActionResult> AddToCart(int Id, string returnUrl, decimal sum)
        {
            Employee product = await _context.Employees
                .FirstOrDefaultAsync(p => p.Id == Id);
            sum = product.Price;
            if (product != null)
            {
                cart.AddItem(product, 1, sum);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int productId, string returnUrl)
        {
            Employee product = await _context.Employees
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
