namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEightTeams : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Users values('060ef6dc-57c7-4fff-b26c-4f2f5d8b04a6','user1@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real1',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('057c7df9-c438-4c9e-9e88-dcd2badad86b','user2@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real2',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('803bf3b9-d5bb-474c-9217-fd59cabc5f2d','user3@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real3',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('fe9d7875-5075-47f8-a7f1-e890336d73e9','user4@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real4',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('fb3d8733-f1cd-46ca-975c-4d88a2af801c','user5@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real5',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('11a72098-44ec-4d69-996f-4837a9dff9d4','user6@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real6',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('b4cd733a-4dc1-4d0d-8208-22f7392f7782','user7@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real7',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "Insert into Users values('588f30df-a290-4ac3-a97f-03209c1c1675','user8@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Real8',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru','000000');" +
                "insert into Teams values ('31466536-a94c-47a5-819b-15df6cf347cb', 'Arsenal','ARS','','060ef6dc-57c7-4fff-b26c-4f2f5d8b04a6',NULL,'Emirates Stadium','1886','BF2061D0-4230-49F0-830A-92F2C016BE38');" +
                "insert into Teams values ('fa9e5ba4-edde-4f77-ba98-8c154111d4e8', 'Chelsea','CHE','','057c7df9-c438-4c9e-9e88-dcd2badad86b',NULL,'Stamford Bridge','1905','BF2061D0-4230-49F0-830A-92F2C016BE38');" +
                "insert into Teams values ('f9f896fd-496b-4acd-a503-b2da5a97fc00', 'Manchester City','MC','','803bf3b9-d5bb-474c-9217-fd59cabc5f2d',NULL,'City of Manchester Stadium','1887','BF2061D0-4230-49F0-830A-92F2C016BE38');" +
                "insert into Teams values ('bebef5e4-d80c-4520-9694-b5d0db1b49f0', 'Sevilla','SEV','','fe9d7875-5075-47f8-a7f1-e890336d73e9',NULL,'Estadio Ramon Sanchez Pizjuan','1905','FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B');" +
                "insert into Teams values ('1069c4e2-dc68-4406-9b10-9592f2ee6b08', 'Atletiko Madrid','ATL','','fb3d8733-f1cd-46ca-975c-4d88a2af801c',NULL,'Estadio Vicente Calderon','1903','FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B');" +
                "insert into Teams values ('9bcf947b-77cd-456b-b89d-6724de0b1f2c', 'Dnipro','DD','','11a72098-44ec-4d69-996f-4837a9dff9d4',NULL,'Днепр Арена','1918','47A4EC4D-D649-4374-B43F-3056BED72DD3');" +
                "insert into Teams values ('e77d9ead-1412-4713-9e98-e764cd522456', 'Shahtar','SHD','','b4cd733a-4dc1-4d0d-8208-22f7392f7782',NULL,'Донбасс Арена','1936','47A4EC4D-D649-4374-B43F-3056BED72DD3');" +
                "insert into Teams values ('fddfb73a-c099-4bf3-bbd1-ca6283a7d1ff', 'Metalist','MEH','','588f30df-a290-4ac3-a97f-03209c1c1675',NULL,'Металлист','1925','47A4EC4D-D649-4374-B43F-3056BED72DD3')");
        }
        
        public override void Down()
        {
        }
    }
}
