using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IPlayerSettingsRepository : IRepository<PlayerSettings>
    {
        List<PlayerSettings> GetPlayersSettingsByMatchId(Guid id);
        void SetIsWritableToMatchPlayers(Match match);
    }
}
