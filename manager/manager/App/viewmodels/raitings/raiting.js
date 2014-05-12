define(['httpWrapper', 'plugins/router'], function (httpWrapper, router) {

    var viewmodel = {
        activate: activate,
        players: ko.observableArray([]),
        goToPlayerProfile: goToPlayerProfile
    };

    return viewmodel;

    function activate(position) {
        viewmodel.players([]);
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

    function goToPlayerProfile(player) {
        router.navigate('playerprofile/' + player.publicId);
    }

});