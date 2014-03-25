using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class PlayerSettingsRepository : Repository<PlayerSettings>, IPlayerSettingsRepository
    {
        public PlayerSettingsRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public List<PlayerSettings> GetPlayersSettingsByMatchId(Guid id)
        {
            return _dataContext.GetSet<PlayerSettings>().Where(z => z.Match.Id == id).ToList();
        }

        public void SetIsWritableToMatchPlayers(Match match)
        {
            var data = _dataContext.GetSet<PlayerSettings>().Where(z => z.Match.Id == match.Id).ToList();
            foreach (var item in data)
                item.SetNotWritable();
        }
    }
}
