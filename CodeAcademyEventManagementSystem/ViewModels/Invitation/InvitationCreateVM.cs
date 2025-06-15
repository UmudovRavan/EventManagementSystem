using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Invitation
{
    public class InvitationCreateVM
    {
        [Required(ErrorMessage = "Tədbir seçimi məcburidir.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Şəxs seçimi məcburidir.")]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Status sahəsi məcburidir.")]
        [StringLength(50, ErrorMessage = "Status 50 simvoldan uzun ola bilməz.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Göndərilmə tarixi məcburidir.")]
        [DataType(DataType.DateTime)]
        public DateTime SentAt { get; set; }
    }
}
