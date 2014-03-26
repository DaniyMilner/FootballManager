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
        public EventManager()
        {
            _countEvents = 0;
            _cellIndex = 0;
            _lineIndex = 0;
            _wasGoal = false;
        }

        private readonly Random _random = new Random();
        private List<Player>[,] _attackTeam;
        private List<Player>[,] _defendTeam;
        private Player _defendGoalkeeper;
        private int _lineIndex;
        private int _cellIndex;
        private Guid _eventLineId;
        private int _currentMinute;
        private CustomTeamSettings _attackTeamSettings;
        private List<EventLine> _customEventLineList;

        private IEventLineRepository _eventLineRepository;

        private bool _wasGoal;

        public string Result { get; set; }

        private const int N = 2;
        private int _countEvents;
        private const int MaxCountEvents = 5;

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
            Guid eventLineId, IEventLineRepository eventLineRepository, int currentMinute, CustomTeamSettings attackTeamSettings,
            List<EventLine> eventLineList)
        {
            var teamInfo = new TeamInformation();
            var defenderTeam = teamInfo.ConvertToGuestLineUp(defend);
            Result = string.Empty;
            _countEvents = 0;
            _attackTeam = attack;
            _defendTeam = defenderTeam;
            _defendGoalkeeper = defendGoalkeeper;
            _eventLineId = eventLineId;
            _eventLineRepository = eventLineRepository;
            _currentMinute = currentMinute;
            _attackTeamSettings = attackTeamSettings;
            _customEventLineList = eventLineList;
            _wasGoal = false;

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

            return _wasGoal ? 1 : 0;
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


        #region StartStopEvents
        public string StartMatchEvent()
        {
            return "Старт матча";
        }

        public string FinishMatchEvent()
        {
            return "Конец матча";
        }

        public string StartSecondHalf()
        {
            return "Начало 2 тайма";
        }

        public string EndFirstTime()
        {
            return "Конец 1 тайма";
        }
        #endregion

        #region Defender Events

        private void DefenderPasOneTop()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                      " [Defender Pas One Top] => ";
            _lineIndex--;
            SetOfMidfilderEvents();
        }
        private void DefenderPasTwoTop()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                      " [Defender Pas Two Top] => ";
            _lineIndex -= 2;
            SetOfForwardEvents();
        }
        private void DefenderPasOneRightTop()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                      " [Defender Pas One Right Top] => ";
            _lineIndex--;
            if (_cellIndex != N)
                _cellIndex++;
            SetOfMidfilderEvents();
        }
        private void DefenderPasOneLeftTop()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                      " [Defender Pas One Left Top] => ";
            _lineIndex--;
            if (_cellIndex != 0)
                _cellIndex--;
            SetOfMidfilderEvents();
        }
        #endregion

        #region Midfilder Events

        private void MidfilderPasOneTop(AttackDirection attackDirection = AttackDirection.Top)
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            var defendPlayer = GetDefendPlayer(_random.Next(0, 2), _cellIndex);
            double defendSelection = 0;
            const double selectionSetting = 1.5;

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var attackPas = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Pas").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            if (defendPlayer!=null){
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
                            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                              " [Midfilder Pas One Top] Success => ";
                            _lineIndex--;
                            break;
                        }
                    case AttackDirection.Right:
                        {
                            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                              " [Midfilder Pas Top Right] Success => ";
                            _lineIndex--;
                            if (_cellIndex != N)
                                _cellIndex++;
                            break;
                        }
                    case AttackDirection.Left:
                        {
                            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                              " [Midfilder Pas Top Left] Success => ";
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

                Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Midfilder Pas One Anyware] Fail (" + defendPlayer.Name + " " + defendPlayer.Surname;

                var chanceFoul = _random.Next(0, 100 + Convert.ToInt32(selectionSetting * 100));
                if (chanceFoul < 100) //не нарушил
                {
                    Result += " good game)";
                }
                else // нарушил
                {
                    Result += " Foul) => ";
                    FieldPlayerFoul(defendPlayer);
                }
            }
        }
        private void MidfilderPasOneBack()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Midfilder Pas One Back] => ";
            if (_lineIndex != N)
                _lineIndex++;
            SetOfDefenderEvents();
        }
        private void MidfilderPasOneRight()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Midfilder Pas One Right] => ";
            if (_cellIndex != N)
                _cellIndex++;
            else if (_lineIndex != N) _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderPasOneLeft()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Midfilder Pas One Left] => ";
            if (_cellIndex != 0)
                _cellIndex--;
            else if (_lineIndex != N) _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderStrikeTwo()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);

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
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.3))
            {
                Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Midfilder Strike Two] => Goal";
                Goal(attackPlayer);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                             " [Midfilder Strike Two] => ";
                    GoalkeeperCorner();
                }
                else
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                             " [Midfilder Strike Two] => ";
                    GoalkeeperСaught();
                }
            }

        }
        #endregion

        #region Forwards Events

        private void ForwardStrikeOne(Player attackPlayer = null)
        {
            if (attackPlayer == null) attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }
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
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100 * 0.7))
            {
                Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                          " [Forward Strike One] => Goal";
                Goal(attackPlayer);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                             " [Forward Strike One] => ";
                    GoalkeeperCorner();
                }
                else
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                             " [Forward Strike One] => ";
                    GoalkeeperСaught();
                }
            }
        }
        private void ForwardPas(AttackDirection attackDirection, Player attackPlayer = null)
        {
            if (attackPlayer == null) attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            var defendPlayer = GetDefendPlayer(_lineIndex, _cellIndex);

            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }

            var attackSkills = attackPlayer.SkillPlayerCollection.ToList();
            var defendSkills = defendPlayer.SkillPlayerCollection.ToList();
            var attackPas = attackSkills.Where(attackSkill => attackSkill.Skill.Name == "Pas").Select(attackSkill => attackSkill.Value).FirstOrDefault();
            var defendSelection = defendSkills.Where(defendSkill => defendSkill.Skill.Name == "Selection").Select(defendSkill => defendSkill.Value).FirstOrDefault();

            var chance = _random.Next(0, Convert.ToInt32(attackPas * 100 + defendSelection * 100 * 0.75));
            if (chance < Convert.ToInt32(attackPas * 100))
            {
                switch (attackDirection)
                {
                    case AttackDirection.Right:
                        {
                            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                                  " [Forward Pas Right] Success => ";
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
                            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                                  " [Forward Pas Left] Success => ";
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
                Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                     " [Forward Pas Anyware] Fail (" + defendPlayer.Name + " " + defendPlayer.Surname + " good game(FW))";
            }
        }
        private void Forward1On1()
        {
            var attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            if (attackPlayer == null)
            {
                if (_cellIndex == N) _cellIndex--;
                else if (_cellIndex == 0) _cellIndex++;
                attackPlayer = GetAttackPlayer(_lineIndex, _cellIndex);
            }
            //переделать используя настройки игрока
            var chance = _random.Next(0, 3);
            Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                           " [Forward 1 on 1] => ";
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
            if (chance < attackDribbling*100)
            {
                var ran = _random.Next(0, 100);
                if (ran < 50)
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " +
                              attackPlayer.Surname +
                              " [Forward Beat] => Goal";
                    Goal(attackPlayer);
                    _wasGoal = true;
                }
                else
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " +
                              attackPlayer.Surname +
                              " [Forward Beat] => Foul => ";
                    GoalkeeperFoul();
                }
            }
            else
            {
                Result += " {" + _lineIndex + "," + _cellIndex + "} " + attackPlayer.Name + " " + attackPlayer.Surname +
                           " [Forward Beat] => " + _defendGoalkeeper.Name + " " + _defendGoalkeeper.Surname + " good";
            }
        }
        #endregion

        #region Goalkeeper Events

        private void GoalkeeperСaught()
        {
            Result += "Сaught " + _defendGoalkeeper.Name + " " + _defendGoalkeeper.Surname;
        }

        private void GoalkeeperCorner()
        {
            Result += "Corner => ";
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
                    Result += _defendGoalkeeper.Name + " " + _defendGoalkeeper.Surname + " [Strike to Midfielder] => ";
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
            if (chance < Convert.ToInt32((attackAccuracy + attackImpactForce) * 100))
            {
                Result += " {" + _lineIndex + "," + _cellIndex + "} " + playerForPenalty.Name + " " + playerForPenalty.Surname +
                          " [Penalty] => Goal";
                Goal(playerForPenalty);
                _wasGoal = true;
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + playerForPenalty.Name + " " + playerForPenalty.Surname +
                             " [Penalty] => ";
                    GoalkeeperCorner();
                }
                else
                {
                    Result += " {" + _lineIndex + "," + _cellIndex + "} " + playerForPenalty.Name + " " + playerForPenalty.Surname +
                             " [Penalty] => ";
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
                    Result += "Yellow card " + player.Name + " " + player.Surname;

                    //проверить на 2 желтую
                    var list =
                        _customEventLineList.Where(z => z.Player.Id == player.Id && z.Type == EventLineType.Yellow)
                            .ToList();
                    if (list.Count == 2)
                    {
                        var redEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Red);
                        _eventLineRepository.Add(redEvent);
                        _customEventLineList.Add(redEvent);
                        Result += "Red card " + player.Name + " " + player.Surname;
                        RemovePlayerFromDefendTeam(player);
                    }
                }
                else
                {
                    //красная
                    var redEvent = new EventLine(_eventLineId, player, _currentMinute, EventLineType.Red);
                    _eventLineRepository.Add(redEvent);
                    _customEventLineList.Add(redEvent);
                    Result += "Red card " + player.Name + " " + player.Surname;
                    RemovePlayerFromDefendTeam(player);
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
