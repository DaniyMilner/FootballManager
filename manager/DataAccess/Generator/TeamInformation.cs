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
            JavaScriptSerializer json = new JavaScriptSerializer();
            CustomTeamSettings customTeamSettings = json.Deserialize<CustomTeamSettings>(teamSettings.Settings);
            customTeamSettings.PlayerCorner = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Corner);
            customTeamSettings.PlayerFreekick = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Freekick);
            customTeamSettings.PlayerPenalty = teamPlayers.FirstOrDefault(z => z.Id == customTeamSettings.Penalty);
            return customTeamSettings;
        }

        public List<Player>[,] GetTeamLineUp(TeamSettings teamSettings, Arrangement arrangement, List<Player> teamPlayers)
        {
            int arrayLength = 3;
            List<Player>[,] array = new List<Player>[arrayLength, arrayLength];
            JavaScriptSerializer json = new JavaScriptSerializer();

            var customTeamLineUp = json.Deserialize<CustomLineUp>(teamSettings.LineUp);

            for (int i = 0; i < arrayLength; i++)
                for (int j = 0; j < arrayLength; j++)
                    array[i, j] = new List<Player>();

            if (arrangement.Type == ArrangementType.Scheme442 || arrangement.Type == ArrangementType.Scheme433 || arrangement.Type == ArrangementType.Scheme451)
            {
                array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));
                array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));

                if (arrangement.Type == ArrangementType.Scheme442)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
                else if (arrangement.Type == ArrangementType.Scheme433)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[0, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
                else if (arrangement.Type == ArrangementType.Scheme451)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
            }
            else if (arrangement.Type == ArrangementType.Scheme343 || arrangement.Type == ArrangementType.Scheme352)
            {
                array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));

                if (arrangement.Type == ArrangementType.Scheme343)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[0, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
                else if (arrangement.Type == ArrangementType.Scheme352)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
            }
            else if (arrangement.Type == ArrangementType.Scheme532 || arrangement.Type == ArrangementType.Scheme541)
            {
                array[2, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Two));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Three));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Four));
                array[2, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Five));
                array[2, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Six));

                if (arrangement.Type == ArrangementType.Scheme532)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
                else if (arrangement.Type == ArrangementType.Scheme541)
                {
                    array[1, 0].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Seven));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eight));
                    array[1, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Nine));
                    array[1, 2].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Ten));
                    array[0, 1].Add(teamPlayers.FirstOrDefault(z => z.Id == customTeamLineUp.Eleven));
                }
            }
            return array;
        }
    }
}
