update PlayerSettings
set Settings = '{\"orient\":\"balance\",\"pas\":\"balance\",\"strike\":\"balance\",\"oneone\":\"hit\",\"canopy\":\"balance\",\"selection\":\"middle\",\"dedication\":\"balance\",\"penalty\":\"balance\"}'
from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id
where Players.Position_Id<>'C7C41812-D6BA-42FE-88B5-D93965D40DD8'

update PlayerSettings
set Settings = '{\"oneone\":\"balance\",\"penalty\":\"balance\",\"dedication\":\"balance\",\"canopy\":\"balance\"}'
from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id
where Players.Position_Id='C7C41812-D6BA-42FE-88B5-D93965D40DD8'




