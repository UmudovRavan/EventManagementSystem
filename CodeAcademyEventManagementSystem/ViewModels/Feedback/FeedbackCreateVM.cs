using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Feedback
{
    public class FeedbackCreateVM
    {
        [Required(ErrorMessage = "Tədbir seçimi məcburidir.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Reytinq qeyd etmək məcburidir.")]
        [Range(1, 5, ErrorMessage = "Reytinq 1 ilə 5 arasında olmalıdır.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Şərh 500 simvoldan uzun ola bilməz.")]
        public string Comment { get; set; }
    }
}
