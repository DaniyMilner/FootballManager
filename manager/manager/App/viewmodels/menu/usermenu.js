﻿define(['plugins/router', 'userContext', 'httpWrapper'], function (router, userContext, httpWrapper) {

    var viewmodel = {
        activate: activate,
        isAuthenticated: ko.observable(false),
        goToPlayerProfile: goToPlayerProfile,
        goToEquipment: goToEquipment,
        goToTeamComposition: goToTeamComposition,
        goToTeamsByCuntryId: goToTeamsByCuntryId,

        seasonsList: ko.observableArray([]),
        getSeasonsList: getSeasonsList
    };

    return viewmodel;

    function goToPlayerProfile() {
        var publicId = userContext.user.playersCollection[0].publicId;
        if (_.isNull(publicId) || _.isUndefined(publicId)) {
            router.navigate('playerprofile');
        } else {
            router.navigate('playerprofile/' + publicId);
        }
    }

    function goToEquipment() {
        router.navigate('equipment');
    }

    function activate() {
        this.isAuthenticated(userContext.isAuthenticated);
        viewmodel.getSeasonsList();
    }

    function goToTeamComposition() {
        var teamid = userContext.user.playersCollection[0].teamId;
        if (_.isNull(teamid) || _.isUndefined(teamid)) {
            router.navigate('team');
        } else {
            router.navigate('team/' + teamid);
        }
    }

    function getSeasonsList() {
        viewmodel.seasonsList([]);
        httpWrapper.post('api/menu/getseasonslist').then(function (response) {
            if (response.success) {
                for (var i = 0; i < response.data.length; i++) {
                    viewmodel.seasonsList.push(response.data[i]);
                }
            }
        }).fail(function (response) {
            console.log(response);
        });
    }

    function goToTeamsByCuntryId(id) {
        if (_.isNull(id)) {
            router.navigate('teams');
        } else {
            router.navigate('teams/' + id);
        }

    }
});