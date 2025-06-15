using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Location
{
    public class LocationCreateVM
    {
        [Required(ErrorMessage = "Məkan adı məcburidir.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Məkan adı 2 ilə 100 simvol arasında olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ünvan sahəsi məcburidir.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Ünvan 5 ilə 200 simvol arasında olmalıdır.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Tutuş sahəsi məcburidir.")]
        [Range(1, 10000, ErrorMessage = "Tutuş 1 ilə 10000 arasında olmalıdır.")]
        public int Capacity { get; set; }
    }
}
