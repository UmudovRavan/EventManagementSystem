using CodeAcademyEventManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<EventSystemDB>();

            string[] roleNames = { "Admin", "User", "Telebe", "Muellim", "Qonaq" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            string adminEmail = "ravanmu@code.edu.az";
            string adminPassword = "AdminPassword123!";
            string adminUserName = "admin-user";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    LastLoginIpAdr = "127.0.0.1"
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    if(!await context.Persons.AnyAsync(p=>p.Email == adminEmail))
                    {
                        var adminPerson = new Person
                        {
                            Name = "Sistem",
                            Surname = "Admini",
                            Email = adminEmail,
                            Phone = "000-000-0000",
                            Role = "Admin", // Bu, Person modelinizdəki biznes roludur
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        };
                        context.Persons.Add(adminPerson);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!await context.EventTypes.AnyAsync())
            {
                context.EventTypes.AddRange(
                    new EventType { Name = "Konfrans", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false },
                    new EventType { Name = "Seminar", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false },
                    new EventType { Name = "Bootcamp", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false },
                    new EventType { Name = "Masterclass", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Locations.AnyAsync())
            {
                context.Locations.AddRange(
                    new Location { Name = "Bakı Konfrans Merkezi", Address = "Nerimanov, Bakı", Capacity = 200, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Organizers.AnyAsync())
            {
                context.Organizers.AddRange(
                    new Organizer { FullName = "Code Academy", Email = "info@code.edu.az", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false }
                );
                await context.SaveChangesAsync();
            }
            if (!await context.Persons.AnyAsync(p => p.Email == "ali@mail.com" || p.Email == "nigar@mail.com"))
            {
                context.Persons.AddRange(
                    new Person { Name = "Ali", Surname = "Əliyev", Email = "ali@mail.com", Phone = "0511111111", Role = "Telebe", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false },
                    new Person { Name = "Nigar", Surname = "Həsənova", Email = "nigar@mail.com", Phone = "0522222222", Role = "Muellim", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsDeleted = false }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Events.AnyAsync())
            {
                var location = await context.Locations.FirstOrDefaultAsync(l => l.Name == "Baki Konfrans Merkezi");
                var eventType = await context.EventTypes.FirstOrDefaultAsync(et => et.Name == "Bootcamp");
                var organizer = await context.Organizers.FirstOrDefaultAsync(o => o.FullName == "Code Academy");

                if (location != null && eventType != null && organizer != null)
                {
                    context.Events.AddRange(
                        new Event
                        {
                            Title = "C# Bootcamp",
                            Description = "C# telimi",
                            Date = DateTime.Now.AddDays(10), 
                            LocationId = location.Id,
                            EventTypeId = eventType.Id,
                            OrganizerId = organizer.Id,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            if (!await context.Invitations.AnyAsync())
            {
                var @event = await context.Events.FirstOrDefaultAsync(e => e.Title == "C# Bootcamp");
                var personAli = await context.Persons.FirstOrDefaultAsync(p => p.Email == "ali@mail.com");

                if (@event != null && personAli != null)
                {
                    context.Invitations.AddRange(
                        new Invitation
                        {
                            EventId = @event.Id,
                            PersonId = personAli.Id,
                            Status = "Accepted",
                            SentAt = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            if (!await context.Participations.AnyAsync())
            {
                var invitation = await context.Invitations
                                        .Include(i => i.Event)
                                        .Include(i => i.Person)
                                        .FirstOrDefaultAsync(i => i.Event.Title == "C# Bootcamp" && i.Person.Email == "ali@mail.com");

                if (invitation != null)
                {
                    context.Participations.AddRange(
                        new Participation
                        {
                            InvitationId = invitation.Id,
                            CheckInTime = DateTime.Now.AddDays(10).AddHours(9),
                            SeatNumber = "A12",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            if (!await context.Notifications.AnyAsync())
            {
                var @event = await context.Events.FirstOrDefaultAsync(e => e.Title == "C# Bootcamp");

                if (@event != null)
                {
                    context.Notifications.AddRange(
                        new Notification
                        {
                            EventId = @event.Id,
                            Message = "Bootcamp sabah saat 10:00-da baslayir!",
                            SentAt = DateTime.Now.AddDays(9),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            if (!await context.Feedbacks.AnyAsync())
            {
                var @event = await context.Events.FirstOrDefaultAsync(e => e.Title == "C# Bootcamp");
                var personAli = await context.Persons.FirstOrDefaultAsync(p => p.Email == "ali@mail.com");

                if (@event != null && personAli != null)
                {
                    context.Feedbacks.AddRange(
                        new Feedback
                        {
                            EventId = @event.Id,
                            PersonId = personAli.Id,
                            Rating = 5,
                            Comment = "Ela tedbir idi cox beyendim.",
                            SubmittedAt = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
