define(['httpWrapper', 'plugins/router', 'userContext'], function(httpWrapper, router, userContext) {

    var viewmodel = {
        activate: activate,
        team: ko.observableArray([]),
        teamId: '',
        removePlayerFromTeam: removePlayerFromTeam,
        goToTeamSettings: goToTeamSettings,
        upateSalary: upateSalary,
        updateNumber: updateNumber
    };

    return viewmodel;

    function updateNumber(player) {
        httpWrapper.post('api/player/updateplayernumber', { id: player.id, salary: player.number }).then(function (response) {
            if (response.success) {
                
            }
        });
    }

    function upateSalary(player) {
        httpWrapper.post('api/player/updateplayersalary', {id: player.id, salary: player.salary}).then(function (response) {
            if (response.success) {

            }
        });
    }

    function goToTeamSettings() {
        router.navigate('teamsettings');
    }

    function activate() {
        viewmodel.team([]);
        var id = userContext.user.playersCollection[0].teamId;
        if (_.isNull(id) || _.isUndefined(id)) {
            router.navigate('home');
        } else {
            viewmodel.teamId = id;
            httpWrapper.post('api/team/getteambyid', { id: id }).then(function (response) {
                if (!response.success) {
                    router.navigate('home');
                } else {
                    parseData(response.data);
                }
            });
        }
    }

    function removePlayerFromTeam(player) {
        httpWrapper.post('api/team/removeplayer', { teamId: viewmodel.teamId, playerId: player.id }).then(function(response) {
            if (response.success) {
                viewmodel.team(_.without(viewmodel.team(), player));
            }
        });
    }

    function parseData(data) {
        viewmodel.team(_.map(data, function (player) {
            return {
                id: player.Id,
                name: player.Name,
                surname: player.Surname,
                publicId: player.PublicId,
                position: {
                    id: player.Position.Id,
                    name: player.Position.Name,
                    publicId: player.Position.PublicId
                },
                country: {
                    id: player.Country.Id,
                    name: player.Country.Name,
                    publicId: player.Country.PublicId
                },
                skills: _.chain(player.Skills).sortBy(function (skill) { return skill.id; })
                    .map(function (skill) {
                        return {
                            id: skill.id,
                            value: skill.value
                        }
                    }).value()
            }
        }));
    }

});