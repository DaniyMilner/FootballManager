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
            IPlayerRepository playerRepository, IArrangementRepository arrangementRepository)
        {
            Match match;
            Team home, guest;
            TeamSettings homeSettings, guestSettings;
            CustomTeamSettings customHomeSettings, customGuestSettings;
            JavaScriptSerializer json = new JavaScriptSerializer();
            TeamInformation teamInfo = new TeamInformation();
            List<Player> homePlayers, guestPlayers;
            Arrangement homeArrangement, guestArrangement;
            List<Player>[,] homeLineUp, guestLineUp;
            //1. выбор матчей для генерации и генерация в цикле
            //2. закрепить атрибут isWritable для все игроков матча

            //3. собрать информацию о матче, состав, установки игроков, установки команды, экипировку
            match = matchRepository.Get(new Guid("FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00"));
            home = teamRepository.Get(match.HomeTeamId);
            guest = teamRepository.Get(match.GuestTeamId);

            homePlayers = playerRepository.GetAllPlayersByTeamId(home.Id);
            guestPlayers = playerRepository.GetAllPlayersByTeamId(guest.Id);

            homeSettings = teamSettingsRepository.GetSettingsForCurrentMatchAndTeam(home, match);
            guestSettings = teamSettingsRepository.GetSettingsForCurrentMatchAndTeam(guest, match);

            //установки команды
            customHomeSettings = teamInfo.GetTeamSettings(homeSettings, homePlayers);
            customGuestSettings = teamInfo.GetTeamSettings(guestSettings, guestPlayers);

            //состав команды
            homeArrangement = arrangementRepository.Get(new Guid("96BF6834-4C80-4F84-BC5A-DDDECD38B384"));//переделать
            guestArrangement = arrangementRepository.Get(new Guid("96BF6834-4C80-4F84-BC5A-DDDECD38B384"));//переделать
            homeLineUp = teamInfo.GetTeamLineUp(homeSettings, homeArrangement, homePlayers);
            guestLineUp = teamInfo.GetTeamLineUp(guestSettings, guestArrangement, guestPlayers);
            int i = 0;











            //4. присоеденить игроков к сетке поля и высчитать расчетную силу каждого игрока и команды
            //5. в цикле генерировать события и вставлять их в список
            //6. из списка событий генерировать json и записать в базу
        }
    }
}
