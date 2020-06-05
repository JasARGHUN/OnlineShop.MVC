using EmployeeStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Controllers
{
    [Authorize(Roles = "admin")]
    [Authorize(Policy = "AdminRolePolicy")]
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;
        public int orderPages = 2; // Number of objects on 1 page Orders
        public int viewOrderDbPages = 3; // The number of objects on 1 page View Database.

        public OrderController(IOrderRepository repositoryService, Cart cartService)
        {
            _repository = repositoryService;
            _cart = cartService;
        }

        public ViewResult Index(int page = 1)
        {
            var qry = _repository.Orders.AsNoTracking().Where(o => !o.Shipped);
            var model = PagingList.Create(qry, orderPages, page);
            return View(model);
        }

        [HttpGet]
        public ViewResult ViewDataBase(string filter, int page = 1, string sortExpression = "OrderTime")
        {
            var data = _repository.Orders.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                data = data.Where(d => d.Name.Contains(filter));
            }
            var model = PagingList.Create(data, viewOrderDbPages, page, sortExpression, "OrderTime");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "ViewDataBase";
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkShipped(int orderID)
        {
            Order order = await _repository.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Checkout() =>
            View(new Models.Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ViewBag.ErrorMessage = $"Sorry, your cart is empty!";
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                order.TotalAmount = _cart.ComputeTotalValue();
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
