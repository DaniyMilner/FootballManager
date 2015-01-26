define(['plugins/router', 'httpWrapper', 'userContext'], function (router, httpWrapper, userContext) {

    var viewmodel = {
        goToTeam: goToTeam,
        teams: ko.observableArray([]),
        isAuthenticated: userContext.isAuthenticated && userContext.hasPlayer && userContext.user.playersCollection[0] && !userContext.user.playersCollection[0].teamId,
        applyToJoin: applyToJoin,
        activate: activate
    };

    return viewmodel;

    function applyToJoin(team) {
        httpWrapper.post('api/team/applytojoin', { playerId: userContext.user.playersCollection[0].id }).then(function (response) {
            if (response.success) {
                router.navigate('team/' + team.shortName);
            }
        });
    }

    function goToTeam(team) {
        router.navigate('team/' + team.shortName);
    }

    function activate(publicId) {
        viewmodel.teams([]);
        if (_.isNull(publicId) || _.isUndefined(publicId)) {
            return httpWrapper.post('api/teams/getteamsbycountrypublicid').then(function (response) {
                parseResponse(response);
            });
        } else {
            return httpWrapper.post('api/teams/getteamsbycountrypublicid/'+ publicId).then(function (response) {
                parseResponse(response);
            });
        }
    }

    function parseResponse(response) {
        if (response.data) {
            viewmodel.teams(_.map(response.data, function(team) {
                return {
                    id: team.Id,
                    name: team.Name,
                    shortName: team.ShortName,
                    country: team.Country,
                    stadium: team.Stadium,
                    year: team.Year
                }
            }));
            viewmodel.teams(_.sortBy(viewmodel.teams(), function(item) {
                return item.country;
            }));
        } else {
            router.navigate('home');
        }
    }

});