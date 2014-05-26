define(['plugins/router', 'userContext', 'httpWrapper'], function (router, userContext, httpWrapper) {

    var viewmodel = {
        activate: activate,
        isAuthenticated: ko.observable(false),
        hasTeam: ko.observable(false),
        team: ko.observable(null),
        goToPlayerProfile: goToPlayerProfile,
        goToEquipment: goToEquipment,
        goToTeamComposition: goToTeamComposition,
        goToTeamSettings:goToTeamSettings,
        isCoatch: ko.observable(false),
        seasonsList: ko.observableArray([]),
        getSeasonsList: getSeasonsList,
        goToRaiting: goToRaiting,
        goToGKRaiting: goToGKRaiting,
        goToDEFRaiting: goToDEFRaiting,
        goToMIDRaiting: goToMIDRaiting,
        goToFWRaiting: goToFWRaiting,
        goToOwnTeam: goToOwnTeam,
        goToTraining: goToTraining,
        goToTeams: goToTeams,
        goToUATeams: goToUATeams,
        goToENTeams: goToENTeams,
        goToESTeams: goToESTeams,
        goToCoathRoom: goToCoathRoom,
        goToCity: goToCity
    };

    return viewmodel;

    function goToCity() {
        router.navigate('city');
    }

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

    function goToCoathRoom() {
        router.navigate('coach');
    }

    function goToTeamSettings() {
        router.navigate('teamsettings');
    }

    function goToTraining() {
        router.navigate('training');
    }

    function activate() {
        this.isAuthenticated(userContext.isAuthenticated);
        if (userContext.isCoatch) {
            viewmodel.isCoatch(true);
        } else {
            viewmodel.isCoatch(false);
        }
        if (userContext.hasPlayer) {
            return getTeamName(userContext.user.playersCollection[0].teamId).then(function() {
                return viewmodel.getSeasonsList();
            });
        } else {
            return viewmodel.getSeasonsList();
        }
    }

    function getTeamName(teamId) {
        return httpWrapper.post('api/team/getteamname', { id: teamId }).then(function (response) {
            if (response.success) {
                viewmodel.hasTeam(true);
                viewmodel.team(response.data);
            } else {
                viewmodel.hasTeam(false);
            }
        });
    }

    function goToOwnTeam() {
        router.navigate('team/' + viewmodel.team().shortName);
    }

    function goToTeamComposition() {
        var teamid;
        if (userContext.user.playersCollection != undefined) {
            teamid = userContext.user.playersCollection[0].teamId;
        }
         
        if (_.isNull(teamid) || _.isUndefined(teamid)) {
            router.navigate('team');
        } else {
            router.navigate('team/' + teamid);
        }
    }

    function getSeasonsList() {
        viewmodel.seasonsList([]);
        return httpWrapper.post('api/menu/getseasonslist').then(function (response) {
            if (response.success) {
                for (var i = 0; i < response.data.length; i++) {
                    viewmodel.seasonsList.push(response.data[i]);
                }
            }
        }).fail(function (response) {
            console.log(response);
        });
    }

    function goToTeams() {
        router.navigate('teams');
    }

    function goToTeamsById(id) {
        router.navigate('teams/' + id);
    }

    function goToUATeams() {
        goToTeamsById('UA');
    }
    function goToENTeams() {
        goToTeamsById('EN');
    }
    function goToESTeams() {
        goToTeamsById('ES');
    }

    function goToRaiting() {
        router.navigate('raiting');
    }

    function goToRaitingByPosition(position) {
        router.navigate('raiting/' + position);
    }

    function goToGKRaiting() {
        goToRaitingByPosition('GK');
    }
    function goToDEFRaiting() {
        goToRaitingByPosition('DEF');
    }
    function goToMIDRaiting() {
        goToRaitingByPosition('MID');
    }
    function goToFWRaiting() {
        goToRaitingByPosition('FW');
    }

});