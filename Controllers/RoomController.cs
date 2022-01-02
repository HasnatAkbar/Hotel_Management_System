using HotelManagementSystem1.Models;
using HotelManagementSystem1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem1.Controllers
{
    public class RoomController : Controller
    {
        private HotelDBEntities objHotelDBEntities;
        public RoomController()
        {
            objHotelDBEntities = new HotelDBEntities();
        }
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDBEntities.BookingStatus
                select new SelectListItem()
                {
                    Text = obj.BookingStatus,
                    Value = obj.BookingStatusid.ToString()
                }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objHotelDBEntities.RoomTypes
                select new SelectListItem()
                {
                    Text = obj.RoomTypeName,
                    Value = obj.RoomTypeid.ToString()
                }).ToList();

            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = string.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;

            if (objRoomViewModel.Roomid == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);

                objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                //objHotelDBEntities
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusid = objRoomViewModel.BookingStatusid,
                    IsActive = true,
                    RoomImage = ActualImageName,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeid = objRoomViewModel.RoomTypeid
                };
                objHotelDBEntities.Rooms.Add(objRoom);
                message = "Added.";
            }
            else
            {
                Room objRoom = objHotelDBEntities.Rooms.Single(model => model.Roomid == objRoomViewModel.Roomid);
                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                    objRoom.RoomImage = ActualImageName;
                }
                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.BookingStatusid = objRoomViewModel.BookingStatusid;
                objRoom.IsActive = true;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomTypeid = objRoomViewModel.RoomTypeid;
                message = "Updated.";
            }

            objHotelDBEntities.SaveChanges();
            return Json(data:new {message = "Room Successfully Added." + message, success = true}, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailViewModels =
                (from objRoom in objHotelDBEntities.Rooms
                    join objBooking in objHotelDBEntities.BookingStatus on objRoom.BookingStatusid equals objBooking
                        .BookingStatusid
                    join objRoomType in objHotelDBEntities.RoomTypes on objRoom.RoomTypeid equals objRoomType.RoomTypeid
                 where objRoom.IsActive == true
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     Roomid = objRoom.Roomid
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailViewModels);
        }

        [HttpGet]
        public JsonResult EditRoomDetails(int Roomid)
        {
            var result = objHotelDBEntities.Rooms.Single(model => model.Roomid == Roomid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeletRoomDetails(int Roomid)
        {
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.Roomid == Roomid);
            objRoom.IsActive = false;
            objHotelDBEntities.SaveChanges();
            return Json(data: new { message = "Record Successfully Deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}