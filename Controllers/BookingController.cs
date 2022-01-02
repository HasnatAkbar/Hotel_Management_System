using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagementSystem1.Models;
using HotelManagementSystem1.ViewModel;

namespace HotelManagementSystem1.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBEntities objHotelDBEntities;
        public BookingController()
        {
            objHotelDBEntities = new HotelDBEntities();
        }
        // GET: Booking
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRoom in objHotelDBEntities.Rooms
                    where objRoom.BookingStatusid == 2
                    select new SelectListItem()
                    {
                        Text = objRoom.RoomNumber,
                        Value = objRoom.Roomid.ToString()
                    }
                ).ToList();
            return View(objBookingViewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            return Json(data: "", JsonRequestBehavior.AllowGet);
        }
    }
}