using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Notification
{
    public class NotificationCreateVM
    {
        [Required(ErrorMessage = "Tədbir seçimi məcburidir.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Mesaj sahəsi məcburidir.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Mesaj 10 ilə 500 simvol arasında olmalıdır.")]
        public string Message { get; set; }
    }
}
