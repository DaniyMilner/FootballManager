using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Repositories;

namespace DataAccess.Generator
{
    public class EventManager
    {
        private readonly IEventLineRepository _eventLineRepository;
        private readonly Random _random;
        private readonly TeamInformation _teamInfo;
        private readonly Guid _eventLineId;

        private const int MaxCountEvents = 3;
        private const int N = 2;

        private bool _wasGoal;
        private int _countEvents;

        public EventManager() { }
        public EventManager(IEventLineRepository eventLineRepository, Guid eventLineId)
        {
            _eventLineRepository = eventLineRepository;
            _random = new Random();
            _teamInfo = new TeamInformation();
            _eventLineId = eventLineId;

            Reset();
        }

        private void Reset()
        {
            _countEvents = 0;
            _wasGoal = false;
        }

        private List<Player>[,] _attackTeam;
        private List<Player>[,] _defendTeam;
        private Player _defendGoalkeeper;
        private int _lineIndex;
        private int _cellIndex;
        private int _currentMinute;
        private CustomTeamSettings _attackTeamSettings;
        private List<EventLine> _customEventLineList;
        private List<MatchEventItem> _customResultList;

        private Player GetAttackPlayer(int i, int j)
        {
            if (_attackTeam[i, j].Count == 0) return null;
            if (_attackTeam[i, j].Count == 1) return _attackTeam[i, j][0];
            var index = _random.Next(0, _attackTeam[i, j].Count);
            return _attackTeam[i, j][index];
        }
        private Player GetDefendPlayer(int i, int j)
        {
            if (_defendTeam[i, j].Count == 0) return null;
            if (_defendTeam[i, j].Count == 1) return _defendTeam[i, j][0];
            var index = _random.Next(0, _defendTeam[i, j].Count);
            return _defendTeam[i, j][index];
        }

        public int MatchEvent(List<Player>[,] attack, List<Player>[,] defend, Player defendGoalkeeper,
            int currentMinute, CustomTeamSettings attackTeamSettings, List<EventLine> eventLineList, List<MatchEventItem> resultList)
        {
            var defenderTeam = _teamInfo.ConvertToGuestLineUp(defend);

            Reset();
            _attackTeam = attack;
            _defendTeam = defenderTeam;
            _defendGoalkeeper = defendGoalkeeper;

            _currentMinute = currentMinute;
            _attackTeamSettings = attackTeamSettings;
            _customEventLineList = eventLineList;
            _customResultList = resultList;
            //опередить линию начала атаки
            _lineIndex = _random.Next(1, 3);
            //опередить клетку начала атаки
            _cellIndex = _random.Next(0, 3);

            //определяем линию и генерируем стартовое событие из набора
            switch (_lineIndex)
            {
                case 2:
                    SetOfDefenderEvents();
                    break;
                case 1:
                    SetOfMidfilderEvents();
                    break;
            }

            eventLineList = _customEventLineList;

            return Convert.ToInt32(_wasGoal);
        }

        private void SetOfDefenderEvents()
        {
            if (_countEvents != MaxCountEvents)
            {
                _countEvents++;
                int eventNumber = _random.Next(1, 5);
                if (eventNumber == 1)
                {
                    //пас на 1 вперед
                    DefenderPasOneTop();
                }
                else if (eventNumber == 2)
                {
                    //пас на 2 вперед
                    DefenderPasTwoTop();
                }
                else if (eventNumber == 3)
                {
                    //пас на 1 вперед вправо
                    DefenderPasOneRightTop();
                }
                else if (eventNumber == 4)
                {
                    //пас на 1 вперед влево
                    DefenderPasOneLeftTop();
                }
            }
        }
        private void SetOfMidfilderEvents()
        {
            if (_countEvents != MaxCountEvents)
            {
                _countEvents++;
                int eventNumber = _random.Next(1, 8);
                if (eventNumber == 1)
                {
                    //пас на 1 вперед
                    MidfilderPasOneTop();
                }
                else if (eventNumber == 2)
                {
                    //пас на 1 назад
                    MidfilderPasOneBack();
                }
                else if (eventNumber == 3)
                {
                    //пас на 1 вправо
                    MidfilderPasOneRight();
                }
                else if (eventNumber == 4)
                {
                    //пас на 1 влево
                    MidfilderPasOneLeft();
                }
                else if (eventNumber == 5)
                {
                    //пас на 1 вперед вправо
                    MidfilderPasOneTop(AttackDirection.Right);
                }
                else if (eventNumber == 6)
                {
                    //пас на 1 вперед влево
                    MidfilderPasOneTop(AttackDirection.Left);
                }
                else if (eventNumber == 7)
                {
                    //удар на 2
                    MidfilderStrikeTwo();
                }
            }
        }
        private void SetOfForwardEvents()
        {
            if (_countEvents != MaxCountEvents)
            {
                _countEvents++;
                int eventNumber = _random.Next(1, 5);
                if (eventNumber == 1)
                {
                    // удар на 1
                    ForwardStrikeOne();
                }
                else if (eventNumber == 2)
                {
                    //пас на 1 вправо
                    ForwardPas(AttackDirection.Right);
                }
                else if (eventNumber == 3)
                {
                    //пас на 1 влево
                    ForwardPas(AttackDirection.Left);
                }
                else if (eventNumber == 4)
                {
                    //выход 1 на 1
                    Forward1On1();
                }
            }
        }

        private Player CheckPlayer(Player player)
        {
            if (player == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                else
                {
                    switch (_random.Next(0, 2))
                    {
                        case 0:
                            {
                                _cellIndex--;
                                break;
                            }
                        case 1:
                            {
                                _cellIndex++;
                                break;
                            }
                    }
                }
                player = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            }
            return player;
        }

        #region StartStopEvents
        public void StartMatchEvent(List<MatchEventItem> eventItems)
        {
            //Старт матча
            eventItems.Add(new MatchEventItem(MatchEventItemType.StartFirst));
        }

        public void FinishMatchEvent(List<MatchEventItem> eventItems)
        {
            // Конец матча
            eventItems.Add(new MatchEventItem(MatchEventItemType.EndSecond));
        }

        public void StartSecondHalf(List<MatchEventItem> eventItems)
        {
            //Начало 2 тайма
            eventItems.Add(new MatchEventItem(MatchEventItemType.StartSecond));
        }

        public void EndFirstTime(List<MatchEventItem> eventItems)
        {
            //Конец 1 тайма
            eventItems.Add(new MatchEventItem(MatchEventItemType.EndSecond));
        }
        #endregion

        #region Defender Events

        private void DefenderPasOneTop()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.DefenderPasOneTop));
            _lineIndex--;
            SetOfMidfilderEvents();
        }
        private void DefenderPasTwoTop()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.DefenderPasTwoTop));
            _lineIndex -= 2;
            SetOfForwardEvents();
        }
        private void DefenderPasOneRightTop()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.DefenderPasOneRightTop));
            _lineIndex--;
            if (_cellIndex != N)
                _cellIndex++;
            SetOfMidfilderEvents();
        }
        private void DefenderPasOneLeftTop()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.DefenderPasOneLeftTop));
            _lineIndex--;
            if (_cellIndex != 0)
                _cellIndex--;
            SetOfMidfilderEvents();
        }
        #endregion

        #region Midfilder Events

        private void MidfilderPasOneTop(AttackDirection attackDirection = AttackDirection.Top)
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            var defendPlayer = GetDefendPlayer(_random.Next(0, 2), _cellIndex);
            double defendSelection = 0;
            const double selectionSetting = 1.5;

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var attackPas = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Pas").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            if (defendPlayer != null)
            {
                var defendSkills = defendPlayer.SkillPlayerCollection.ToList();
                defendSelection = defendSkills.Where(defendSkill => defendSkill.Skill.Name == "Selection").Select(defendSkill => defendSkill.Value).FirstOrDefault();
            }
            //переделать в соответствии с настройками игрока

            var chance = _random.Next(0, Convert.ToInt32(attackPas * 100 + defendSelection * 100 * 0.5 * selectionSetting));

            if (chance < Convert.ToInt32(attackPas * 100))
            {
                switch (attackDirection)
                {
                    case AttackDirection.Top:
                        {
                            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasOneTop));
                            _lineIndex--;
                            break;
                        }
                    case AttackDirection.Right:
                        {
                            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasTopRight));
                            _lineIndex--;
                            if (_cellIndex != N)
                                _cellIndex++;
                            break;
                        }
                    case AttackDirection.Left:
                        {
                            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasTopLeft));
                            _lineIndex--;
                            if (_cellIndex != 0)
                                _cellIndex--;
                            break;
                        }
                }
                SetOfForwardEvents();
            }
            else
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, defendPlayer.Id, null, MatchEventItemType.MidfilderPasOneAnywareFail));
                            
                var chanceFoul = _random.Next(0, 100 + Convert.ToInt32(selectionSetting * 100));
                if (chanceFoul < 100) //не нарушил
                {
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, defendPlayer.Id, null, MatchEventItemType.DefenderGoodGame));
                }
                else // нарушил
                {
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, defendPlayer.Id, null, MatchEventItemType.DefenderFoul));
                    FieldPlayerFoul(defendPlayer);
                }
            }
        }
        private void MidfilderPasOneBack()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasOneBack));
            if (_lineIndex != N)
                _lineIndex++;
            SetOfDefenderEvents();
        }
        private void MidfilderPasOneRight()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasOneRight));
            if (_cellIndex != N)
                _cellIndex++;
            else if (_lineIndex != N) _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderPasOneLeft()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderPasOneLeft));
            if (_cellIndex != 0)
                _cellIndex--;
            else if (_lineIndex != N) _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderStrikeTwo()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var goalkeeperSkills = _defendGoalkeeper.SkillPlayerCollection.ToList();

            var attackImpactForce = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "ImpactForce").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var attackAccuracy = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Accuracy").Select(attackSkill => attackSkill.Value).FirstOrDefault();

            var goalkeeperReaction = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Reaction").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPlayingInTheAir = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "PlayingInTheAir").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperJump = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Jump").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPositioning = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Positioning").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.3 +
                      (goalkeeperReaction + goalkeeperPlayingInTheAir + goalkeeperJump + goalkeeperPositioning) * 100));

            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.MidfilderStrikeTwo));
            
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.3))
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.Goal));
                Goal(attackPlayer);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    //_customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, _defendGoalkeeper.Id, null, MatchEventItemType.GoalkeeperToCorner));
                    //GoalkeeperCorner();
                    GoalkeeperСaught();
                }
                else
                {
                    GoalkeeperСaught();
                }
            }

        }
        #endregion

        #region Forwards Events

        private void ForwardStrikeOne(Player attackPlayer = null)
        {
            if (attackPlayer == null)
                attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var goalkeeperSkills = _defendGoalkeeper.SkillPlayerCollection.ToList();

            var attackImpactForce = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "ImpactForce").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var attackAccuracy = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Accuracy").Select(attackSkill => attackSkill.Value).FirstOrDefault();

            var goalkeeperReaction = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Reaction").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPlayingInTheAir = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "PlayingInTheAir").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperJump = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Jump").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPositioning = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Positioning").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.7 +
                      (goalkeeperReaction + goalkeeperPlayingInTheAir + goalkeeperJump + goalkeeperPositioning) * 100));

            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.ForwardStrikeOne));
            
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.7))
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.Goal));
                Goal(attackPlayer);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    //_customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, _defendGoalkeeper.Id, null, MatchEventItemType.GoalkeeperToCorner));
                    //GoalkeeperCorner();
                    GoalkeeperСaught();
                }
                else
                {
                    GoalkeeperСaught();
                }
            }
        }
        private void ForwardPas(AttackDirection attackDirection, Player attackPlayer = null)
        {
            if (attackPlayer == null)
                attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));

            double defendSelection = 0;
            var defendPlayer = GetDefendPlayer(_lineIndex, _cellIndex);

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var attackPas = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Pas").Select(attackSkill => attackSkill.Value).FirstOrDefault();

            if (defendPlayer != null)
            {
                var defendSkills = defendPlayer.SkillPlayerCollection.ToList();
                defendSelection = defendSkills.Where(defendSkill => defendSkill.Skill.Name == "Selection").Select(defendSkill => defendSkill.Value).FirstOrDefault();
            }

            var chance = _random.Next(0, Convert.ToInt32(attackPas * 100 + defendSelection * 100 * 0.75));
            if (chance < Convert.ToInt32(attackPas * 100))
            {
                switch (attackDirection)
                {
                    case AttackDirection.Right:
                        {
                            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.ForwardPasRight));
                            if (_cellIndex != N)
                            {
                                _cellIndex++;
                                SetOfForwardEvents();
                            }
                            else
                            {
                                _lineIndex++;
                                SetOfMidfilderEvents();
                            }
                            break;
                        }
                    case AttackDirection.Left:
                        {
                            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.ForwardPasLeft));
                            if (_cellIndex != 0)
                            {
                                _cellIndex--;
                                SetOfForwardEvents();
                            }
                            else
                            {
                                _lineIndex++;
                                SetOfMidfilderEvents();
                            }
                            break;
                        }
                }
            }
            else
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, defendPlayer.Id, null, MatchEventItemType.ForwardPasAnywareFail));
            }
        }
        private void Forward1On1()
        {
            var attackPlayer = CheckPlayer(GetAttackPlayer(_lineIndex, _cellIndex));

            //переделать используя настройки игрока
            var chance = _random.Next(0, 3);
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.ForwardOneOnOne));
            switch (chance)
            {
                case 0:
                    {
                        //бить сразу
                        ForwardStrikeOne(attackPlayer);
                        break;
                    }
                case 1:
                    {
                        //обыграть
                        ForwardBeat(attackPlayer);
                        break;
                    }
                case 2:
                    {
                        //отдать пас
                        var side = _random.Next(0, 2);
                        ForwardPas(side == 0 ? AttackDirection.Left : AttackDirection.Right, attackPlayer);
                        break;
                    }
            }
        }
        private void ForwardBeat(Player attackPlayer)
        {
            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var goalkeeperSkills = _defendGoalkeeper.SkillPlayerCollection.ToList();

            var attackDribbling = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Dribbling").Select(attackSkill => attackSkill.Value).FirstOrDefault();

            var goalkeeperJump = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Jump").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPositioning = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Positioning").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32(attackDribbling * 100 +
                      (goalkeeperJump * 0.5 + goalkeeperPositioning) * 100));
            
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.ForwardBeat));
            
            if (chance < attackDribbling * 100)
            {
                var ran = _random.Next(0, 100);
                if (ran < 50)
                {
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, attackPlayer.Id, null, null, MatchEventItemType.Goal));
                    Goal(attackPlayer);
                    _wasGoal = true;
                }
                else
                {
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, null, _defendGoalkeeper.Id, MatchEventItemType.GoalkeeperFoul));
                    GoalkeeperFoul();
                }
            }
            else
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, null, _defendGoalkeeper.Id, MatchEventItemType.GoalkeeperSave));
            }
        }
        #endregion

        #region Goalkeeper Events

        private void GoalkeeperСaught()
        {
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, null, _defendGoalkeeper.Id, MatchEventItemType.GoalkeeperCaught));
        }

        private void GoalkeeperCorner()
        {
            var cornerPlayer = _attackTeamSettings.PlayerCorner;
            var cornerSkills = cornerPlayer.SkillPlayerCollection.ToList();
            var goalkeeperSkills = _defendGoalkeeper.SkillPlayerCollection.ToList();

            var attackPas = cornerSkills.Where(attackSkill => attackSkill.Skill.Name == "Pas").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var attackAccuracy = cornerSkills.Where(attackSkill => attackSkill.Skill.Name == "Accuracy").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var goalkeeperPlayingInTheAir = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "PlayingInTheAir").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32((attackPas + attackAccuracy) * 100 * 0.3 + goalkeeperPlayingInTheAir * 100));
            if (chance < Convert.ToInt32((attackPas + attackAccuracy) * 100 * 0.3))
            {
                _lineIndex = _random.Next(0, 3);
                _cellIndex = _random.Next(0, 3);
                ForwardStrikeOne();
            }
            else
            {
                var chanceToStrike = _random.Next(0, 3);
                if (chanceToStrike != 2)
                {
                    GoalkeeperСaught();
                }
                else
                {
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, null, _defendGoalkeeper.Id, MatchEventItemType.GoalkeeperStrikeToMidfielder));
                    _lineIndex = _cellIndex = 1;
                    SetOfMidfilderEvents();
                }
            }

        }

        private void GoalkeeperFoul()
        {
            var playerForPenalty = _attackTeamSettings.PlayerPenalty;
            var attackSkills = playerForPenalty.SkillPlayerCollection.ToList();
            var goalkeeperSkills = _defendGoalkeeper.SkillPlayerCollection.ToList();

            var attackImpactForce = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "ImpactForce").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var attackAccuracy = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Accuracy").Select(attackSkill => attackSkill.Value).FirstOrDefault();

            var goalkeeperReaction = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Reaction").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPlayingInTheAir = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "PlayingInTheAir").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperJump = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Jump").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();
            var goalkeeperPositioning = goalkeeperSkills.Where(goalkeeperSkill => goalkeeperSkill.Skill.Name == "Positioning").Select(goalkeeperSkill => goalkeeperSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 +
                      (goalkeeperReaction + goalkeeperPlayingInTheAir + goalkeeperJump + goalkeeperPositioning) * 100));
            
            _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, playerForPenalty.Id, _defendGoalkeeper.Id, null, MatchEventItemType.Penalty));
               
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100))
            {
                _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, playerForPenalty.Id, _defendGoalkeeper.Id, null, MatchEventItemType.Goal));
                Goal(playerForPenalty);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    //_customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, null, _defendGoalkeeper.Id, null, MatchEventItemType.GoalkeeperToCorner));
                    //GoalkeeperCorner();
                    GoalkeeperСaught();
                }
                else
                {
                    GoalkeeperСaught();
                }
            }

            //TODO добавить желтую карточку
        }

        #endregion

        private void FieldPlayerFoul(Player player)
        {
            var chanceCard = _random.Next(0, 225);
            if (chanceCard >= 100)
            {
                if (chanceCard < 200)
                {
                    var customEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Yellow);
                    //желтая
                    _eventLineRepository.Add(customEvent);
                    _customEventLineList.Add(customEvent);
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, player.Id, null, null, MatchEventItemType.Yellow));

                    //проверить на 2 желтую
                    var list =
                        _customEventLineList.Where(z => z.Player.Id == player.Id && z.Type == EventLineType.Yellow)
                            .ToList();
                    if (list.Count == 2)
                    {
                        var redEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Red);
                        _eventLineRepository.Add(redEvent);
                        _customEventLineList.Add(redEvent);
                        _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, player.Id, null, null, MatchEventItemType.Red));
                        RemovePlayerFromDefendTeam(player);
                    }
                }
                else
                {
                    //красная
                    var redEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Red);
                    _eventLineRepository.Add(redEvent);
                    _customEventLineList.Add(redEvent);
                    _customResultList.Add(new MatchEventItem(_lineIndex, _cellIndex, player.Id, null, null, MatchEventItemType.Red));
                    RemovePlayerFromDefendTeam(player);

                    //TODO не больше 2 красных карточек на команду
                }
            }
            else
            {
                //нету картки
            }
        }

        private void Goal(Player player)
        {
            var customEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Goal);
            _eventLineRepository.Add(customEvent);
            _customEventLineList.Add(customEvent);
        }

        private void RemovePlayerFromDefendTeam(Player player)
        {
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    if (_defendTeam[i, j].FirstOrDefault(z => z.Id == player.Id) != null)
                    {
                        _defendTeam[i, j].Remove(player);
                    }
                }
            }
        }
    }
}
