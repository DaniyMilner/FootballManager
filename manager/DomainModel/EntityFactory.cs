﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel
{
    public interface IEntityFactory
    {
        User User(string userName, string password, string email, string parrentId, string skype,
            DateTime? birthday, string city, string aboutMySelf, bool sex, string publicId);

        Player Player(string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country,
            string publicId,
            DateTime createDate);

        Skill Skill(string name, int ordering);

        SkillsPlayer SkillsPlayer(Skill skill, Player player);

        EventLine EventLine(Guid lineId, Player player, int minute, EventLineType type);

        Arrangement Arrangement(string scheme, ArrangementType type);

        Weather Weather(string name, WeatherType type);

        Match Match(Guid homeTeamId, Guid guestTeamId, Guid eventLineId, Weather weather, int fansCount,
            int ticketPrice, string publicId, Guid tournamentItemId);

        PlayerSettings PlayerSettings(Player player, Match match, int index);

        TeamSettings TeamSettings(Match match, Arrangement arrangement, Team team);

        Seasons Seasons(string title);

        Tournament Tournament(string title, Country country, int countItems, Seasons season, string publicId);

        TournamentItem TournamentItem(int itemNumber, Tournament tournament, DateTime dateStart);
    }

    public class EntityFactory : IEntityFactory
    {
        public User User(string userName, string password, string email, string parrentId, string skype,
            DateTime? birthday, string city, string aboutMySelf, bool sex, string publicId)
        {
            return new User(userName, password, email, parrentId, skype, birthday, city, aboutMySelf, sex, publicId);
        }

        public Player Player(string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country,
            string publicId, DateTime createDate)
        {
            return new Player(name, surname, age, weight, growth, number, salary, money, humor, condition, user, position, illness, country, publicId, createDate);
        }

        public Skill Skill(string name, int ordering)
        {
            return new Skill(name, ordering);
        }

        public SkillsPlayer SkillsPlayer(Skill skill, Player player)
        {
            return new SkillsPlayer(skill, player);
        }

        public EventLine EventLine(Guid lineId, Player player, int minute, EventLineType type)
        {
            return new EventLine(lineId, player, minute, type);
        }

        public Arrangement Arrangement(string scheme, ArrangementType type)
        {
            return new Arrangement(scheme, type);
        }

        public Weather Weather(string name, WeatherType type)
        {
            return new Weather(name, type);
        }

        public Match Match(Guid homeTeamId, Guid guestTeamId, Guid eventLineId, Weather weather, int fansCount,
            int ticketPrice, string publicId, Guid tournamentItemId) 
        {
            return new Match(homeTeamId, guestTeamId, eventLineId, weather, fansCount, ticketPrice, publicId, tournamentItemId);
        }

        public PlayerSettings PlayerSettings(Player player, Match match, int index)
        {
            return new PlayerSettings(player, match, index);
        }

        public TeamSettings TeamSettings(Match match, Arrangement arrangement, Team team)
        {
            return new TeamSettings(match, arrangement, team);
        }

        public Seasons Seasons(string title)
        {
            return new Seasons(title);
        }

        public Tournament Tournament(string title, Country country, int countItems, Seasons season, string publicId)
        {
            return new Tournament(title, country, countItems, season, publicId);
        }

        public TournamentItem TournamentItem(int itemNumber, Tournament tournament, DateTime dateStart)
        {
            return new TournamentItem(itemNumber, tournament, dateStart);
        }
    }
}
