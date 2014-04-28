define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            self: this,

            activePageIndex: ko.observable(1),

            homeGoal: ko.observable(0), //+
            guestGoal: ko.observable(0), //+

            matchId: '', //+
            matchDateStart: ko.observable(''), //+

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
            homeGoalEventsLine: ko.observableArray([]),//+
            guestGoalEventsLine: ko.observableArray([]),//+
            homeYellowEventsLine: ko.observableArray([]),//+
            guestYellowEventsLine: ko.observableArray([]),//+
            homeRedEventsLine: ko.observableArray([]),//+
            guestRedEventsLine: ko.observableArray([]),//+

            activate: activate,
            manageEventLine: function () {
                //wtf
                viewmodel.customEventsLine([]);
                viewmodel.homeGoalEventsLine([]);
                viewmodel.guestGoalEventsLine([]);
                viewmodel.homeYellowEventsLine([]);
                viewmodel.guestYellowEventsLine([]);
                viewmodel.homeRedEventsLine([]);
                viewmodel.guestRedEventsLine([]);
                viewmodel.homeGoal(0);
                viewmodel.guestGoal(0);

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
                                    viewmodel.homeGoal(viewmodel.homeGoal() + 1);
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
                                    viewmodel.guestGoal(viewmodel.guestGoal() + 1);
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

                        viewmodel.manageEventLine();
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
    });