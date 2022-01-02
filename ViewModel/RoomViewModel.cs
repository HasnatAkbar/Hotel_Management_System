using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem1.ViewModel
{
    public class RoomViewModel
    {
        public int Roomid { get; set; }
        [Display(Name = "Room No.")]
        [Required(ErrorMessage ="Room Number is Required.")]
        public string RoomNumber { get; set; }

        [Display(Name = "Room Image")]
        public string RoomImage { get; set; }

        [Display(Name = "Room Price")]
        [Required(ErrorMessage = "Room Price is Required.")]
        [Range(500, 10000, ErrorMessage = "Room Price Should be equal and greater than {1}")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking Status is Required.")]
        
        public int BookingStatusid { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Room Type is Required.")]
        public int RoomTypeid { get; set; }

        [Display(Name = "Room Capacity")]
        [Required(ErrorMessage = "Room Capacity is Required.")]
        [Range(1,8,ErrorMessage ="Room capacity should be equal and greater than {1}")]
        public int RoomCapacity { get; set; }
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Room Description")]
        [Required(ErrorMessage = "Room Description is Required.")]
        public string RoomDescription { get; set; }
        
        public List<SelectListItem> ListOfBookingStatus { get; set; }
        public List<SelectListItem> ListOfRoomType { get; set; }

    }
}