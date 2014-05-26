define(['httpWrapper', 'plugins/router'], function (httpWrapper, router) {

    var viewmodel = {
        activate: activate,
        players: ko.observableArray([]),
        goToPlayerProfile: goToPlayerProfile,
        title: ko.observable('')
    };

    return viewmodel;

    function activate(position) {
        viewmodel.players([]);
        viewmodel.title(setTitle(position));
        if (_.isNull(position) || _.isUndefined(position)) {
            httpWrapper.post('api/player/getplayersbyposition').then(function (response) {
                parseResponse(response);
            });
        } else {
            httpWrapper.post('api/player/getplayersbyposition', { position: position }).then(function (response) {
                parseResponse(response);
            });
        }
    }

    function parseResponse(response) {
        viewmodel.players(_.chain(response.data).sortBy(function (player) {
            return player.skillValue;
        }).reverse().map(function (player, index) {
            return {
                index: index + 1,
                fullName: player.fullName,
                countryId: player.countryId,
                countryName: player.countryName,
                skillValue: player.skillValue,
                publicId: player.publicId
            }
        }).value());
    }

    function setTitle(position) {
        debugger;
        switch (position) {
            case 'GK':
                return 'Топ голкиперов';
            case 'MID':
                return 'Топ полузащитников';
            case 'DEF':
                return 'Топ защитников';
            case 'FW':
                return 'Топ нападающих';  
        }
        return 'Общий топ';
    }

    function goToPlayerProfile(player) {
        router.navigate('playerprofile/' + player.publicId);
    }

});