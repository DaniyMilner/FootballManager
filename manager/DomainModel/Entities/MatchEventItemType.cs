using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public enum MatchEventItemType
    {
        StartFirst,//0
        EndFirst,//1
        StartSecond,//2
        EndSecond,//3

        DefenderPasOneTop,//4
        DefenderPasTwoTop,//5
        DefenderPasOneRightTop,//6
        DefenderPasOneLeftTop,//7

        MidfilderPasOneTop,//8
        MidfilderPasTopRight,//9
        MidfilderPasTopLeft,//10
        MidfilderPasOneAnywareFail,//11

        MidfilderPasOneBack,//12
        MidfilderPasOneRight,//13
        MidfilderPasOneLeft,//14
        MidfilderStrikeTwo,//15

        ForwardStrikeOne,//16
        ForwardPasRight,//17
        ForwardPasLeft,//18
        ForwardPasAnywareFail,//19
        ForwardOneOnOne,//20
        ForwardBeat,//21

        GoalkeeperFoul,//22
        GoalkeeperSave,//23
        GoalkeeperCaught,//24
        GoalkeeperStrikeToMidfielder,//25
        GoalkeeperToCorner,//26

        DefenderGoodGame,//27
        DefenderFoul,//28
        Penalty,//29
        Goal,//30
        Yellow,//31
        Red//32
        
    }
}
