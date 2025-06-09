using CodeAcademyEventManagementSystem.Repository.Implementation;
using CodeAcademyEventManagementSystem.Repository.Interface;

namespace CodeAcademyEventManagementSystem.Extentions
{
    public static class CustomRepositoryRegistration
    {
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
        }
    }
}
