using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Generator
{
    public class PlayerInformation
    {
        public List<CustomPlayerSettings> GetPlayersSettingsByMatchId(IPlayerSettingsRepository playerSettingsRepository, Guid matchId)
        {
            var json = new JavaScriptSerializer();
            var settingsList = playerSettingsRepository.GetPlayersSettingsByMatchId(matchId);
            var result = new List<CustomPlayerSettings>();
            foreach (var item in settingsList)
            {
                var res = json.Deserialize<CustomPlayerSettings>(item.Settings);
                res.IsCaptain = item.isCaptain;
                res.TeamId = item.Player.TeamId;
                result.Add(res);
            }
            return result;
        }

        public double CaptainImpact(List<Player> teamPlayers, double currentTotal)
        {
            Player teamCaptain = null;
            bool isFind = false;
            foreach (var player in teamPlayers)
            {
                foreach (var item in player.PlayerSettingsCollection.ToList())
                {
                    if (item.isCaptain)
                    {
                        teamCaptain = item.Player;
                        isFind = true;
                        break;
                    }
                }
                if (isFind)
                    break;
            }

            if (teamCaptain != null)
            {
                foreach (var item in teamCaptain.SkillPlayerCollection.ToList())
                {
                    if (item.Skill.Name == "Leadership")
                    {
                        currentTotal += item.Value * 1.5;
                        break;
                    }
                }
            }

            return currentTotal;
        }
    }
}
