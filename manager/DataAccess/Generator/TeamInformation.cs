using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DomainModel.Entities;

namespace DataAccess.Generator
{
    public class TeamInformation
    {
        public CustomTeamSettings GetTeamSettings(TeamSettings teamSettings, List<Player> teamPlayers)
        {
            var json = new JavaScriptSerializer();
            var customTeamSettings = json.Deserialize<CustomTeamSettings>(teamSettings.Settings);
            customTeamSettings.PlayerCorner = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Corner);
            customTeamSettings.PlayerFreekick = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Freekick);
            customTeamSettings.PlayerPenalty = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Penalty);
            return customTeamSettings;
        }

        public List<Player>[,] GetTeamLineUp(TeamSettings teamSettings, Arrangement arrangement, List<Player> teamPlayers)
        {
            const int arrayLength = 3;
            var array = new List<Player>[arrayLength, arrayLength];
            var json = new JavaScriptSerializer();

            var customTeamLineUp = json.Deserialize<CustomLineUp>(teamSettings.LineUp);

            for (int i = 0; i < arrayLength; i++)
                for (int j = 0; j < arrayLength; j++)
                    array[i, j] = new List<Player>();

            switch (arrangement.Type)
            {
                case ArrangementType.Scheme451:
                case ArrangementType.Scheme433:
                case ArrangementType.Scheme442:
                    array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));
                    array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                    switch (arrangement.Type)
                    {
                        case ArrangementType.Scheme442:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                        case ArrangementType.Scheme433:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[0, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                        case ArrangementType.Scheme451:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                    }
                    break;
                case ArrangementType.Scheme352:
                case ArrangementType.Scheme343:
                    array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                    array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));
                    switch (arrangement.Type)
                    {
                        case ArrangementType.Scheme343:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[0, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                        case ArrangementType.Scheme352:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                    }
                    break;
                case ArrangementType.Scheme541:
                case ArrangementType.Scheme532:
                    array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));
                    array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                    array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    switch (arrangement.Type)
                    {
                        case ArrangementType.Scheme532:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                        case ArrangementType.Scheme541:
                            array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                            array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                            array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                            array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                            break;
                    }
                    break;
            }
            return array;
        }

        public List<Player>[,] ConvertToGuestLineUp(List<Player>[,] lineUp)
        {
            const int n = 3;
            var result = new List<Player>[n, n];

            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    result[n - 1 - i, n - 1 - j] = lineUp[i, j];

            return result;
        }
    }
}
