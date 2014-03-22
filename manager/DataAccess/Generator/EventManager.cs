using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generator
{
    public class EventManager
    {
        private readonly Random _random = new Random();
        private List<Player>[,] _attackTeam = null;
        private List<Player>[,] _defendTeam = null;
        private int _lineIndex = 0;
        private int _cellIndex = 0;

        public string Result { get; set; }

        private const int n = 2;
        private int _countEvents = 0;
        private const int MaxCountEvents = 5;

        public void MatchEvent(List<Player>[,] attack, List<Player>[,] defend)
        {
            var teamInfo = new TeamInformation();
            var defenderTeam = teamInfo.ConvertToGuestLineUp(defend);
            Result = string.Empty;
            _countEvents = 0;
            _attackTeam = attack;
            _defendTeam = defenderTeam;

            //опередить линию начала атаки
            _lineIndex = _random.Next(1, 3);
            //опередить клетку начала атаки
            _cellIndex = _random.Next(0, 3);


            //определяем линию и генерируем стартовое событие из набора
            if (_lineIndex == 2)
            {
                SetOfDefenderEvents();
            }
            else if (_lineIndex == 1)
            {
                SetOfMidfilderEvents();
            }
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
                    MidfilderPasTopRight();
                }
                else if (eventNumber == 6)
                {
                    //пас на 1 вперед влево
                    MidfilderPasTopLeft();
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
                    ForwardPasRight();
                }
                else if (eventNumber == 3)
                {
                    //пас на 1 влево
                    ForwardPasLeft();
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
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Defender Pas One Top]";
            _lineIndex--;
            SetOfMidfilderEvents();
        }

        private void DefenderPasTwoTop()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Defender Pas Two Top]";
            _lineIndex -= 2;
            SetOfForwardEvents();
        }

        private void DefenderPasOneRightTop()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Defender Pas One Right Top]";
            _lineIndex--;
            if (_cellIndex != n)
                _cellIndex++;
            SetOfMidfilderEvents();
        }

        private void DefenderPasOneLeftTop()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Defender Pas One Left Top]";
            _lineIndex--;
            if (_cellIndex != 0)
                _cellIndex--;
            SetOfMidfilderEvents();
        }
        #endregion

        #region Midfilder Events
        private void MidfilderPasOneTop()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas One Top]";
            _lineIndex--;
            SetOfForwardEvents();
        }
        private void MidfilderPasOneBack()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas One Back]";
            _lineIndex++;
            SetOfDefenderEvents();
        }
        private void MidfilderPasOneRight()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas One Right]";
            if (_cellIndex != n)
                _cellIndex++;
            else _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderPasOneLeft()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas One Left]";
            if (_cellIndex != 0)
                _cellIndex--;
            else _lineIndex++;
            SetOfMidfilderEvents();
        }
        private void MidfilderPasTopRight()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas Top Right]";
            _lineIndex--;
            if (_cellIndex != n)
                _cellIndex++;
            SetOfForwardEvents();
        }
        private void MidfilderPasTopLeft()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Pas Top Left]";
            _lineIndex--;
            if (_cellIndex != 0)
                _cellIndex--;
            SetOfForwardEvents();
        }
        private void MidfilderStrikeTwo()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Midfilder Strike Two]";
            _countEvents = MaxCountEvents;
            SetOfForwardEvents();
        }
        #endregion

        #region Forwards Events

        private void ForwardStrikeOne()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Forward Strike One]";
            _countEvents = MaxCountEvents;
            SetOfForwardEvents();
        }

        private void ForwardPasRight()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Forward Pas Right]";
            if (_cellIndex != n)
            {
                _cellIndex++;
                SetOfForwardEvents();
            }
            else
            {
                _lineIndex++;
                SetOfMidfilderEvents();
            }
        }

        private void ForwardPasLeft()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Forward Pas Left]";
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
        }

        private void Forward1On1()
        {
            Result += " {" + _lineIndex.ToString() + "," + _cellIndex.ToString() + "} => " + "[Forward 1 On 1]";
            _countEvents = MaxCountEvents;
            SetOfForwardEvents();
        }
        #endregion
    }
}
