define(['plugins/router', 'userContext'], function(router, userContext) {

    var viewmodel = {
        activate: activate,
        isAuthenticated: ko.observable(false),
        goToPlayerProfile: goToPlayerProfile,
        goToEquipment: goToEquipment,
        goToTeamComposition: goToTeamComposition
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
    }

    function goToTeamComposition() {
        var teamid = userContext.user.playersCollection[0].teamId;
        if (_.isNull(teamid) || _.isUndefined(teamid)) {
            router.navigate('team');
        } else {
            router.navigate('team/' + teamid);
        }
    }

});