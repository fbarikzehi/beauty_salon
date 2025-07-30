using Beauty.Model.Account.Permission;
using Beauty.Model.Account.Role;
using Beauty.Model.Account.User;
using Beauty.Model.Application.Setting;
using Beauty.Model.Appointment;
using Beauty.Model.Customer;
using Beauty.Model.Financial;
using Beauty.Model.Line;
using Beauty.Model.Personnel;
using Beauty.Model.Product;
using Beauty.Model.Salon;
using Beauty.Model.Service;
using Beauty.Model.Setting;
using Beauty.Model.Sms;
using Beauty.Persistence.EntityConfigurations.Application;
using Beauty.Persistence.EntityConfigurations.Appointment;
using Beauty.Persistence.EntityConfigurations.Customer;
using Beauty.Persistence.EntityConfigurations.Financial;
using Beauty.Persistence.EntityConfigurations.Line;
using Beauty.Persistence.EntityConfigurations.Permission;
using Beauty.Persistence.EntityConfigurations.Personnel;
using Beauty.Persistence.EntityConfigurations.Role;
using Beauty.Persistence.EntityConfigurations.Salon;
using Beauty.Persistence.EntityConfigurations.Service;
using Beauty.Persistence.EntityConfigurations.Setting;
using Beauty.Persistence.EntityConfigurations.Sms;
using Beauty.Persistence.EntityConfigurations.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Beauty.Persistence.Context
{
    public class CoreDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<SettingModel> Settings { get; set; }

        public DbSet<SalonModel> Salons { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<PersonnelProfileModel> Personnels { get; set; }

        public DbSet<FinancialYearModel> FinancialYears { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<CustomerProfileModel> Customers { get; set; }

        public DbSet<UnitModel> Units { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public DbSet<PermissionModel> Permissions { get; set; }

        public DbSet<LineModel> Lines { get; set; }

        public DbSet<ServicePackageModel> ServicePackages { get; set; }

        public DbSet<SmsMessageModel> SmsMessages { get; set; }
        public DbSet<SmsHistoryModel> SmsHistories { get; set; }

        public DbSet<CalendarModel> Calendars { get; set; }
        public DbSet<BankModel> Banks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)//Microsoft.Extensions.Configuration.FileExtensions
                                              .AddJsonFile("appsettings.json").Build();//Microsoft.Extensions.Configuration.Json

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CoreConnectionString"), x => x.MigrationsHistoryTable("__DbMigrationsHistory", "_"));
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();//to see the involved property values from models.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionActionEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SettingEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SalonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SalonContactEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceDetailEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PersonnelProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelContactEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelWorkingTimeEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new FinancialYearEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentServiceDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentPaymentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentDiscountEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerProfileEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new LineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LinePersonnelEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ServicePackageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServicePackageItemEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SmsMessageEntityTypeConfiguration());


            modelBuilder.Entity<ProfileModel>().HasQueryFilter(b => EF.Property<string>(b, "_tenantId") == _tenantId);
            modelBuilder.Entity<ProfileModel>().HasQueryFilter(p => !p.IsDeleted);


            using (var context = new CoreDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            using (var context = new CoreDbContext())
            {
                #region ContextDefaultTrackingBehavior
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                var blogs = context.ProfileModel.ToList();
                #endregion
            }


            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                        .ThenInclude(post => post.Author)
                            .ThenInclude(author => author.Photo)
                    .ToList();
            }
        }
    }
}
