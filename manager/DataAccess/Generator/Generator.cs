using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
namespace DataAccess.Generator
{
    public class Generator
    {
        public void Run(IMatchRepository matchRepository, IEntityFactory entityFactory, ITeamRepository teamRepository,
            ICountryRepository countryRepository, ITeamSettingsRepository teamSettingsRepository,
            IPlayerRepository playerRepository, IArrangementRepository arrangementRepository,
            IPlayerSettingsRepository playerSettingsRepository, IEventLineRepository eventLineRepository)
        {
            var json = new JavaScriptSerializer();
            var teamInfo = new TeamInformation();
            var playerInfo = new PlayerInformation();

            //1. выбор матчей для генерации и генерация в цикле
            Match match = matchRepository.Get(new Guid("FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00"));
            //2. закрепить атрибут isWritable для все игроков матча
            playerSettingsRepository.SetIsWritableToMatchPlayers(match);

            //3. собрать информацию о матче, состав, установки игроков, установки команды, экипировку 
            //   и присоеденить игроков к сетке поля

            Team home = teamRepository.Get(match.HomeTeamId);
            Team guest = teamRepository.Get(match.GuestTeamId);

            List<Player> homePlayers = playerRepository.GetAllPlayersByTeamId(home.Id);
            List<Player> guestPlayers = playerRepository.GetAllPlayersByTeamId(guest.Id);

            TeamSettings homeSettings = home.TeamSettingsCollection.FirstOrDefault(z => z.Match.Id == match.Id);
            TeamSettings guestSettings = guest.TeamSettingsCollection.FirstOrDefault(z => z.Match.Id == match.Id);

            //установки команды
            CustomTeamSettings customHomeTeamSettings = teamInfo.GetTeamSettings(homeSettings, homePlayers);
            CustomTeamSettings customGuestTeamSettings = teamInfo.GetTeamSettings(guestSettings, guestPlayers);

            //состав команды
            List<Player>[,] homeLineUp = teamInfo.GetTeamLineUp(homeSettings, homeSettings.Arrangement, homePlayers);
            List<Player>[,] guestLineUp = teamInfo.GetTeamLineUp(guestSettings, guestSettings.Arrangement, guestPlayers);

            //голкиперы команд
            Player homeGoalkeeper = teamInfo.GetTeamGoalkeeper(homeSettings, homePlayers);
            Player guestGoalkeeper = teamInfo.GetTeamGoalkeeper(guestSettings, guestPlayers);

            //установки всех игроков
            List<CustomPlayerSettings> playersSettings = playerInfo.GetPlayersSettingsByMatchId(playerSettingsRepository, match.Id);

            //4. высчитать расчетную силу команды
            double totalHome = 0, totalGuest = 0;
            totalHome += homePlayers.Sum(homePlayer => homePlayer.SkillPlayerCollection.ToList().Sum(skill => skill.Value));
            totalGuest += guestPlayers.Sum(guestPlayer => guestPlayer.SkillPlayerCollection.ToList().Sum(skill => skill.Value));

            //домашняя команда *1.2
            totalHome *= 1.2;

            //влияние капитана
            totalHome = playerInfo.CaptainImpact(homePlayers, totalHome);
            totalGuest = playerInfo.CaptainImpact(guestPlayers, totalGuest);

            //влияние лидерства
            //TODO сделать влияние лидерства на расчетную силу команд

            //шанс на атаку
            int homeChance = 0, guestChance = 0;
            double total = Math.Round(totalHome + totalGuest);
            homeChance = Convert.ToInt32(Math.Round((totalHome / total) * 100, 0));
            guestChance = Convert.ToInt32(Math.Round((totalGuest / total) * 100, 0));


            //5. в цикле генерировать события и вставлять их в список
            //var resultList = new List<string>();
            var resultList = new List<MatchEvent>();
            var manager = new EventManager(eventLineRepository, match.EventLineId);
            var game = new GameManager();
            int currentMinute = 0;
            bool secondHalf = false;
            int homeGoal = 0, guestGoal = 0;
            var lineEvents = new List<EventLine>();

            resultList.Add(new MatchEvent(currentMinute, true, new List<MatchEventItem>()));
            manager.StartMatchEvent(resultList[resultList.Count - 1].EventsLine);

            do
            {
                currentMinute += game.GetMinute();
                if (game.TeamWithBall(homeChance, guestChance))
                {
                    resultList.Add(new MatchEvent(currentMinute, true, new List<MatchEventItem>()));
                    homeGoal += manager.MatchEvent(homeLineUp, guestLineUp, guestGoalkeeper,
                        currentMinute, customHomeTeamSettings, lineEvents, resultList[resultList.Count-1].EventsLine);
                }
                else
                {
                    resultList.Add(new MatchEvent(currentMinute, false, new List<MatchEventItem>()));
                    guestGoal += manager.MatchEvent(guestLineUp, homeLineUp, homeGoalkeeper,
                        currentMinute, customGuestTeamSettings, lineEvents, resultList[resultList.Count - 1].EventsLine);
                }
                if (!secondHalf && currentMinute > 45)
                {
                    resultList.Add(new MatchEvent(currentMinute, true, new List<MatchEventItem>()));
                    manager.EndFirstTime(resultList[resultList.Count - 1].EventsLine);
                    secondHalf = true;
                    currentMinute = 45;
                    resultList.Add(new MatchEvent(currentMinute, true, new List<MatchEventItem>()));
                    manager.StartSecondHalf(resultList[resultList.Count - 1].EventsLine);
                }
            } while (currentMinute < 90);

            resultList.Add(new MatchEvent(currentMinute, true, new List<MatchEventItem>()));
            manager.FinishMatchEvent(resultList[resultList.Count - 1].EventsLine);

            //6. из списка событий генерировать json и записать в базу
            match.SetResult(json.Serialize(resultList));
            int a = 0;
        }
    }
}
