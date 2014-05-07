using Autofac;
using DataAccess.Repositories;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .As<IDataContext>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .As<IQuerableRepository<User>>();

            builder.RegisterType<PlayerRepository>()
                .As<IPlayerRepository>()
                .As<IQuerableRepository<Player>>();

            builder.RegisterType<PositionRepository>()
                .As<IPositionRepository>()
                .As<IQuerableRepository<Position>>();

            builder.RegisterType<CountryRepository>()
               .As<ICountryRepository>()
               .As<IQuerableRepository<Country>>();

            builder.RegisterType<TeamRepository>()
                .As<ITeamRepository>()
                .As<IQuerableRepository<Team>>();

            builder.RegisterType<SkillRepository>()
                .As<ISkillRepository>()
                .As<IQuerableRepository<Skill>>();

            builder.RegisterType<SkillsPlayerRepository>()
                .As<ISkillsPlayerRepository>()
                .As<IQuerableRepository<SkillsPlayer>>();

            builder.RegisterType<EventLineRepository>()
                .As<IEventLineRepository>()
                .As<IQuerableRepository<EventLine>>();

            builder.RegisterType<ArrangementRepository>()
               .As<IArrangementRepository>()
               .As<IQuerableRepository<Arrangement>>();

            builder.RegisterType<WeatherRepository>()
               .As<IWeatherRepository>()
               .As<IQuerableRepository<Weather>>();

            builder.RegisterType<MatchRepository>()
              .As<IMatchRepository>()
              .As<IQuerableRepository<Match>>();

            builder.RegisterType<PlayerSettingsRepository>()
              .As<IPlayerSettingsRepository>()
              .As<IQuerableRepository<PlayerSettings>>();

            builder.RegisterType<TeamSettingsRepository>()
              .As<ITeamSettingsRepository>()
              .As<IQuerableRepository<TeamSettings>>();

            builder.RegisterType<IllnessRepository>()
              .As<IIllnessRepository>()
              .As<IQuerableRepository<Illness>>();

            builder.RegisterType<NumberingRepository>()
              .As<INumberingRepository>()
              .As<IQuerableRepository<Numbering>>();

            builder.RegisterType<SeasonsRepository>()
              .As<ISeasonsRepository>()
              .As<IQuerableRepository<Seasons>>();

            builder.RegisterType<TournamentRepository>()
              .As<ITournamentRepository>()
              .As<IQuerableRepository<Tournament>>();

            builder.RegisterType<TournamentItemRepository>()
              .As<ITournamentItemRepository>()
              .As<IQuerableRepository<TournamentItem>>();

            base.Load(builder);
        }
    }
}
