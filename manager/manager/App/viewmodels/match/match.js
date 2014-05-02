define(['plugins/router', 'httpWrapper', 'matchEvents'],
    function (router, httpWrapper, matchEvents) {

        var viewmodel = {
            self: this,

            activePageIndex: ko.observable(0),

            homeGoal: ko.observable(0), //+
            guestGoal: ko.observable(0), //+

            matchId: '', //+
            matchDateStart: ko.observable(''), //+
            matchResult: ko.observableArray([]),//+
            customMatchResult: ko.observableArray([]),//+
            customMatchResultOnline: ko.observableArray([]),//+

            homePlayers: ko.observable(), //+
            guestPlayers: ko.observable(), //+

            homeTeamName: ko.observable(''), //+
            homeTeamShortName: ko.observable(''), //+
            homeTeamLogo: ko.observable(''),
            homeStadium: ko.observable(''), //+

            guestTeamName: ko.observable(''), //+
            guestTeamShortName: ko.observable(''), //+
            guestTeamLogo: ko.observable(''),

            eventsLine: ko.observable(), //+
            customEventsLine: ko.observableArray([]),//+
            customEventsLineOnline: ko.observableArray([]),//+
            customEventsLineIndex:ko.observable(0),//+

            homeGoalEventsLine: ko.observableArray([]),//+
            homeGoalEventsLineOnline: ko.observableArray([]),//+
            guestGoalEventsLine: ko.observableArray([]),//+
            guestGoalEventsLineOnline: ko.observableArray([]),//+
            homeYellowEventsLine: ko.observableArray([]),//+
            homeYellowEventsLineOnline: ko.observableArray([]),//+
            guestYellowEventsLine: ko.observableArray([]),//+
            guestYellowEventsLineOnline: ko.observableArray([]),//+
            homeRedEventsLine: ko.observableArray([]),//+
            homeRedEventsLineOnline: ko.observableArray([]),//+
            guestRedEventsLine: ko.observableArray([]),//+
            guestRedEventsLineOnline: ko.observableArray([]),//+

            activate: activate,
            clear: function () {
                //wtf
                viewmodel.customMatchResultOnline([]);
                viewmodel.customEventsLine([]);
                viewmodel.customEventsLineOnline([]);
                viewmodel.homeGoalEventsLine([]);
                viewmodel.homeGoalEventsLineOnline([]);
                viewmodel.guestGoalEventsLine([]);
                viewmodel.guestGoalEventsLineOnline([]);
                viewmodel.homeYellowEventsLine([]);
                viewmodel.homeYellowEventsLineOnline([]);
                viewmodel.guestYellowEventsLine([]);
                viewmodel.guestYellowEventsLineOnline([]);
                viewmodel.homeRedEventsLine([]);
                viewmodel.homeRedEventsLineOnline([]);
                viewmodel.guestRedEventsLine([]);
                viewmodel.guestRedEventsLineOnline([]);
                viewmodel.homeGoal(0);
                viewmodel.guestGoal(0);
            },
            manageEventLine: function () {
                for (var i = 0; i < viewmodel.eventsLine().length; i++) {
                    if (isHomeTeamByPlayerId(viewmodel.eventsLine()[i].playerId)) {
                        var homeplayer = getHomePlayerById(viewmodel.eventsLine()[i].playerId);
                        if (homeplayer) {
                            var itemToAddHome = {
                                minute: viewmodel.eventsLine()[i].minute,
                                type: viewmodel.eventsLine()[i].type,
                                playerName: homeplayer.name,
                                playerSurname: homeplayer.surname,
                                isHome: true,
                                coord: getCoordinatesForEventsLine(viewmodel.eventsLine()[i].minute)
                            };
                            viewmodel.customEventsLine().push(itemToAddHome);
                            switch (viewmodel.eventsLine()[i].type) {
                                case 0:
                                    //viewmodel.homeGoal(viewmodel.homeGoal() + 1);
                                    viewmodel.homeGoalEventsLine().push(itemToAddHome);
                                    break;
                                case 1:
                                    viewmodel.homeYellowEventsLine().push(itemToAddHome);
                                    break;
                                case 2:
                                    viewmodel.homeRedEventsLine().push(itemToAddHome);
                                    break;
                            }
                        }
                    } else {
                        var guestplayer = getGuestPlayerById(viewmodel.eventsLine()[i].playerId);
                        if (guestplayer) {
                            var itemToAddGuest = {
                                minute: viewmodel.eventsLine()[i].minute,
                                type: viewmodel.eventsLine()[i].type,
                                playerName: guestplayer.name,
                                playerSurname: guestplayer.surname,
                                isHome: false,
                                coord: getCoordinatesForEventsLine(viewmodel.eventsLine()[i].minute)
                            };
                            viewmodel.customEventsLine().push(itemToAddGuest);
                            switch (viewmodel.eventsLine()[i].type) {
                                case 0:
                                    //viewmodel.guestGoal(viewmodel.guestGoal() + 1);
                                    viewmodel.guestGoalEventsLine().push(itemToAddGuest);
                                    break;
                                case 1:
                                    viewmodel.guestYellowEventsLine().push(itemToAddGuest);
                                    break;
                                case 2:
                                    viewmodel.guestRedEventsLine().push(itemToAddGuest);
                                    break;
                            }
                        }
                    }
                }
            },
            setPageIndex0: function () {
                viewmodel.activePageIndex(0);
            },
            setPageIndex1: function () {
                viewmodel.activePageIndex(1);
            },
            setPageIndex2: function () {
                viewmodel.activePageIndex(2);
            },
            setPageIndex3: function () {
                viewmodel.activePageIndex(3);
            },
            manageMatchResult: function () {
                var lineIndexes = getEventsIndexesLine();
                for (var i = 0; i < lineIndexes.length; i++) {
                    var text = substitution(getTextFromLineIndex(lineIndexes[i].lineType), lineIndexes[i].linePersons);
                    if (text != '') {
                        viewmodel.customMatchResult().push({
                            minute: lineIndexes[i].minute,
                            text: text,
                            isGoal: lineIndexes[i].isGoal,
                            isYellow: lineIndexes[i].isYellow,
                            isRed: lineIndexes[i].isRed
                        });
                    }
                }
            },
            addEventsToOnline: function () {
                if (viewmodel.customEventsLineIndex() != 0 && viewmodel.customEventsLineIndex() == viewmodel.customMatchResult().length) {
                    return false;
                }
                var customEvent = viewmodel.customMatchResult()[viewmodel.customEventsLineIndex()];
                viewmodel.customMatchResultOnline.push(customEvent);

                if (customEvent.isGoal) {
                    var tempGoalLine = getCustomEventByMinute(customEvent.minute);
                    if (tempGoalLine) {
                        viewmodel.customEventsLineOnline.push(tempGoalLine);
                        manageCustomGoalEventByMinute(customEvent.minute);
                    }
                }
                else if (customEvent.isYellow) {
                    var tempYellowLine = getCustomEventByMinute(customEvent.minute);
                    if (tempYellowLine) {
                        viewmodel.customEventsLineOnline.push(tempYellowLine);
                        manageCustomYellowEventByMinute(customEvent.minute);
                    }
                }
                else if (customEvent.isRed) {
                    var tempRedLine = getCustomEventByMinute(customEvent.minute);
                    if (tempRedLine) {
                        viewmodel.customEventsLineOnline.push(tempRedLine);
                        manageCustomRedEventByMinute(customEvent.minute);
                    }
                }
                viewmodel.customEventsLineIndex(viewmodel.customEventsLineIndex() + 1);
                return true;
            },
            online: function () {
                var interval = setInterval(function () {
                    var status = viewmodel.addEventsToOnline();
                    //document.querySelector('.translation-wrapper .table tr:last-child').scrollIntoViewIfNeeded();
                    if (!status)
                        clearInterval(interval);
                }, 500);
            }
        };

        return viewmodel;

        function activate(id) {
            viewmodel.matchId = id;
            return httpWrapper.post('api/match/getmatchresult', { id: viewmodel.matchId }).then(function (response) {
                if (response.success) {
                    console.log(response);

                    viewmodel.matchDateStart(getDateFromString(response.data.matchInfo.dateStart));
                    viewmodel.homeTeamName(response.data.homeTeamInfo.name);
                    viewmodel.homeTeamShortName(response.data.homeTeamInfo.shortName);
                    viewmodel.homeStadium(response.data.homeTeamInfo.stadium);
                    viewmodel.guestTeamName(response.data.guestTeamInfo.name);
                    viewmodel.guestTeamShortName(response.data.guestTeamInfo.shortName);
                    if (response.data.wasMatch) {
                        viewmodel.homePlayers(response.data.homePlayers);
                        viewmodel.guestPlayers(response.data.guestPlayers);
                        viewmodel.eventsLine(response.data.eventsLine);
                        viewmodel.matchResult(response.data.matchResult);

                        viewmodel.clear();

                        viewmodel.manageEventLine();
                        viewmodel.manageMatchResult();
                        viewmodel.online();
                    }
                } else {
                    router.navigate('home');
                }
            }).fail(function (response) {
                console.log(response);
            });
        }

        function getDateFromString(datestring) {
            return new Date(parseInt(datestring.replace('/Date(', '').replace(')/', ''))).toLocaleString();
        }

        function isHomeTeamByPlayerId(playerId) {
            for (var i = 0; i < viewmodel.homePlayers().length; i++) {
                if (viewmodel.homePlayers()[i].id == playerId) {
                    return true;
                }
            }
            return false;
        }

        function getHomePlayerById(playerId) {
            for (var i = 0; i < viewmodel.homePlayers().length; i++) {
                if (viewmodel.homePlayers()[i].id == playerId) {
                    return viewmodel.homePlayers()[i];
                }
            }
            return null;
        }
        function getGuestPlayerById(playerId) {
            for (var i = 0; i < viewmodel.guestPlayers().length; i++) {
                if (viewmodel.guestPlayers()[i].id == playerId) {
                    return viewmodel.guestPlayers()[i];
                }
            }
            return null;
        }

        function getCoordinatesForEventsLine(minute) {
            if (minute >= 0 && minute <= 20)
                return minute + 1;
            else if (minute >= 21 && minute <= 40)
                return minute + 2;
            else if (minute >= 41 && minute <= 60)
                return minute + 3;
            else if (minute >= 61 && minute <= 80)
                return minute + 4;
            return minute + 5;
        }

        function getEventsIndexesLine() {
            var array = [];
            viewmodel.matchResult().forEach(function (item) {
                var mas = {
                    minute: '',
                    lineType: [],
                    isHome: '',
                    linePersons: [],
                    isGoal: false,
                    isYellow: false,
                    isRed: false
                };
                mas.minute = item.Minute;
                mas.isHome = item.IsHome;
                item.EventsLine.forEach(function (element) {
                    mas.lineType.push(element.EventType);
                    mas.linePersons.push({
                        attack: element.AttackPlayer,
                        defend: element.DefendPlayer,
                        goalkeeper: element.Goalkeeper
                    });
                });
                switch (item.EventsLine[item.EventsLine.length - 1].EventType) {
                    case 30:
                        mas.isGoal = true;
                        break;
                    case 31:
                        mas.isYellow = true;
                        break;
                    case 32:
                        mas.isRed = true;
                        break;
                }
                array.push(mas);
            });
            return array;
        }

        function getTextFromLineIndex(lineindex) {
            if (lineindex[0] >= 0 && lineindex[0] <= 3) {
                return matchEvents.getStartStopEvent(lineindex[0]);
            }

            if (lineindex.length == 3) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && (lineindex[2] >= 4 && lineindex[2] <= 14))
                    return matchEvents.getMidfieldPasses(lineindex);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18 && lineindex[1] != 15) && (lineindex[2] >= 4 && lineindex[2] <= 18 && lineindex[1] != 15))
                    return matchEvents.getForwardPasAnyware(lineindex, 1);
            }

            if (lineindex.length >= 2 && lineindex.length <= 4) {
                if (lineindex[0] == 11 && lineindex[1] == 27)
                    return matchEvents.getTryToDribbling(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 11 && lineindex[2] == 27)
                    return matchEvents.getTryToDribbling(lineindex, 2);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 11 && lineindex[3] == 27)
                    return matchEvents.getTryToDribbling(lineindex, 3);
            }

            if (lineindex.length >= 2 && lineindex.length <= 4) {
                if (lineindex[0] == 15 && lineindex[1] == 24)
                    return matchEvents.getStrikeTwoCautch(lineindex);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 15 && lineindex[2] == 24)
                    return matchEvents.getPasThenStrikeTwoCautch(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 15 && lineindex[3] == 24)
                    return matchEvents.getPasThenStrikeTwoCautch(lineindex, 2);
            }

            if (lineindex.length == 3 || lineindex.length == 4) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 16 && lineindex[2] == 24)
                    return matchEvents.getPasThenStrikeOneCautch(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 16 && lineindex[3] == 24)
                    return matchEvents.getPasThenStrikeOneCautch(lineindex, 2);
            }

            if (lineindex.length >= 3 && lineindex.length <= 5) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 16 && lineindex[2] == 30)
                    return matchEvents.getStrikeOneGoal(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 16 && lineindex[3] == 30)
                    return matchEvents.getStrikeOneGoal(lineindex, 2);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 20 && lineindex[2] == 16 && lineindex[3] == 30)
                    return matchEvents.getStrikeOneGoal(lineindex, 3);
            }

            if (lineindex.length == 2 || lineindex.length == 3) {
                if (lineindex[0] == 11 && lineindex[1] == 28)
                    return matchEvents.getTryPasAndFoul(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 11 && lineindex[2] == 28)
                    return matchEvents.getTryPasAndFoul(lineindex, 2);
            }

            if (lineindex.length >= 2 || lineindex.length <= 4) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 21) && lineindex[1] == 19)
                    return matchEvents.getForwardPasFail(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 21) && (lineindex[1] >= 4 && lineindex[1] <= 21) && lineindex[2] == 19)
                    return matchEvents.getForwardPasFail(lineindex, 2);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 20 && lineindex[3] == 19)
                    return matchEvents.getForwardPasFail(lineindex, 3);
            }

            if (lineindex.length >= 2 && lineindex.length <= 4) {
                if (lineindex[0] == 15 && lineindex[1] == 30)
                    return matchEvents.getStrikeTwoGoal(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 15 && lineindex[2] == 30)
                    return matchEvents.getStrikeTwoGoal(lineindex, 2);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 15 && lineindex[3] == 30)
                    return matchEvents.getStrikeTwoGoal(lineindex, 3);
            }

            if (lineindex.length == 4) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 20 && lineindex[3] == 17)
                    return matchEvents.getForwardPasAnyware(lineindex, 2);
            }

            if (lineindex.length >= 3 && lineindex.length <= 5) {
                if (lineindex[0] == 11 && lineindex[1] == 28 && lineindex[2] == 31)
                    return matchEvents.yellowForDefender(lineindex, 1);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 11 && lineindex[2] == 28 && lineindex[3] == 31)
                    return matchEvents.yellowForDefender(lineindex, 2);
                else if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 11 && lineindex[3] == 28 && lineindex[4] == 31)
                    return matchEvents.yellowForDefender(lineindex, 3);
            }

            if (lineindex.length == 5) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 14) && lineindex[2] == 20 && lineindex[3] == 16 && lineindex[4] == 24)
                    return matchEvents.OneOnOneStrikeCautch(lineindex);
            }

            if (lineindex.length == 5) {
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 20 && lineindex[3] == 21 && lineindex[4] == 23)
                    return matchEvents.ForwardBeatSave(lineindex);
            }

            if (lineindex.length >= 3 && lineindex.length <= 5) {
                if (lineindex[0] == 15 && lineindex[1] == 26 && lineindex[2] == 24)
                    return matchEvents.OneOnOneThenCornerCauth(lineindex, 3);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 15 && lineindex[2] == 26 && lineindex[3] == 24)
                    return matchEvents.OneOnOneThenCornerCauth(lineindex, 1);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 15 && lineindex[3] == 26 && lineindex[4] == 24)
                    return matchEvents.OneOnOneThenCornerCauth(lineindex, 2);
            }

            if (lineindex.length >= 3 || lineindex.length <= 5) {
                //[*,20,18]
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 20 && lineindex[2] == 18)
                    return matchEvents.getForwardPasFail(lineindex, 2);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 20 && lineindex[3] == 18)
                    return matchEvents.getForwardPasFail(lineindex, 3);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 20) && (lineindex[2] >= 4 && lineindex[2] <= 20) && lineindex[3] == 20 && lineindex[4] == 18)
                    return matchEvents.getForwardPasFail(lineindex, 4);
            }

            if (lineindex.length == 4 || lineindex.length == 5) {
                //[*, 20, 21, 30]
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 20 && lineindex[2] == 21 && lineindex[3] == 30)
                    return matchEvents.OneOneGoal(lineindex, 1);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 20 && lineindex[3] == 21 && lineindex[4] == 30)
                    return matchEvents.OneOneGoal(lineindex, 2);
            }

            if (lineindex.length >= 3 && lineindex.length <= 5) {
                //[*, 11,28,32]
                if (lineindex[0] == 11 && lineindex[1] == 28 && lineindex[2] == 32)
                    return matchEvents.redForDefender(lineindex, 1);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && lineindex[1] == 11 && lineindex[2] == 28 && lineindex[3] == 32)
                    return matchEvents.redForDefender(lineindex, 2);
                if ((lineindex[0] >= 4 && lineindex[0] <= 14) && (lineindex[1] >= 4 && lineindex[1] <= 18) && lineindex[2] == 11 && lineindex[3] == 28 && lineindex[4] == 32)
                    return matchEvents.redForDefender(lineindex, 3);
            }

            switch (JSON.stringify(lineindex)) {
                case '[4,15,26,16,24]':
                case '[4,15,26,16,26,16,26,24]':
                    return matchEvents.CornerCautch(lineindex);
                case '[5,20,21,22,29,24]':
                    return matchEvents.PenaltyCaught(lineindex);
            }
            return '';
        }

        function substitution(text, persons) {
            var tempText = text;
            while (tempText.indexOf('{') != -1) {
                var index = tempText.substring(tempText.indexOf('{') + 1, tempText.indexOf('}'));
                tempText = tempText.substring(tempText.indexOf('}') + 1);

                switch (index.split(',')[0]) {
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '10':
                    case '11':
                    case '12':
                    case '13':
                    case '14':
                    case '15':
                    case '16':
                    case '17':
                    case '18':
                    case '19':
                    case '20':
                    case '21':
                    case '32':
                        text = text.replace("{" + index + "}", '<span>' + getNameSurnameByGuidId(persons[index.split(',')[1]].attack) + '</span>');
                        break;
                    case '27':
                    case '28':
                        text = text.replace("{" + index + "}", '<span>' + getNameSurnameByGuidId(persons[index.split(',')[1]].defend) + '</span>');
                        break;
                    case '22':
                    case '23':
                    case '24':
                    case '25':
                    case '26':
                        text = text.replace("{" + index + "}", '<span>' + getNameSurnameByGuidId(persons[index.split(',')[1]].goalkeeper) + '</span>');
                        break;
                    default:
                        console.log(persons);
                        console.log(index);
                }
            }
            return text;
        }

        function getNameSurnameByGuidId(guid) {
            for (var i = 0; i < viewmodel.homePlayers().length; i++) {
                if (viewmodel.homePlayers()[i].id == guid) {
                    return viewmodel.homePlayers()[i].name + ' ' + viewmodel.homePlayers()[i].surname;
                }
            }
            for (i = 0; i < viewmodel.guestPlayers().length; i++) {
                if (viewmodel.guestPlayers()[i].id == guid) {
                    return viewmodel.guestPlayers()[i].name + ' ' + viewmodel.guestPlayers()[i].surname;
                }
            }
            return '{}';
        }

        function getCustomEventByMinute(minute) {
            for (var i = 0; i < viewmodel.customEventsLine().length; i++) {
                if (viewmodel.customEventsLine()[i].minute == minute) {
                    return viewmodel.customEventsLine()[i];
                }
            }
            return null;
        }

        function manageCustomGoalEventByMinute(minute) {
            for (var i = 0; i < viewmodel.homeGoalEventsLine().length; i++) {
                if (viewmodel.homeGoalEventsLine()[i].minute == minute) {
                    viewmodel.homeGoalEventsLineOnline.push(viewmodel.homeGoalEventsLine()[i]);
                    viewmodel.homeGoal(viewmodel.homeGoal() + 1);
                }
            }
            for (i = 0; i < viewmodel.guestGoalEventsLine().length; i++) {
                if (viewmodel.guestGoalEventsLine()[i].minute == minute) {
                    viewmodel.guestGoalEventsLineOnline.push(viewmodel.guestGoalEventsLine()[i]);
                    viewmodel.guestGoal(viewmodel.guestGoal() + 1);
                }
            }
        }

        function manageCustomYellowEventByMinute(minute) {
            for (var i = 0; i < viewmodel.homeYellowEventsLine().length; i++) {
                if (viewmodel.homeYellowEventsLine()[i].minute == minute) {
                    viewmodel.homeYellowEventsLineOnline.push(viewmodel.homeYellowEventsLine()[i]);
                }
            }
            for (i = 0; i < viewmodel.guestYellowEventsLine().length; i++) {
                if (viewmodel.guestYellowEventsLine()[i].minute == minute) {
                    viewmodel.guestYellowEventsLineOnline.push(viewmodel.guestYellowEventsLine()[i]);
                }
            }
        }

        function manageCustomRedEventByMinute(minute) {
            for (var i = 0; i < viewmodel.homeRedEventsLine().length; i++) {
                if (viewmodel.homeRedEventsLine()[i].minute == minute) {
                    viewmodel.homeRedEventsLineOnline.push(viewmodel.homeRedEventsLine()[i]);
                }
            }
            for (i = 0; i < viewmodel.guestRedEventsLine().length; i++) {
                if (viewmodel.guestRedEventsLine()[i].minute == minute) {
                    viewmodel.guestRedEventsLineOnline.push(viewmodel.guestRedEventsLine()[i]);
                }
            }
        }
    });