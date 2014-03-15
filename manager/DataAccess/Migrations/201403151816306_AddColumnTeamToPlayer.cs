namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTeamToPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "TeamId", c => c.Guid());
            AddColumn("dbo.PlayerSettings", "IndexField", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerSettings", "Index");

            Sql("update PlayerSettings set Settings = '{\"orient\":\"balance\",\"pas\":\"balance\",\"strike\":\"balance\",\"oneone\":\"hit\",\"canopy\":\"balance\",\"selection\":\"middle\",\"dedication\":\"balance\",\"penalty\":\"balance\"}' from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id where Players.Position_Id<>'C7C41812-D6BA-42FE-88B5-D93965D40DD8';" +
                "update PlayerSettings set Settings = '{\"oneone\":\"balance\",\"penalty\":\"balance\",\"dedication\":\"balance\",\"canopy\":\"balance\"}' from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id where Players.Position_Id='C7C41812-D6BA-42FE-88B5-D93965D40DD8'"+
                "update TeamSettings set Settings = '{\"corner\":\"c566524c-bab9-48c5-91eb-e40c41ec82ba\",\"freekick\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\",\"penalty\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\"}' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update TeamSettings set LineUp = '{\"1\":\"50ff51cb-efb2-4ed7-99e4-66d8ec02e579\",\"2\":\"7d89607b-4693-4202-9a6c-7d0d627eae8d\",\"3\":\"19c4a11e-4b06-4a06-8814-f7500bf3932f\",\"4\":\"46802ba5-ff77-4ae8-b0ca-e630b3fb1e88\",\"5\":\"cd44f089-8ffa-4cd6-a287-92ea0d11816f\",\"6\":\"c566524c-bab9-48c5-91eb-e40c41ec82ba\",\"7\":\"1e33c713-5b15-44bf-9bc9-354ee8dafc88\",\"8\":\"a621cbd6-6f07-41d3-a16d-e7514ec0fb18\",\"9\":\"e349a773-c11d-4547-b7b0-1e86279d3b89\",\"10\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\",\"11\":\"af5262d2-24d2-402b-83d6-986403d85a9f\",}' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update TeamSettings set PlayerSend = '96E3624E-0146-4887-AB4C-137545D7C3B5' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set isCaptain = 1 where PlayerSettings.Player_Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 1 where PlayerSettings.Player_Id='50ff51cb-efb2-4ed7-99e4-66d8ec02e579' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 2 where PlayerSettings.Player_Id='7d89607b-4693-4202-9a6c-7d0d627eae8d' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 3 where PlayerSettings.Player_Id='19c4a11e-4b06-4a06-8814-f7500bf3932f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 4 where PlayerSettings.Player_Id='46802ba5-ff77-4ae8-b0ca-e630b3fb1e88' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 5 where PlayerSettings.Player_Id='cd44f089-8ffa-4cd6-a287-92ea0d11816f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 6 where PlayerSettings.Player_Id='c566524c-bab9-48c5-91eb-e40c41ec82ba' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 7 where PlayerSettings.Player_Id='1e33c713-5b15-44bf-9bc9-354ee8dafc88' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 8 where PlayerSettings.Player_Id='a621cbd6-6f07-41d3-a16d-e7514ec0fb18' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 9 where PlayerSettings.Player_Id='e349a773-c11d-4547-b7b0-1e86279d3b89' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 10 where PlayerSettings.Player_Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 11 where PlayerSettings.Player_Id='af5262d2-24d2-402b-83d6-986403d85a9f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update TeamSettings set Settings = '{\"corner\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"freekick\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"penalty\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\"}' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update TeamSettings set LineUp = '{\"1\":\"C965D541-D3EF-49D3-9434-18C00A9C0C95\",\"2\":\"4E8BCBC7-77B5-428B-926A-7E8F9F483097\",\"3\":\"828877F4-D4D6-47B8-97E9-E8422DDF0B7E\",\"4\":\"5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2\",\"5\":\"D5ADF195-C185-4742-9CF7-4C971C069AEF\",\"6\":\"9C8773C0-DE77-4E75-891C-D5C037070CF2\",\"7\":\"B6871799-07C2-4322-818B-C5D3BAE1919B\",\"8\":\"7D12569A-9630-4CF0-84FC-580552034C39\",\"9\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"10\":\"A49AEFC3-D087-495F-8CF9-D8CF6C301847\",\"11\":\"C42B6F3C-4DA1-4237-9DFD-4462D4C7154A\",}' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update TeamSettings set PlayerSend = '8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set isCaptain = 1 where PlayerSettings.Player_Id='C965D541-D3EF-49D3-9434-18C00A9C0C95' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 1 where PlayerSettings.Player_Id='C965D541-D3EF-49D3-9434-18C00A9C0C95' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 2 where PlayerSettings.Player_Id='4E8BCBC7-77B5-428B-926A-7E8F9F483097' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 3 where PlayerSettings.Player_Id='828877F4-D4D6-47B8-97E9-E8422DDF0B7E' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 4 where PlayerSettings.Player_Id='5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 5 where PlayerSettings.Player_Id='D5ADF195-C185-4742-9CF7-4C971C069AEF' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 6 where PlayerSettings.Player_Id='9C8773C0-DE77-4E75-891C-D5C037070CF2' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 7 where PlayerSettings.Player_Id='B6871799-07C2-4322-818B-C5D3BAE1919B' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 8 where PlayerSettings.Player_Id='7D12569A-9630-4CF0-84FC-580552034C39' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 9 where PlayerSettings.Player_Id='06FE6EB1-7429-4AED-90E8-CBF1FED9281E' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 10 where PlayerSettings.Player_Id='A49AEFC3-D087-495F-8CF9-D8CF6C301847' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update PlayerSettings set IndexField = 11 where PlayerSettings.Player_Id='C42B6F3C-4DA1-4237-9DFD-4462D4C7154A' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='af5262d2-24d2-402b-83d6-986403d85a9f';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='eb78bba8-1651-49aa-b477-85ac3c608f3a';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='e349a773-c11d-4547-b7b0-1e86279d3b89';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='a621cbd6-6f07-41d3-a16d-e7514ec0fb18';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='1e33c713-5b15-44bf-9bc9-354ee8dafc88';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='6c8a8ebd-e6e5-446b-8f56-78449445b146';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='c566524c-bab9-48c5-91eb-e40c41ec82ba';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='f30b6ba8-21ea-40ac-9316-f1bb924846b7';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='7d89607b-4693-4202-9a6c-7d0d627eae8d';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='19c4a11e-4b06-4a06-8814-f7500bf3932f';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='46802ba5-ff77-4ae8-b0ca-e630b3fb1e88';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='cd44f089-8ffa-4cd6-a287-92ea0d11816f';"+
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='50ff51cb-efb2-4ed7-99e4-66d8ec02e579';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='A49AEFC3-D087-495F-8CF9-D8CF6C301847';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='C42B6F3C-4DA1-4237-9DFD-4462D4C7154A';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='414DBCFC-8B9F-43C2-9A96-27013462895F';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='06FE6EB1-7429-4AED-90E8-CBF1FED9281E';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='45E2CAE3-01B3-476B-BC12-C92EDEA9A704';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='7D12569A-9630-4CF0-84FC-580552034C39';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='B6871799-07C2-4322-818B-C5D3BAE1919B';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='9C8773C0-DE77-4E75-891C-D5C037070CF2';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='D5ADF195-C185-4742-9CF7-4C971C069AEF';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='E5C643D6-9933-4D8F-9297-E729B19A30EB';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='828877F4-D4D6-47B8-97E9-E8422DDF0B7E';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='4E8BCBC7-77B5-428B-926A-7E8F9F483097';"+
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='C965D541-D3EF-49D3-9434-18C00A9C0C95';");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerSettings", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerSettings", "IndexField");
            DropColumn("dbo.Players", "TeamId");
        }
    }
}
