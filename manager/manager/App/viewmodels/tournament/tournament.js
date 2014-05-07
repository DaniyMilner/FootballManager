define(['plugins/router', 'httpWrapper'], function (router, httpWrapper) {

    var viewmodel = {
        tournamentId: ko.observable(''),
        tournamentTable: ko.observableArray([]),
        lastTour: ko.observable(''),
        nextTour: ko.observable(''),
        lastMatches: ko.observableArray([]),
        nextMatches: ko.observableArray([]),
        activate: activate,
        gettournamentinfo: gettournamentinfo,
        getLastMatches: getLastMatches,
        getNextMatches: getNextMatches
    };

    function activate(id) {
        viewmodel.tournamentId(id);
        return viewmodel.gettournamentinfo();
    }
    function gettournamentinfo() {
        return httpWrapper.post('api/tournament/gettournamentinfo', { id: viewmodel.tournamentId() }).then(function (response) {
            viewmodel.tournamentTable([]);
           
            if (response.success) {
                if (typeof response.data == 'object') {
                    for (var i = 0; i < response.data.length; i++) {
                        viewmodel.tournamentTable.push(response.data[i]);
                    }
                } else {
                    router.navigate('home');
                }
            }
            viewmodel.getLastMatches();
            viewmodel.getNextMatches();
        }).fail(function (response) {
            console.log(response);
        });
    }

    function getLastMatches() {
        httpWrapper.post('api/tournament/getlastmatches', { id: viewmodel.tournamentId() }).then(function (response) {
            viewmodel.lastMatches([]);
            if (response.success) {
                if (typeof response.data == 'object') {
                    for (var i = 0; i < response.data.resultList.length; i++) {
                        viewmodel.lastMatches.push(response.data.resultList[i]);
                    }
                    viewmodel.lastTour(response.data.tour);
                } else {
                    router.navigate('home');
                }
            }
        }).fail(function (response) {
            console.log(response);
        });
    }

    function getNextMatches() {
        httpWrapper.post('api/tournament/getnextmatches', { id: viewmodel.tournamentId() }).then(function (response) {
            viewmodel.lastMatches([]);
            if (response.success) {
                if (typeof response.data == 'object') {
                    for (var i = 0; i < response.data.resultList.length; i++) {
                        viewmodel.nextMatches.push(response.data.resultList[i]);
                    }
                    viewmodel.nextTour(response.data.tour);
                } else {
                    router.navigate('home');
                }
            }
        }).fail(function (response) {
            console.log(response);
        });
    }

    return viewmodel;

});