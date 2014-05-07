define(['plugins/router', 'httpWrapper'], function (router,httpWrapper) {

    var viewmodel = {
        seasonId: ko.observable(''),
        tournamentsList: ko.observableArray([]),
        activate: activate
    };

    function activate(id) {
        viewmodel.seasonId(id);
        return httpWrapper.post('api/tournament/gettournamentlistbyseasonid', { seasonTitle: viewmodel.seasonId() }).then(function (response) {
            viewmodel.tournamentsList([]);
            if (response.success) {
                if (typeof response.data == 'object') {
                    for (var i = 0; i < response.data.length; i++) {
                        viewmodel.tournamentsList.push(response.data[i]);
                    }
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