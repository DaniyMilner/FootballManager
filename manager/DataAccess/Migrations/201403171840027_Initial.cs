namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arrangements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Scheme = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublicId = c.String(nullable: false, maxLength: 254),
                        Name = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Age = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Growth = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Salary = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        Humor = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        PublicId = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                        Illness_Id = c.Guid(nullable: false),
                        Position_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        TeamId = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Illnesses", t => t.Illness_Id, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Illness_Id)
                .Index(t => t.Position_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Country_Id);

            CreateTable(
                "dbo.Illnesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IllnessName = c.String(nullable: false, maxLength: 254),
                        TimeForRecovery = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublicId = c.String(nullable: false, maxLength: 254),
                        Name = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 254),
                        PasswordHash = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        ParentId = c.String(),
                        Skype = c.String(),
                        Birthday = c.DateTime(),
                        City = c.String(),
                        AboutMySelf = c.String(),
                        Sex = c.Boolean(),
                        Language = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        Logo = c.String(nullable: false),
                        CoachId = c.Guid(),
                        AssistantId = c.Guid(),
                        Stadium = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);

            CreateTable(
                "dbo.EventLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LineId = c.Guid(nullable: false),
                        Minute = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Player_Id);

            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeTeamId = c.Guid(nullable: false),
                        GuestTeamId = c.Guid(nullable: false),
                        EventLineId = c.Guid(nullable: false),
                        FansCount = c.Int(nullable: false),
                        TicketPrice = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        Result = c.String(),
                        Weather_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weathers", t => t.Weather_Id, cascadeDelete: true)
                .Index(t => t.Weather_Id);

            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PlayerSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IndexField = c.Int(nullable: false),
                        Settings = c.String(),
                        isCaptain = c.Boolean(nullable: false),
                        isWritable = c.Boolean(nullable: false),
                        Match_Id = c.Guid(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                        
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Match_Id)
                .Index(t => t.Player_Id);

            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Ordering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SkillsPlayers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Double(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                        Skill_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.Skill_Id);

            CreateTable(
                "dbo.TeamSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Settings = c.String(),
                        LineUp = c.String(),
                        Arrangement_Id = c.Guid(nullable: false),
                        Match_Id = c.Guid(nullable: false),
                        PlayerSend = c.Guid(),
                        Team_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arrangements", t => t.Arrangement_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Arrangement_Id)
                .Index(t => t.Match_Id)
                .Index(t => t.Team_Id);



            Sql("Insert into Positions values ('C7C41812-D6BA-42FE-88B5-D93965D40DD8','GK','Голкипер');" +
                "Insert into Positions values ('51126600-164A-49BD-8C23-BB81A1D9D922','DEF','Защитник');" +
                "Insert into Positions values ('11683223-3908-4739-BE94-EC969C6A35BB','MID','Полузащитник');" +
                "Insert into Positions values ('B114C1DB-9663-4CF1-9290-1739BA6476BF','FW','Форвард');" +
                "Insert into Countries values ('47A4EC4D-D649-4374-B43F-3056BED72DD3','UA','Ukraine');" +
                "Insert into Countries values ('04AB5363-0806-41D0-A7EF-876BE34196B1','RU','Russia');" +
                "Insert into Countries values ('8D302B08-E550-4F3C-9158-85D82A96FE99','BY','Belarus');" +
                "Insert into Countries values ('F7079996-24E5-41A2-9E9B-EBC2DC680DD4','PL','Poland');" +
                "Insert into Countries values ('BF2061D0-4230-49F0-830A-92F2C016BE38','EN','England');" +
                "Insert into Countries values ('99CA397E-5E32-4C90-87BD-35AB2D843BAD','FR','France');" +
                "Insert into Countries values ('23773B84-68DE-4B96-8954-66499E5F521C','DE','Germany');" +
                "Insert into Countries values ('9E734C1D-8F25-4137-9F06-7FEBA439EA00','IT','Italy');" +
                "Insert into Countries values ('FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','ES','Spain');" +
                "Insert into Countries values ('12F3F161-F78D-4182-9C0D-4ED458D2539F','PT','Portugal');" +
                "Insert into Illnesses values ('08E94207-3A21-4CF2-9BF1-A3795442CA68','healthy', 0)");


            Sql("Insert into Skills values ('4A0313D3-7BA0-47B8-B262-09DF4623D4C6','Speed',1);" +
                "Insert into Skills values ('8F23769D-7839-4AEE-880C-0C5D4CE596FD','GameByHead',2);" +
                "Insert into Skills values ('C5CBD0B4-968B-401D-B419-E5352FA1ACE1','ImpactForce',3);" +
                "Insert into Skills values ('93F0C018-BE74-4F31-B697-659DF316F3ED','Pas',4);" +
                "Insert into Skills values ('BDA97D67-7854-49B6-8D68-44DEA6F2038C','Accuracy',5);" +
                "Insert into Skills values ('9093FAC1-FE5D-42BB-B29B-134D03DD89E7','Selection',6);" +
                "Insert into Skills values ('ACB1BBB4-A301-492D-B61B-242455C27C0B','Dribbling',7)" +
                "Insert into Skills values ('15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41','Endurance',13);" +
                "Insert into Skills values ('EFBCEAEC-C9F8-4F77-BAEA-A1256A48758B','Reaction',9);" +
                "Insert into Skills values ('A7722C27-1326-4DE9-991D-FEB068F4F7A1','Possession',8);" +
                "Insert into Skills values ('2E73F75C-22AD-49C2-8B34-2E07D71F502A','PlayingInTheAir',10);" +
                "Insert into Skills values ('BFB6782A-2B34-4C40-B1B5-E7BA4FEF0CA4','Jump',11);" +
                "Insert into Skills values ('53418FFB-C1B6-4B32-A39F-87AA43D46C53','Positioning',12);" +
                "Insert into Skills values ('9C3D8FBD-46B4-410C-B158-2115FE399657','Leadership',14);");

            Sql("Insert into Users values('96e3624e-0146-4887-ab4c-137545d7c3b5','admin@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Admin',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru');" +
                    "Insert into Users values('8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5','adm@manager.com','f5bb0c8de146c67b44babbf4e6584cc0','Barca',null,'ololosh',GETDATE(),'Zhitomir',null,1,'ru');" +
                    "Insert into Players values('6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','Андрей','Шевченко',22,75,185,7,500,300,5,100,'p000001',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('af5262d2-24d2-402b-83d6-986403d85a9f','Андрей','Ярмоленко',25,85,180,8,500,300,5,100,'p000002',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('eb78bba8-1651-49aa-b477-85ac3c608f3a','Сергей','Ребров',28,70,175,10,500,300,5,100,'p000003',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('e349a773-c11d-4547-b7b0-1e86279d3b89','Олег','Гусев',20,75,175,9,500,300,5,100,'p000004',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('a621cbd6-6f07-41d3-a16d-e7514ec0fb18','Анатолий','Тимощук',25,80,175,4,500,300,5,100,'p000005',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('1e33c713-5b15-44bf-9bc9-354ee8dafc88','Сергей','Назаренко',22,80,175,6,500,300,5,100,'p000006',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('6c8a8ebd-e6e5-446b-8f56-78449445b146','Андрей','Гусин',23,80,175,5,500,300,5,100,'p000007',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('c566524c-bab9-48c5-91eb-e40c41ec82ba','Евгений','Коноплянка',19,75,175,11,500,300,5,100,'p000008',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('f30b6ba8-21ea-40ac-9316-f1bb924846b7','Андрей','Русон',25,75,175,3,500,300,5,100,'p000009',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('7d89607b-4693-4202-9a6c-7d0d627eae8d','Евгений','Хачериди',23,85,195,2,500,300,5,100,'p000010',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('19c4a11e-4b06-4a06-8814-f7500bf3932f','Александр','Кучер',22,85,185,12,500,300,5,100,'p000011',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','Ярослав','Ракицкий',22,85,185,13,500,300,5,100,'p000012',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('cd44f089-8ffa-4cd6-a287-92ea0d11816f','Артем','Федецкий',22,85,185,14,500,300,5,100,'p000013',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('50ff51cb-efb2-4ed7-99e4-66d8ec02e579','Александр','Шовковский',30,85,195,1,500,300,5,100,'p000014',GETDATE(),'47A4EC4D-D649-4374-B43F-3056BED72DD3','08E94207-3A21-4CF2-9BF1-A3795442CA68','C7C41812-D6BA-42FE-88B5-D93965D40DD8','96e3624e-0146-4887-ab4c-137545d7c3b5',null);" +
                    "Insert into Players values('A49AEFC3-D087-495F-8CF9-D8CF6C301847','Давид','Вилья',22,75,185,7,500,300,5,100,'p000015',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','Фернандо','Торрес',25,85,180,8,500,300,5,100,'p000016',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('414DBCFC-8B9F-43C2-9A96-27013462895F','Давид','Негредо',28,70,175,10,500,300,5,100,'p000017',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','B114C1DB-9663-4CF1-9290-1739BA6476BF','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('06FE6EB1-7429-4AED-90E8-CBF1FED9281E','Хави','Ернандес',20,75,175,9,500,300,5,100,'p000018',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('45E2CAE3-01B3-476B-BC12-C92EDEA9A704','Сеск','Фабрегас',25,80,175,4,500,300,5,100,'p000019',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('7D12569A-9630-4CF0-84FC-580552034C39','Андреас','Иньеста',22,80,175,6,500,300,5,100,'p000020',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('B6871799-07C2-4322-818B-C5D3BAE1919B','Давид','Силва',23,80,175,5,500,300,5,100,'p000021',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('9C8773C0-DE77-4E75-891C-D5C037070CF2','Серхио','Бускетс',19,75,175,11,500,300,5,100,'p000022',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','11683223-3908-4739-BE94-EC969C6A35BB','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('D5ADF195-C185-4742-9CF7-4C971C069AEF','Серхио','Рамос',25,75,175,3,500,300,5,100,'p000023',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('E5C643D6-9933-4D8F-9297-E729B19A30EB','Альваро','Альбелоа',23,85,195,2,500,300,5,100,'p000024',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','Жеррар','Пике',22,85,185,12,500,300,5,100,'p000025',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('828877F4-D4D6-47B8-97E9-E8422DDF0B7E','Карлес','Пуйоль',22,85,185,13,500,300,5,100,'p000026',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('4E8BCBC7-77B5-428B-926A-7E8F9F483097','Хорди','Альба',22,85,185,14,500,300,5,100,'p000027',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','51126600-164A-49BD-8C23-BB81A1D9D922','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into Players values('C965D541-D3EF-49D3-9434-18C00A9C0C95','Икер','Касильяс',30,85,195,1,500,300,5,100,'p000028',GETDATE(),'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','08E94207-3A21-4CF2-9BF1-A3795442CA68','C7C41812-D6BA-42FE-88B5-D93965D40DD8','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null);" +
                    "Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'6336967b-dbda-46b0-9f1f-0b9c9c1ded3c','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'af5262d2-24d2-402b-83d6-986403d85a9f','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'eb78bba8-1651-49aa-b477-85ac3c608f3a','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'A49AEFC3-D087-495F-8CF9-D8CF6C301847','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'C42B6F3C-4DA1-4237-9DFD-4462D4C7154A','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','ACB1BBB4-A301-492D-B61B-242455C27C0B');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'414DBCFC-8B9F-43C2-9A96-27013462895F','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'e349a773-c11d-4547-b7b0-1e86279d3b89','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'a621cbd6-6f07-41d3-a16d-e7514ec0fb18','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'1e33c713-5b15-44bf-9bc9-354ee8dafc88','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'6c8a8ebd-e6e5-446b-8f56-78449445b146','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'c566524c-bab9-48c5-91eb-e40c41ec82ba','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'06FE6EB1-7429-4AED-90E8-CBF1FED9281E','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'45E2CAE3-01B3-476B-BC12-C92EDEA9A704','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'7D12569A-9630-4CF0-84FC-580552034C39','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'B6871799-07C2-4322-818B-C5D3BAE1919B','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'9C8773C0-DE77-4E75-891C-D5C037070CF2','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'f30b6ba8-21ea-40ac-9316-f1bb924846b7','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'7d89607b-4693-4202-9a6c-7d0d627eae8d','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'19c4a11e-4b06-4a06-8814-f7500bf3932f','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'46802ba5-ff77-4ae8-b0ca-e630b3fb1e88','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'cd44f089-8ffa-4cd6-a287-92ea0d11816f','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'D5ADF195-C185-4742-9CF7-4C971C069AEF','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'E5C643D6-9933-4D8F-9297-E729B19A30EB','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'828877F4-D4D6-47B8-97E9-E8422DDF0B7E','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','4A0313D3-7BA0-47B8-B262-09DF4623D4C6');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','8F23769D-7839-4AEE-880C-0C5D4CE596FD');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','C5CBD0B4-968B-401D-B419-E5352FA1ACE1');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','93F0C018-BE74-4F31-B697-659DF316F3ED');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','BDA97D67-7854-49B6-8D68-44DEA6F2038C');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','9093FAC1-FE5D-42BB-B29B-134D03DD89E7');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','A7722C27-1326-4DE9-991D-FEB068F4F7A1');Insert into SkillsPlayers values (NEWID(),1.0,'4E8BCBC7-77B5-428B-926A-7E8F9F483097','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','EFBCEAEC-C9F8-4F77-BAEA-A1256A48758B');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','2E73F75C-22AD-49C2-8B34-2E07D71F502A');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','BFB6782A-2B34-4C40-B1B5-E7BA4FEF0CA4');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','53418FFB-C1B6-4B32-A39F-87AA43D46C53');Insert into SkillsPlayers values (NEWID(),1.0,'50ff51cb-efb2-4ed7-99e4-66d8ec02e579','9C3D8FBD-46B4-410C-B158-2115FE399657');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','EFBCEAEC-C9F8-4F77-BAEA-A1256A48758B');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','2E73F75C-22AD-49C2-8B34-2E07D71F502A');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','BFB6782A-2B34-4C40-B1B5-E7BA4FEF0CA4');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','53418FFB-C1B6-4B32-A39F-87AA43D46C53');Insert into SkillsPlayers values (NEWID(),1.0,'C965D541-D3EF-49D3-9434-18C00A9C0C95','9C3D8FBD-46B4-410C-B158-2115FE399657');" +
                    "Insert into Weathers values ('C3772375-4D2A-4EA8-AAEC-1B7EFA131FD3','Sun',0);Insert into Weathers values ('31C86FCF-AF04-49BC-9313-A3DD5ACF1DE4','Rain',1);" +
                    "Insert into Arrangements values ('96BF6834-4C80-4F84-BC5A-DDDECD38B384','Scheme442',0);Insert into Arrangements values ('C4B085E8-F04F-4FFA-A273-7F424E93CABE','Scheme433',1);Insert into Arrangements values ('96583811-FF01-4D28-A04C-C0EFC593082C','Scheme451',2);Insert into Arrangements values ('65E3BBCC-6C1F-4F7D-AE02-A139C053A872','Scheme343',3);Insert into Arrangements values ('B6BFFEEA-F12C-463D-B21E-1C45F9E0CE3A','Scheme352',4);Insert into Arrangements values ('87781C26-213B-4D0D-BD65-3F629500498F','Scheme532',5);Insert into Arrangements values ('26DE652D-DD48-47D5-9EFA-752A0181B7D2','Scheme541',6);" +
                    "Insert into Teams values ('EB7F8585-48CD-4051-B6B3-BA445A32FB36','Dynamo Kyiv','DK','','96e3624e-0146-4887-ab4c-137545d7c3b5',null,'Olympiskiy',1927,'47A4EC4D-D649-4374-B43F-3056BED72DD3');Insert into Teams values ('43A41BD3-158F-4546-9A53-111CB0852FD8','Barselona','BAR','','8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5',null,'Camp Nou',1910,'FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B');" +
                    "Insert into Matches values ('FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','EB7F8585-48CD-4051-B6B3-BA445A32FB36','43A41BD3-158F-4546-9A53-111CB0852FD8','E9E8588E-08F7-4D94-820C-19D315A94DEA',0,0,GETDATE(),null,'C3772375-4D2A-4EA8-AAEC-1B7EFA131FD3');" +
                    "Insert into TeamSettings values ('FA47DE54-1FA6-4F10-B47E-F6B9E889D533',null,null,'96BF6834-4C80-4F84-BC5A-DDDECD38B384','FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00',null,'EB7F8585-48CD-4051-B6B3-BA445A32FB36');Insert into TeamSettings values ('4F09AD3C-03BB-471E-8ED9-1E0960D7946D',null,null,'96BF6834-4C80-4F84-BC5A-DDDECD38B384','FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00',null,'43A41BD3-158F-4546-9A53-111CB0852FD8');" +
                    "Insert into PlayerSettings values ('E0463FAD-60CB-4824-92AA-28BC0627002C',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','6336967b-dbda-46b0-9f1f-0b9c9c1ded3c');Insert into PlayerSettings values ('1D3BB714-AC89-40A7-AF91-5DC1884FA2A3',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','af5262d2-24d2-402b-83d6-986403d85a9f');Insert into PlayerSettings values ('0D6EFE97-BEC4-4530-8127-B4C9C378C1E6',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','eb78bba8-1651-49aa-b477-85ac3c608f3a');Insert into PlayerSettings values ('C421ABF9-1274-45E3-A157-20B035A52343',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','e349a773-c11d-4547-b7b0-1e86279d3b89');Insert into PlayerSettings values ('C2365C31-D556-4AFE-9CE8-79991A6ECBAD',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','a621cbd6-6f07-41d3-a16d-e7514ec0fb18');Insert into PlayerSettings values ('FED0E3F7-9053-4F4E-B173-CE07978BF009',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','1e33c713-5b15-44bf-9bc9-354ee8dafc88');Insert into PlayerSettings values ('E700B9FC-96A2-4D98-BBBA-2DB351DCC90E',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','6c8a8ebd-e6e5-446b-8f56-78449445b146');Insert into PlayerSettings values ('9866680A-9C6C-47C3-9814-8E36A2BDC83E',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','c566524c-bab9-48c5-91eb-e40c41ec82ba');Insert into PlayerSettings values ('51F9433F-722E-4275-86DB-53C43748F986',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','f30b6ba8-21ea-40ac-9316-f1bb924846b7');Insert into PlayerSettings values ('B63999B9-E7F8-40A8-9E03-D631A1C73194',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','7d89607b-4693-4202-9a6c-7d0d627eae8d');Insert into PlayerSettings values ('91E76901-4224-48BE-80E8-5715FAC5A640',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','19c4a11e-4b06-4a06-8814-f7500bf3932f');Insert into PlayerSettings values ('BF6FAA5A-B7E8-47CF-AFEE-21CB0D7D804C',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','46802ba5-ff77-4ae8-b0ca-e630b3fb1e88');Insert into PlayerSettings values ('B8649B90-5CFE-49C4-B0D1-7842D6FB2E7A',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','cd44f089-8ffa-4cd6-a287-92ea0d11816f');Insert into PlayerSettings values ('996A6931-4AFA-48F3-B886-8128472C5006',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','50ff51cb-efb2-4ed7-99e4-66d8ec02e579');Insert into PlayerSettings values ('9F0B916B-5B65-46DD-82BE-CFD58F16DB43',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','A49AEFC3-D087-495F-8CF9-D8CF6C301847');Insert into PlayerSettings values ('4BB4DD10-FF3A-404F-A3F6-F204EF34B1DE',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','C42B6F3C-4DA1-4237-9DFD-4462D4C7154A');Insert into PlayerSettings values ('492B8B04-97FB-47FE-9343-15B5F4D969E9',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','414DBCFC-8B9F-43C2-9A96-27013462895F');Insert into PlayerSettings values ('766DA6C5-5356-4143-BB68-6A01CB6A3BBA',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','06FE6EB1-7429-4AED-90E8-CBF1FED9281E');Insert into PlayerSettings values ('7D5CDB62-EB6C-4A9E-87A3-F47244842221',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','45E2CAE3-01B3-476B-BC12-C92EDEA9A704');Insert into PlayerSettings values ('C39A6916-7137-4E3B-AA16-97F53FC43083',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','7D12569A-9630-4CF0-84FC-580552034C39');Insert into PlayerSettings values ('F077551E-29F1-4FD0-B1FC-A9C20635C8BE',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','B6871799-07C2-4322-818B-C5D3BAE1919B');Insert into PlayerSettings values ('4F0FF34B-0721-43D4-9DE3-6A369696D64E',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','9C8773C0-DE77-4E75-891C-D5C037070CF2');Insert into PlayerSettings values ('34191E65-CBFE-4197-873E-E2B2F459071D',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','D5ADF195-C185-4742-9CF7-4C971C069AEF');Insert into PlayerSettings values ('55592127-2C6B-4F7E-8881-9D32740E9F2C',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','E5C643D6-9933-4D8F-9297-E729B19A30EB');Insert into PlayerSettings values ('1CB60B28-901A-4BEB-9F29-AA8F2F07EBB9',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2');Insert into PlayerSettings values ('88309387-E324-4165-B065-D7BC0CD4DD34',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','828877F4-D4D6-47B8-97E9-E8422DDF0B7E');Insert into PlayerSettings values ('7039DFD9-E191-4155-8CC5-20D13D738DD8',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','4E8BCBC7-77B5-428B-926A-7E8F9F483097');Insert into PlayerSettings values ('E1ACE341-E4FA-42AC-9799-0876F3104BA7',0,null,0,0,'FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00','C965D541-D3EF-49D3-9434-18C00A9C0C95');");


            Sql("update PlayerSettings set Settings = '{\"orient\":\"balance\",\"pas\":\"balance\",\"strike\":\"balance\",\"oneone\":\"hit\",\"canopy\":\"balance\",\"selection\":\"middle\",\"dedication\":\"balance\",\"penalty\":\"balance\"}' from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id where Players.Position_Id<>'C7C41812-D6BA-42FE-88B5-D93965D40DD8';" +
                "update PlayerSettings set Settings = '{\"oneone\":\"balance\",\"penalty\":\"balance\",\"dedication\":\"balance\",\"canopy\":\"balance\"}' from PlayerSettings inner join Players on PlayerSettings.Player_Id = Players.Id where Players.Position_Id='C7C41812-D6BA-42FE-88B5-D93965D40DD8'" +
                "update TeamSettings set Settings = '{\"corner\":\"c566524c-bab9-48c5-91eb-e40c41ec82ba\",\"freekick\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\",\"penalty\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\"}' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update TeamSettings set LineUp = '{\"one\":\"50ff51cb-efb2-4ed7-99e4-66d8ec02e579\",\"two\":\"7d89607b-4693-4202-9a6c-7d0d627eae8d\",\"three\":\"19c4a11e-4b06-4a06-8814-f7500bf3932f\",\"four\":\"46802ba5-ff77-4ae8-b0ca-e630b3fb1e88\",\"five\":\"cd44f089-8ffa-4cd6-a287-92ea0d11816f\",\"six\":\"c566524c-bab9-48c5-91eb-e40c41ec82ba\",\"seven\":\"1e33c713-5b15-44bf-9bc9-354ee8dafc88\",\"eight\":\"a621cbd6-6f07-41d3-a16d-e7514ec0fb18\",\"nine\":\"e349a773-c11d-4547-b7b0-1e86279d3b89\",\"ten\":\"6336967b-dbda-46b0-9f1f-0b9c9c1ded3c\",\"eleven\":\"af5262d2-24d2-402b-83d6-986403d85a9f\"}' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update TeamSettings set PlayerSend = '96E3624E-0146-4887-AB4C-137545D7C3B5' where TeamSettings.Team_Id='EB7F8585-48CD-4051-B6B3-BA445A32FB36' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set isCaptain = 1 where PlayerSettings.Player_Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 1 where PlayerSettings.Player_Id='50ff51cb-efb2-4ed7-99e4-66d8ec02e579' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 2 where PlayerSettings.Player_Id='7d89607b-4693-4202-9a6c-7d0d627eae8d' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 3 where PlayerSettings.Player_Id='19c4a11e-4b06-4a06-8814-f7500bf3932f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 4 where PlayerSettings.Player_Id='46802ba5-ff77-4ae8-b0ca-e630b3fb1e88' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 5 where PlayerSettings.Player_Id='cd44f089-8ffa-4cd6-a287-92ea0d11816f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 6 where PlayerSettings.Player_Id='c566524c-bab9-48c5-91eb-e40c41ec82ba' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 7 where PlayerSettings.Player_Id='1e33c713-5b15-44bf-9bc9-354ee8dafc88' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 8 where PlayerSettings.Player_Id='a621cbd6-6f07-41d3-a16d-e7514ec0fb18' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 9 where PlayerSettings.Player_Id='e349a773-c11d-4547-b7b0-1e86279d3b89' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 10 where PlayerSettings.Player_Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 11 where PlayerSettings.Player_Id='af5262d2-24d2-402b-83d6-986403d85a9f' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update TeamSettings set Settings = '{\"corner\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"freekick\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"penalty\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\"}' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update TeamSettings set LineUp = '{\"one\":\"C965D541-D3EF-49D3-9434-18C00A9C0C95\",\"two\":\"4E8BCBC7-77B5-428B-926A-7E8F9F483097\",\"three\":\"828877F4-D4D6-47B8-97E9-E8422DDF0B7E\",\"four\":\"5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2\",\"five\":\"D5ADF195-C185-4742-9CF7-4C971C069AEF\",\"six\":\"9C8773C0-DE77-4E75-891C-D5C037070CF2\",\"seven\":\"B6871799-07C2-4322-818B-C5D3BAE1919B\",\"eight\":\"7D12569A-9630-4CF0-84FC-580552034C39\",\"nine\":\"06FE6EB1-7429-4AED-90E8-CBF1FED9281E\",\"ten\":\"A49AEFC3-D087-495F-8CF9-D8CF6C301847\",\"eleven\":\"C42B6F3C-4DA1-4237-9DFD-4462D4C7154A\"}' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update TeamSettings set PlayerSend = '8CFDF9BD-9ADD-4C49-8BEF-2C8E14C4C9B5' where TeamSettings.Team_Id='43A41BD3-158F-4546-9A53-111CB0852FD8' and TeamSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set isCaptain = 1 where PlayerSettings.Player_Id='C965D541-D3EF-49D3-9434-18C00A9C0C95' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 1 where PlayerSettings.Player_Id='C965D541-D3EF-49D3-9434-18C00A9C0C95' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 2 where PlayerSettings.Player_Id='4E8BCBC7-77B5-428B-926A-7E8F9F483097' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 3 where PlayerSettings.Player_Id='828877F4-D4D6-47B8-97E9-E8422DDF0B7E' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 4 where PlayerSettings.Player_Id='5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 5 where PlayerSettings.Player_Id='D5ADF195-C185-4742-9CF7-4C971C069AEF' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 6 where PlayerSettings.Player_Id='9C8773C0-DE77-4E75-891C-D5C037070CF2' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 7 where PlayerSettings.Player_Id='B6871799-07C2-4322-818B-C5D3BAE1919B' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 8 where PlayerSettings.Player_Id='7D12569A-9630-4CF0-84FC-580552034C39' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 9 where PlayerSettings.Player_Id='06FE6EB1-7429-4AED-90E8-CBF1FED9281E' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 10 where PlayerSettings.Player_Id='A49AEFC3-D087-495F-8CF9-D8CF6C301847' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update PlayerSettings set IndexField = 11 where PlayerSettings.Player_Id='C42B6F3C-4DA1-4237-9DFD-4462D4C7154A' and PlayerSettings.Match_Id='FB8A3E3B-7EC1-4EA6-A20F-D31CB65D9E00';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='6336967b-dbda-46b0-9f1f-0b9c9c1ded3c';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='af5262d2-24d2-402b-83d6-986403d85a9f';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='eb78bba8-1651-49aa-b477-85ac3c608f3a';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='e349a773-c11d-4547-b7b0-1e86279d3b89';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='a621cbd6-6f07-41d3-a16d-e7514ec0fb18';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='1e33c713-5b15-44bf-9bc9-354ee8dafc88';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='6c8a8ebd-e6e5-446b-8f56-78449445b146';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='c566524c-bab9-48c5-91eb-e40c41ec82ba';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='f30b6ba8-21ea-40ac-9316-f1bb924846b7';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='7d89607b-4693-4202-9a6c-7d0d627eae8d';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='19c4a11e-4b06-4a06-8814-f7500bf3932f';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='46802ba5-ff77-4ae8-b0ca-e630b3fb1e88';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='cd44f089-8ffa-4cd6-a287-92ea0d11816f';" +
                "update Players Set TeamId = 'EB7F8585-48CD-4051-B6B3-BA445A32FB36' where Players.Id='50ff51cb-efb2-4ed7-99e4-66d8ec02e579';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='A49AEFC3-D087-495F-8CF9-D8CF6C301847';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='C42B6F3C-4DA1-4237-9DFD-4462D4C7154A';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='414DBCFC-8B9F-43C2-9A96-27013462895F';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='06FE6EB1-7429-4AED-90E8-CBF1FED9281E';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='45E2CAE3-01B3-476B-BC12-C92EDEA9A704';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='7D12569A-9630-4CF0-84FC-580552034C39';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='B6871799-07C2-4322-818B-C5D3BAE1919B';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='9C8773C0-DE77-4E75-891C-D5C037070CF2';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='D5ADF195-C185-4742-9CF7-4C971C069AEF';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='E5C643D6-9933-4D8F-9297-E729B19A30EB';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='5E8FF921-E82A-4FC6-9207-69FF2A3FC9D2';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='828877F4-D4D6-47B8-97E9-E8422DDF0B7E';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='4E8BCBC7-77B5-428B-926A-7E8F9F483097';" +
                "update Players Set TeamId = '43A41BD3-158F-4546-9A53-111CB0852FD8' where Players.Id='C965D541-D3EF-49D3-9434-18C00A9C0C95';");



           

        }

        public override void Down()
        {
            DropForeignKey("dbo.TeamSettings", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamSettings", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.TeamSettings", "Arrangement_Id", "dbo.Arrangements");
            DropForeignKey("dbo.SkillsPlayers", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillsPlayers", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerSettings", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerSettings", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.Matches", "Weather_Id", "dbo.Weathers");
            DropForeignKey("dbo.EventLines", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Teams", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Players", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Players", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Players", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.Players", "Illness_Id", "dbo.Illnesses");
            DropIndex("dbo.TeamSettings", new[] { "Team_Id" });
            DropIndex("dbo.TeamSettings", new[] { "Match_Id" });
            DropIndex("dbo.TeamSettings", new[] { "Arrangement_Id" });
            DropIndex("dbo.SkillsPlayers", new[] { "Skill_Id" });
            DropIndex("dbo.SkillsPlayers", new[] { "Player_Id" });
            DropIndex("dbo.PlayerSettings", new[] { "Player_Id" });
            DropIndex("dbo.PlayerSettings", new[] { "Match_Id" });
            DropIndex("dbo.Matches", new[] { "Weather_Id" });
            DropIndex("dbo.EventLines", new[] { "Player_Id" });
            DropIndex("dbo.Teams", new[] { "Country_Id" });
            DropIndex("dbo.Players", new[] { "Country_Id" });
            DropIndex("dbo.Players", new[] { "User_Id" });
            DropIndex("dbo.Players", new[] { "Position_Id" });
            DropIndex("dbo.Players", new[] { "Illness_Id" });
            DropTable("dbo.TeamSettings");
            DropTable("dbo.SkillsPlayers");
            DropTable("dbo.Skills");
            DropTable("dbo.PlayerSettings");
            DropTable("dbo.Weathers");
            DropTable("dbo.Matches");
            DropTable("dbo.EventLines");
            DropTable("dbo.Teams");
            DropTable("dbo.Users");
            DropTable("dbo.Positions");
            DropTable("dbo.Illnesses");
            DropTable("dbo.Players");
            DropTable("dbo.Countries");
            DropTable("dbo.Arrangements");
        }
    }
}
