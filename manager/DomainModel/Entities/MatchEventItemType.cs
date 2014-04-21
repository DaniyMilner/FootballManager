using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public enum MatchEventItemType
    {
        StartFirst,
        EndFirst,
        StartSecond,
        EndSecond,

        DefenderPasOneTop,
        DefenderPasTwoTop,
        DefenderPasOneRightTop,
        DefenderPasOneLeftTop,

        MidfilderPasOneTop,
        MidfilderPasTopRight,
        MidfilderPasTopLeft,
        MidfilderPasOneAnywareFail,

        MidfilderPasOneBack,
        MidfilderPasOneRight,
        MidfilderPasOneLeft,
        MidfilderStrikeTwo,

        ForwardStrikeOne,
        ForwardPasRight,
        ForwardPasLeft,
        ForwardPasAnywareFail,
        ForwardOneOnOne,
        ForwardBeat,

        GoalkeeperFoul,
        GoalkeeperSave,
        GoalkeeperCaught,
        GoalkeeperStrikeToMidfielder,
        GoalkeeperToCorner,

        DefenderGoodGame,
        DefenderFoul,
        Penalty,
        Goal,
        Yellow,
        Red
        
    }
}
