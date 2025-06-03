using CodeAcademyEventManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Data
{
    public class EventSystemDB : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public EventSystemDB(DbContextOptions<EventSystemDB> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Participation)
                .WithOne(p => p.Invitation)
                .HasForeignKey<Participation>(p => p.InvitationId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventType)
                .WithMany(et => et.Events)
                .HasForeignKey(e => e.EventTypeId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Location)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.EventId);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Person)
                .WithMany(p => p.Invitations)
                .HasForeignKey(i => i.PersonId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.EventId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Feedbacks)
                .HasForeignKey(f => f.EventId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Person)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PersonId);

            // Seed data
            modelBuilder.Entity<EventType>().HasData(
                new EventType { Id = 1, Name = "Konfrans" },
                new EventType { Id = 2, Name = "Seminar" },
                new EventType { Id = 3, Name = "Bootcamp" },
                new EventType { Id = 4, Name = "Masterclass" }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Bakı Konfrans Mərkəzi", Address = "Nərimanov, Bakı", Capacity = 200 }
            );

            modelBuilder.Entity<Organizer>().HasData(
                new Organizer { Id = 1, FullName = "Code Academy", Email = "info@code.edu.az" }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Əli", Surname = "Əliyev", Email = "ali@mail.com", Phone = "0511111111", Role = "Tələbə" },
                new Person { Id = 2, Name = "Nigar", Surname = "Həsənova", Email = "nigar@mail.com", Phone = "0522222222", Role = "Müəllim" }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Title = "C# Bootcamp", Description = "Dərin C# təlimi", Date = DateTime.Now.AddDays(10), LocationId = 1, EventTypeId = 3, OrganizerId = 1 }
            );

            modelBuilder.Entity<Invitation>().HasData(
                new Invitation { Id = 1, EventId = 1, PersonId = 1, Status = "Accepted", SentAt = DateTime.Now }
            );

            modelBuilder.Entity<Participation>().HasData(
                new Participation { Id = 1, InvitationId = 1, CheckInTime = DateTime.Now.AddDays(10).AddHours(9), SeatNumber = "A12" }
            );

            modelBuilder.Entity<Notification>().HasData(
                new Notification { Id = 1, EventId = 1, Message = "Bootcamp sabah saat 10:00-da başlayır", SentAt = DateTime.Now.AddDays(9) }
            );

            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                {
                    Id = 1,
                    EventId = 1,
                    PersonId = 1,
                    Rating = 5,
                    Comment = "Əla tədbir idi!",
                    SubmittedAt = DateTime.Now
                });
        }
    }
}