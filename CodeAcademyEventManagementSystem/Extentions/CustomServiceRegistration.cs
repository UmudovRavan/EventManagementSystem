using CodeAcademyEventManagementSystem.Service.Implementation;
using CodeAcademyEventManagementSystem.Service.Interfaces;

namespace CodeAcademyEventManagementSystem.Extentions
{
    public static class CustomServiceRegistration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventTypeService, EventTypeService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOrganizerService, OrganizerService>();
            services.AddScoped<IParticipationService, ParticipationService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IDashboardService, DashboardService>();
        }
    }
}
