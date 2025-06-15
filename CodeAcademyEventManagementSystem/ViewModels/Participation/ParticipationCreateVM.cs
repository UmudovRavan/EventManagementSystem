using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Participation
{
    public class ParticipationCreateVM
    {
        [Required(ErrorMessage = "Dəvətnamə seçimi məcburidir.")]
        public int InvitationId { get; set; }

        [Required(ErrorMessage = "Giriş vaxtı sahəsi məcburidir.")]
        [DataType(DataType.DateTime, ErrorMessage = "Keçərli bir tarix və vaxt daxil edin.")]
        public DateTime CheckInTime { get; set; }

        [StringLength(20, ErrorMessage = "Oturacaq nömrəsi 20 simvoldan uzun ola bilməz.")]
        public string SeatNumber { get; set; }
    }
}
