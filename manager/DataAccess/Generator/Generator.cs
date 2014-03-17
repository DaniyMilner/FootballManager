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
            IPlayerSettingsRepository playerSettingsRepository)
        {
            Match match;
            Team home, guest;
            TeamSettings homeSettings, guestSettings;
            CustomTeamSettings customHomeTeamSettings, customGuestTeamSettings;
            List<CustomPlayerSettings> playersSettings;
            JavaScriptSerializer json = new JavaScriptSerializer();
            TeamInformation teamInfo = new TeamInformation();
            PlayerInformation playerInfo = new PlayerInformation();
            List<Player> homePlayers, guestPlayers;
            List<Player>[,] homeLineUp, guestLineUp;
            //1. выбор матчей для генерации и генерация в цикле
            match = matchRepository.Get(new Guid("FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00"));
            //2. закрепить атрибут isWritable для все игроков матча
            playerSettingsRepository.SetIsWritableToMatchPlayers(match);
            
            //3. собрать информацию о матче, состав, установки игроков, установки команды, экипировку 
            //   и присоеденить игроков к сетке поля
            
            home = teamRepository.Get(match.HomeTeamId);
            guest = teamRepository.Get(match.GuestTeamId);
            
            homePlayers = playerRepository.GetAllPlayersByTeamId(home.Id);
            guestPlayers = playerRepository.GetAllPlayersByTeamId(guest.Id);

            homeSettings = home.TeamSettingsCollection.FirstOrDefault(z => z.Match.Id == match.Id);
            guestSettings = guest.TeamSettingsCollection.FirstOrDefault(z => z.Match.Id == match.Id);

            //установки команды
            customHomeTeamSettings = teamInfo.GetTeamSettings(homeSettings, homePlayers);
            customGuestTeamSettings = teamInfo.GetTeamSettings(guestSettings, guestPlayers);

            //состав команды
            homeLineUp = teamInfo.GetTeamLineUp(homeSettings, homeSettings.Arrangement, homePlayers);
            guestLineUp = teamInfo.GetTeamLineUp(guestSettings, guestSettings.Arrangement, guestPlayers);
            
            //установки всех игроков
            playersSettings = playerInfo.GetPlayersSettingsByMatchId(playerSettingsRepository, match.Id);
            

            //4. высчитать расчетную силу каждого игрока и команды
            //5. в цикле генерировать события и вставлять их в список
            //6. из списка событий генерировать json и записать в базу
        }
    }
}
