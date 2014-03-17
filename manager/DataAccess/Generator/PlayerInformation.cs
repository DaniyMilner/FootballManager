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
            JavaScriptSerializer json = new JavaScriptSerializer();
            var settingsList = playerSettingsRepository.GetPlayersSettingsByMatchId(matchId);
            List<CustomPlayerSettings> result = new List<CustomPlayerSettings>();
            foreach (var item in settingsList)
            {
                var res = json.Deserialize<CustomPlayerSettings>(item.Settings);
                res.IsCaptain = item.isCaptain;
                result.Add(res);
            }
            return result;
        }
    }
}
