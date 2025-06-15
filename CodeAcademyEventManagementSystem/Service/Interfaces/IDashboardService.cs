using CodeAcademyEventManagementSystem.Areas.Admin.Models;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetDashboardDataAsync();
    }
}
