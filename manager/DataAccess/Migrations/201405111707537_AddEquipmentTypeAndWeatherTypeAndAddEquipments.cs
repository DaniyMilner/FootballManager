namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentTypeAndWeatherTypeAndAddEquipments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "WeatherType", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "Index", c => c.Int(nullable: false));

            Sql("insert into dbo.Equipments values ('3dd5f475-d223-4feb-9612-860b2a82cbac', 'Перчатки UhlSports #1', 350, 10, 6, 0, 1, 1);" +
                "insert into dbo.Equipments values ('3c22555f-7db0-470f-b583-6719cd816fa8', 'Перчатки UhlSports #2', 650, 15, 9, 0, 1, 2);" +
                "insert into dbo.Equipments values ('e3a04a18-8c0e-42d9-9ce2-0b4a8a30e820', 'Перчатки UhlSports #3', 1000, 20, 12, 0, 1, 3);" +
                "insert into dbo.Equipments values ('4202db2f-36a0-479c-968f-28bdaa1b9572', 'Перчатки Patrick', 280, 10, 6, 0, 2, 4);" +
                "insert into dbo.Equipments values ('c7084e80-67f2-4bfd-99af-2541ee0435d3', 'Перчатки CCM', 500, 15, 9, 0, 2, 5);" +
                "insert into dbo.Equipments values ('d14239be-db38-43a7-9e6f-66c44950c3bb', 'Перчатки UhlSports #4', 700, 20, 12, 0, 2, 6);" +
                "insert into dbo.Equipments values ('8623ea8f-cc5b-42d0-b3ac-b29eb4fcb0f5', 'Бутсы BirdSports', 200, 10, 6, 1, 1, 1);" +
                "insert into dbo.Equipments values ('28a5c681-7d3c-47c7-b375-5d3226d7113a', 'Бутсы Sports', 500, 15, 9, 1, 1, 2);" +
                "insert into dbo.Equipments values ('bafb8648-b60b-44bd-8e6c-c164503b4002', 'Бутсы Joledo #1', 750, 20, 12, 1, 1, 3);" +
                "insert into dbo.Equipments values ('50186442-cdc5-4818-86f2-2c62c058071d', 'Бутсы Joledo #2', 350, 10, 6, 1, 2, 4);" +
                "insert into dbo.Equipments values ('b64b73ea-6aec-43a0-a11b-08400c5d0ad8', 'Бутсы PrinstonsSports', 650, 15, 9, 1, 2, 5);" +
                "insert into dbo.Equipments values ('a302a335-b0ea-4f9b-b295-cb42f0feb277', 'Бутсы F-SD', 900, 20, 12, 1, 2, 6);" +
                "insert into dbo.Equipments values ('5e6061be-7356-4671-9a06-4329f0cb85ee', 'Щитки UhlSports #1', 100, 10, 6, 2, 1, 1);" +
                "insert into dbo.Equipments values ('cb9266fa-dc2e-4a4d-af53-4e9191ec11bb', 'Щитки Ukraine', 350, 15, 9, 2, 1, 2);" +
                "insert into dbo.Equipments values ('e466f2ed-ac83-490f-b6f6-6f6dae2ca2bc', 'Щитки Select', 500, 20, 12, 2, 1, 3);" +
                "insert into dbo.Equipments values ('be538919-72a6-4d4e-804d-5ea14421a213', 'Щитки Nike', 150, 10, 6, 2, 2, 4);" +
                "insert into dbo.Equipments values ('1189dda5-40f7-4e04-9943-a32f248d2e2d', 'Щитки 2KFUTURO', 330, 15, 9, 2, 2, 5);" +
                "insert into dbo.Equipments values ('f2d02854-ea1d-4583-93de-e11d95ef1721', 'Щитки ESPADA', 600, 20, 12, 2, 2, 6);");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "Index");
            DropColumn("dbo.Equipments", "WeatherType");
            DropColumn("dbo.Equipments", "Type");
        }
    }
}
