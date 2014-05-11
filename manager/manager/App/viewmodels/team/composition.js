define(['httpWrapper', 'plugins/router', 'constants'], function (httpWrapper, router, constants) {

    var viewmodel = {
        activate: activate,
        team: ko.observableArray([]),
        teamFW: ko.observableArray([]),
        teamDEF: ko.observableArray([]),
        teamGK: ko.observableArray([]),
        teamMID: ko.observableArray([]),
        skillsGK: ko.observableArray([]),
        skillsDEF: ko.observableArray([]),
        skillsMID: ko.observableArray([]),
        skillsFW: ko.observableArray([]),
        goToPlayerProfile: goToPlayerProfile
    };

    return viewmodel;

    function goToPlayerProfile(player) {
        router.navigate('playerprofile/' + player.publicId);
    }

    function activate(id) {
        clearData();
        return httpWrapper.post('api/team/getteaminfobypublicid/', { id: id }).then(function (response) {
            if (!response.success && response.data) {
                router.navigate('home');
            } else {
                viewmodel.team(_.map(response.data, function (player) {
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
                        skills: _.chain(player.Skills).sortBy(function(skill) { return skill.id; })
                            .map(function(skill) {
                                return {
                                    id: skill.id,
                                    value: skill.value
                                }
                            }).value()
                    }
                }));
                parseTeam(viewmodel.team());
            }
        });
    }

    function parseTeam(team) {
        _.each(team, function (player) {
            if (player.position.publicId == constants.positions.GK) {
                if (viewmodel.skillsGK().length == 0) {
                    viewmodel.skillsGK(_.chain(player.skills).sortBy(function (skill) { return skill.id; })
                        .map(function (skill) {
                            var temp = constants.skills[skill.id];
                            return {
                                id: skill.id,
                                abbr: temp.abbr,
                                name: temp.name,
                                value: skill.value
                            };
                    }).value());
                }
                viewmodel.teamGK.push(player);
            } else if (player.position.publicId == constants.positions.FW) {
                if (viewmodel.skillsFW().length == 0) {
                    viewmodel.skillsFW(_.chain(player.skills).sortBy(function (skill) { return skill.id; })
                        .map(function (skill) {
                            var temp = constants.skills[skill.id];
                            return {
                                id: skill.id,
                                abbr: temp.abbr,
                                name: temp.name,
                                value: skill.value
                            };
                        }).value());
                }
                viewmodel.teamFW.push(player);
            } else if (player.position.publicId == constants.positions.DEF) {
                if (viewmodel.skillsDEF().length == 0) {
                    viewmodel.skillsDEF(_.chain(player.skills).sortBy(function (skill) { return skill.id; })
                        .map(function (skill) {
                            var temp = constants.skills[skill.id];
                            return {
                                id: skill.id,
                                abbr: temp.abbr,
                                name: temp.name,
                                value: skill.value
                            };
                        }).value());
                }
                viewmodel.teamDEF.push(player);
            } else if (player.position.publicId == constants.positions.MID) {
                if (viewmodel.skillsMID().length == 0) {
                    viewmodel.skillsMID(_.chain(player.skills).sortBy(function (skill) { return skill.id; })
                        .map(function (skill) {
                            var temp = constants.skills[skill.id];
                            return {
                                id: skill.id,
                                abbr: temp.abbr,
                                name: temp.name,
                                value: skill.value
                            };
                        }).value());
                }
                viewmodel.teamMID.push(player);
            }
        });
    }

    function clearData() {
        viewmodel.team([]);
        viewmodel.teamMID([]);
        viewmodel.teamFW([]);
        viewmodel.teamDEF([]);
        viewmodel.teamGK([]);
        viewmodel.skillsGK([]);
        viewmodel.skillsDEF([]);
        viewmodel.skillsMID([]);
        viewmodel.skillsFW([]);
    }

})