define(['userContext', 'httpWrapper'], function (userContext, httpWrapper) {

    var viewmodel = {
        matchId: ko.observable(''),
        matchDate: ko.observable(''),

        teamId: ko.observable(''),
        teamName: ko.observable(''),
        teamShortName: ko.observable(''),

        rivalName: ko.observable(''),
        rivalShortName: ko.observable(''),

        allArragements: ko.observableArray([]),
        selectedArragement: ko.observable(''),

        allPlayers: ko.observableArray([]),

        FWList: ko.observableArray([]),
        MIDList: ko.observableArray([]),
        DEFList: ko.observableArray([]),
        GKList: ko.observableArray([]),

        selectedFWList: ko.observableArray([]),
        selectedMIDList: ko.observableArray([]),
        selectedDEFList: ko.observableArray([]),
        selectedGKList: ko.observableArray([]),

        selectedGK: ko.observable(''),
        selectedDEF: ko.observable(''),
        selectedMID: ko.observable(''),
        selectedFW: ko.observable(''),

        canAddGK: ko.observable(true),
        canAddDEF: ko.observable(true),
        canAddMID: ko.observable(true),
        canAddFW: ko.observable(true),

        countGK: ko.observable(1),
        countDEF: ko.observable(0),
        countMID: ko.observable(0),
        countFW: ko.observable(0),

        stepTwoVisible: ko.observable(false),
        stepOkVisible: ko.observable(false),
        playersForSettings: ko.observableArray([]),

        selectedCapitan: ko.observable(''),
        selectedFreeKick: ko.observable(''),
        selectedCorner: ko.observable(''),
        selectedPenalty: ko.observable(''),

        noMatch: ko.observable(false),

        activate: activate,
        showStepTwo: function () {
            viewmodel.stepTwoVisible(true);
            for (var i = 0; i < viewmodel.selectedGKList().length; i++) {
                viewmodel.playersForSettings.push(viewmodel.selectedGKList()[i]);
            }
            for (i = 0; i < viewmodel.selectedDEFList().length; i++) {
                viewmodel.playersForSettings.push(viewmodel.selectedDEFList()[i]);
            }
            for (i = 0; i < viewmodel.selectedMIDList().length; i++) {
                viewmodel.playersForSettings.push(viewmodel.selectedMIDList()[i]);
            }
            for (i = 0; i < viewmodel.selectedFWList().length; i++) {
                viewmodel.playersForSettings.push(viewmodel.selectedFWList()[i]);
            }
        },
        addGK: function () {
            viewmodel.selectedGKList.push(viewmodel.selectedGK());
            viewmodel.GKList.remove(viewmodel.selectedGK());
            viewmodel.checkGK();
        },
        removeGK: function (item) {
            viewmodel.GKList.push(item);
            viewmodel.selectedGKList.remove(item);
            viewmodel.checkGK();
        },
        checkGK: function () {
            viewmodel.selectedGKList().length == viewmodel.countGK() ? viewmodel.canAddGK(false) : viewmodel.canAddGK(true);
        },
        addDEF: function () {
            viewmodel.selectedDEFList.push(viewmodel.selectedDEF());
            viewmodel.DEFList.remove(viewmodel.selectedDEF());
            viewmodel.checkDEF();
        },
        removeDEF: function (item) {
            viewmodel.DEFList.push(item);
            viewmodel.selectedDEFList.remove(item);
            viewmodel.checkDEF();
        },
        checkDEF: function () {
            viewmodel.selectedDEFList().length == viewmodel.countDEF() ? viewmodel.canAddDEF(false) : viewmodel.canAddDEF(true);
        },
        addMID: function () {
            viewmodel.selectedMIDList.push(viewmodel.selectedMID());
            viewmodel.MIDList.remove(viewmodel.selectedMID());
            viewmodel.checkMID();
        },
        removeMID: function (item) {
            viewmodel.MIDList.push(item);
            viewmodel.selectedMIDList.remove(item);
            viewmodel.checkMID();
        },
        checkMID: function () {
            viewmodel.selectedMIDList().length == viewmodel.countMID() ? viewmodel.canAddMID(false) : viewmodel.canAddMID(true);
        },
        addFW: function () {
            viewmodel.selectedFWList.push(viewmodel.selectedFW());
            viewmodel.FWList.remove(viewmodel.selectedFW());
            viewmodel.checkFW();
        },
        removeFW: function (item) {
            viewmodel.FWList.push(item);
            viewmodel.selectedFWList.remove(item);
            viewmodel.checkFW();
        },
        checkFW: function () {
            viewmodel.selectedFWList().length == viewmodel.countFW() ? viewmodel.canAddFW(false) : viewmodel.canAddFW(true);
        },

        send: function () {
            var data = {
                Corner: viewmodel.selectedCorner().id,
                FreeKick: viewmodel.selectedFreeKick().id,
                Penalty: viewmodel.selectedPenalty().id,
                Capitan: viewmodel.selectedCapitan().id,
                One: viewmodel.playersForSettings()[0].id,
                Two: viewmodel.playersForSettings()[1].id,
                Three: viewmodel.playersForSettings()[2].id,
                Four: viewmodel.playersForSettings()[3].id,
                Five: viewmodel.playersForSettings()[4].id,
                Six: viewmodel.playersForSettings()[5].id,
                Seven: viewmodel.playersForSettings()[6].id,
                Eight: viewmodel.playersForSettings()[7].id,
                Nine: viewmodel.playersForSettings()[8].id,
                Ten: viewmodel.playersForSettings()[9].id,
                Eleven: viewmodel.playersForSettings()[10].id,
                ArragementId: viewmodel.selectedArragement().id,
                MatchId: viewmodel.matchId(),
                PlayerId: userContext.user.id,
                TeamId: viewmodel.teamId()
            };
            httpWrapper.post('api/team/sendSettings', data).then(function (response) {
                if (response.success) {
                    viewmodel.stepOkVisible(true);
                }
            }).fail(function (response) {
                console.log(response);
            });
        }
    };

    function activate() {
        clear();
        viewmodel.noMatch(false);
        return httpWrapper.post('api/team/getTeamForMatch', { userId: userContext.user.id }).then(function (response) {
            if (response.success) {
                if (typeof response.data == 'object') {
                    console.log(response.data);
                    viewmodel.matchId(response.data.match.id);
                    viewmodel.matchDate(getDateFromString(response.data.match.date));
                    viewmodel.teamId(response.data.teamId);
                    viewmodel.teamName(response.data.teamName);
                    viewmodel.teamShortName(response.data.teamShortName);
                    viewmodel.rivalName(response.data.match.rivalName);
                    viewmodel.rivalShortName(response.data.match.rivalShortName);

                    viewmodel.allArragements(response.data.allArragements);
                    viewmodel.allPlayers(response.data.playersList);
                    managePlayersList();

                    if (response.data.teamSettings) {
                        viewmodel.selectedArragement(getSelectedArragement(response.data.teamSettings.arragement));
                        manageSelectedArragement();
                        manageLineUp(response);
                        
                    }
                } else {
                    console.log(response.data);
                    viewmodel.noMatch(true);
                }
            }
        }).fail(function (response) {
            console.log(response);
        });
    }

    viewmodel.selectedArragement.subscribe(function () {
        manageSelectedArragement();
    });

    return viewmodel;



    function getDateFromString(datestring) {
        return new Date(parseInt(datestring.replace('/Date(', '').replace(')/', ''))).toLocaleString();
    }

    function getSelectedArragement(arragement) {
        for (var i = 0; i < viewmodel.allArragements().length; i++) {
            if (viewmodel.allArragements()[i].id == arragement.id)
                return viewmodel.allArragements()[i];
        }
        return viewmodel.allArragements()[viewmodel.allArragements().length - 1];
    }

    function managePlayersList() {
        for (var i = 0; i < viewmodel.allPlayers().length; i++) {
            switch (viewmodel.allPlayers()[i].position) {
                case 'FW':
                    viewmodel.FWList.push(viewmodel.allPlayers()[i]);
                    break;
                case 'MID':
                    viewmodel.MIDList.push(viewmodel.allPlayers()[i]);
                    break;
                case 'DEF':
                    viewmodel.DEFList.push(viewmodel.allPlayers()[i]);
                    break;
                case 'GK':
                    viewmodel.GKList.push(viewmodel.allPlayers()[i]);
                    break;
            }
        }
    }

    function manageSelectedArragement() {
        clear();

        if (viewmodel.selectedArragement()) {
            var scheme = viewmodel.selectedArragement().scheme.replace('Scheme', '');

            viewmodel.countDEF(parseInt(scheme[0]));
            viewmodel.countMID(parseInt(scheme[1]));
            viewmodel.countFW(parseInt(scheme[2]));
        }
        managePlayersList();
    }

    function clear() {
        viewmodel.selectedGKList([]);
        viewmodel.selectedDEFList([]);
        viewmodel.selectedMIDList([]);
        viewmodel.selectedFWList([]);
        viewmodel.GKList([]);
        viewmodel.DEFList([]);
        viewmodel.MIDList([]);
        viewmodel.FWList([]);
        viewmodel.canAddGK(true);
        viewmodel.canAddDEF(true);
        viewmodel.canAddMID(true);
        viewmodel.canAddFW(true);
        viewmodel.stepTwoVisible(false);

        viewmodel.playersForSettings([]);
    }

    function getPlayerById(id) {
        for (var i = 0; i < viewmodel.allPlayers().length; i++) {
            if (viewmodel.allPlayers()[i].id == id) {
                return viewmodel.allPlayers()[i];
            }
        }
        return null;
    }

    function manageLineUp(response) {
        var players = [];
        players.push(getPlayerById(response.data.teamSettings.lineUp.one));
        players.push(getPlayerById(response.data.teamSettings.lineUp.two));
        players.push(getPlayerById(response.data.teamSettings.lineUp.three));
        players.push(getPlayerById(response.data.teamSettings.lineUp.four));
        players.push(getPlayerById(response.data.teamSettings.lineUp.five));
        players.push(getPlayerById(response.data.teamSettings.lineUp.six));
        players.push(getPlayerById(response.data.teamSettings.lineUp.seven));
        players.push(getPlayerById(response.data.teamSettings.lineUp.eight));
        players.push(getPlayerById(response.data.teamSettings.lineUp.nine));
        players.push(getPlayerById(response.data.teamSettings.lineUp.ten));
        players.push(getPlayerById(response.data.teamSettings.lineUp.eleven));
        for (var i = 0; i < players.length; i++) {
            switch (players[i].position) {
                case 'FW':
                    viewmodel.selectedFWList.push(players[i]);
                    viewmodel.FWList.remove(players[i]);
                    viewmodel.checkFW();
                    break;
                case 'MID':
                    viewmodel.selectedMIDList.push(players[i]);
                    viewmodel.MIDList.remove(players[i]);
                    viewmodel.checkMID();
                    break;
                case 'DEF':
                    viewmodel.selectedDEFList.push(players[i]);
                    viewmodel.DEFList.remove(players[i]);
                    viewmodel.checkDEF();
                    break;
                case 'GK':
                    viewmodel.selectedGKList.push(players[i]);
                    viewmodel.GKList.remove(players[i]);
                    viewmodel.checkGK();
                    break;
            }
        }
    }
});