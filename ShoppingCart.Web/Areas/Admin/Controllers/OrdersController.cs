using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.DataAccess.Repositories.IRepositories;
using ShoppingCart.Models;
using ShoppingCart.Utility;
using System.Security.Claims;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region APICALL
        public IActionResult AllOrders(string status) 
        {
            IEnumerable<OrderHeader> orderHeaders;

            if(User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity  = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                orderHeaders = _unitOfWork.OrderHeader.GetAll(x => x.ApplicationUserId == claims.Value);
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusPending);
                    break;

                case "approved":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusApproved);
                    break;

                case "underprocess":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusInProcess);
                    break;
                case "shipped":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusShipped);
                    break;

                default:
                    break;
            }

            return Json(new {data=orderHeaders});

        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
