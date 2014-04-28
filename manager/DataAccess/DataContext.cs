using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Migrations;
using DataAccess.Migrations;
using DomainModel.Entities;

namespace DataAccess
{
    public class DataContext : DbContext, IDataContext, IUnitOfWork
    {
        static DataContext()
        {
            try
            {
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
                Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataContext()
            : this("DefaultConnection")
        {
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, args) => DateTimeObjectMaterializer.Materialize(args.Entity);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<SkillsPlayer> SkillsPlayer { get; set; }
        public DbSet<EventLine> EventLines { get; set; }
        public DbSet<Arrangement> Arrangements { get; set; }
        public DbSet<Weather> Weather { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerSettings> PlayerSettings { get; set; }
        public DbSet<TeamSettings> TeamSettings { get; set; }
        public DbSet<Numbering> Numberings { get; set; }

        public IDbSet<T> GetSet<T>() where T : Entity
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<Guid>().Where(p => p.Name == "Id").Configure(p => p.IsKey());

            modelBuilder.Entity<User>().Property(e => e.Email).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<User>().Property(e => e.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.ParentId).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Skype).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Sex).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.City).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Birthday).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.AboutMySelf).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Language).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<User>().HasMany(e => e.PlayerCollection).WithRequired(e => e.User);
            modelBuilder.Entity<User>().Property(e => e.PublicId).IsRequired();

            modelBuilder.Entity<Country>().Property(e => e.Name).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<Country>().Property(e => e.PublicId).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<Country>().HasMany(e => e.TeamCollection).WithRequired(e => e.Country);
            modelBuilder.Entity<Country>().HasMany(e => e.PlayerCollection).WithRequired(e => e.Country);

            modelBuilder.Entity<Illness>().Property(e => e.IllnessName).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<Illness>().Property(e => e.TimeForRecovery).IsRequired();
            modelBuilder.Entity<Illness>().HasMany(e => e.PlayerCollection).WithRequired(e => e.Illness);

            modelBuilder.Entity<Position>().Property(e => e.Name).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<Position>().Property(e => e.PublicId).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<Position>().HasMany(e => e.PlayerCollection).WithRequired(e => e.Position);

            modelBuilder.Entity<Player>().Property(e => e.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Player>().Property(e => e.Surname).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Player>().Property(e => e.Age).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Weight).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Growth).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Number).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Salary).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Money).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Humor).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.Condition).IsRequired();
            modelBuilder.Entity<Player>().HasRequired(e => e.User);
            modelBuilder.Entity<Player>().HasRequired(e => e.Position);
            modelBuilder.Entity<Player>().HasRequired(e => e.Illness);
            modelBuilder.Entity<Player>().HasRequired(e => e.Country);
            modelBuilder.Entity<Player>().Property(e => e.PublicId).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.CreateDate).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.TeamId).IsOptional();
            modelBuilder.Entity<Player>().HasMany(e => e.SkillPlayerCollection).WithRequired(e => e.Player);
            modelBuilder.Entity<Player>().HasMany(e => e.EventLineCollection).WithRequired(e => e.Player);
            modelBuilder.Entity<Player>().HasMany(e => e.PlayerSettingsCollection).WithRequired(e => e.Player);

            modelBuilder.Entity<Team>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Team>().Property(e => e.ShortName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Team>().Property(e => e.Logo).IsRequired();
            modelBuilder.Entity<Team>().Property(e => e.CoachId);
            modelBuilder.Entity<Team>().Property(e => e.AssistantId);
            modelBuilder.Entity<Team>().HasRequired(e => e.Country);
            modelBuilder.Entity<Team>().Property(e => e.Stadium).IsRequired();
            modelBuilder.Entity<Team>().Property(e => e.Year).IsRequired();
            modelBuilder.Entity<Team>().HasMany(e => e.TeamSettingsCollection).WithRequired(e => e.Team);

            modelBuilder.Entity<Skill>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Skill>().Property(e => e.Ordering).IsRequired();
            modelBuilder.Entity<Skill>().HasMany(e => e.SkillPlayerCollection).WithRequired(e => e.Skill);

            modelBuilder.Entity<SkillsPlayer>().HasRequired(e => e.Skill);
            modelBuilder.Entity<SkillsPlayer>().HasRequired(e => e.Player);
            modelBuilder.Entity<SkillsPlayer>().Property(e => e.Value).IsRequired();

            modelBuilder.Entity<EventLine>().Property(e => e.LineId).IsRequired();
            modelBuilder.Entity<EventLine>().HasRequired(e => e.Player);
            modelBuilder.Entity<EventLine>().Property(e => e.Minute).IsRequired();
            modelBuilder.Entity<EventLine>().Property(e => e.Type).IsRequired();

            modelBuilder.Entity<Arrangement>().Property(e => e.Scheme).IsRequired();
            modelBuilder.Entity<Arrangement>().Property(e => e.Type).IsRequired();
            modelBuilder.Entity<Arrangement>().HasMany(e => e.TeamSettingsCollection).WithRequired(e => e.Arrangement);

            modelBuilder.Entity<Weather>().Property(e => e.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Weather>().Property(e => e.Type).IsRequired();
            modelBuilder.Entity<Weather>().HasMany(e => e.MatchCollection).WithRequired(e => e.Weather);

            modelBuilder.Entity<Match>().Property(e => e.HomeTeamId).IsRequired();
            modelBuilder.Entity<Match>().Property(e => e.GuestTeamId).IsRequired();
            modelBuilder.Entity<Match>().Property(e => e.EventLineId).IsRequired();
            modelBuilder.Entity<Match>().HasRequired(e => e.Weather);
            modelBuilder.Entity<Match>().Property(e => e.FansCount).IsRequired();
            modelBuilder.Entity<Match>().Property(e => e.TicketPrice).IsRequired();
            modelBuilder.Entity<Match>().Property(e => e.DateStart).IsRequired();
            modelBuilder.Entity<Match>().Property(e => e.Result).IsOptional();
            modelBuilder.Entity<Match>().Property(e => e.PublicId).IsRequired();
            modelBuilder.Entity<Match>().HasMany(e => e.PlayerSettingsCollection).WithRequired(e => e.Match);

            modelBuilder.Entity<PlayerSettings>().HasRequired(e => e.Player);
            modelBuilder.Entity<PlayerSettings>().HasRequired(e => e.Match);
            modelBuilder.Entity<PlayerSettings>().Property(e => e.IndexField).IsRequired();
            modelBuilder.Entity<PlayerSettings>().Property(e => e.Settings).IsOptional();
            modelBuilder.Entity<PlayerSettings>().Property(e => e.isCaptain).IsRequired();
            modelBuilder.Entity<PlayerSettings>().Property(e => e.isWritable).IsRequired();

            modelBuilder.Entity<TeamSettings>().HasRequired(e => e.Match);
            modelBuilder.Entity<TeamSettings>().HasRequired(e => e.Team);
            modelBuilder.Entity<TeamSettings>().HasRequired(e => e.Arrangement);
            modelBuilder.Entity<TeamSettings>().Property(e => e.Settings).IsOptional();
            modelBuilder.Entity<TeamSettings>().Property(e => e.LineUp).IsOptional();
            modelBuilder.Entity<TeamSettings>().Property(e => e.PlayerSend).IsOptional();

            modelBuilder.Entity<Numbering>().Property(e => e.Number).IsRequired();
            modelBuilder.Entity<Numbering>().Property(e => e.Type).IsRequired();


            base.OnModelCreating(modelBuilder);
        }

        public void Save()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var validationErrors in exception.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }

        }
    }
}
