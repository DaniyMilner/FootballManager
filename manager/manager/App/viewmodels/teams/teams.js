define(['plugins/router', 'httpWrapper'], function (router, httpWrapper) {

    var viewmodel = {
        activate: activate,
        teams: ko.observableArray([])
    };

    return viewmodel;

    function activate(publicId) {
        viewmodel.teams([]);
        if (_.isNull(publicId) || _.isUndefined(publicId)) {
            return httpWrapper.post('api/teams/getteamsbycountrypublicid').then(function (response) {
                debugger;
                parseResponse(response);
            });
        } else {
            return httpWrapper.post('api/teams/getteamsbycountrypublicid/'+ publicId).then(function (response) {
                debugger;
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
                    country: team.Country.Name,
                    stadium: team.Stadium,
                    year: team.Year
                }
            }));
        } else {
            router.navigate('home');
        }
    }

});