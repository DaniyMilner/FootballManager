define(['httpWrapper', 'constants'], function (httpWrapper, constants) {

    var viewmodel = {
        activate: activate,
        isNotHavePlayer: ko.observable(false),
        player: {},
        skills: {}
    };

    return viewmodel;

    function activate(id) {
        viewmodel.player = {};
        viewmodel.skills = {};
        if (_.isNull(id) || _.isUndefined(id)) {
            viewmodel.isNotHavePlayer(true);
        }

        return httpWrapper.post('api/player/getplayerinfo', { publicId: id }).then(function (response) {
            if (!response.success) {
                router.navigate('createplayer');
            } else {
                var item = response.data;
                viewmodel.player = {
                    id: item.Id,
                    name: item.Name,
                    surname: item.Surname,
                    age: item.Age,
                    condition: item.Condition,
                    createDate: parseDateString(item.CreateDate),
                    growth: item.Growth,
                    humor: item.Humor,
                    money: item.Money,
                    number: item.Number,
                    publicId: item.PublicId,
                    salary: _.isUndefined(item.Salary) ? '-' : item.Salary,
                    weight: item.Weight,
                    teamId: item.TeamId,
                    country: item.Country,
                    illness: item.Illness,
                    position: item.Position,
                    skills: item.Skills
                };

                var sum = 0;
                _.each(item.Skills, function(item) {
                    sum += item.value;
                });

                viewmodel.skills = _.map(item.Skills, function(skill) {
                    var temp = constants.skills[skill.id];
                    return {
                        name: temp.name,
                        value: skill.value,
                        width: Math.round((skill.value / sum) * 100)
                    };
                });
            }
        }).then(function() {
            return httpWrapper.post('api/team/getteamname', { id: viewmodel.player.teamId }).then(function (response) {
                debugger;   
                if (response.success) {
                    viewmodel.player.teamId = response.data.name;
                }
            });
        });
    }

    function parseDateString(str) {
        return new Date(parseInt(str.substr(6), 10));
    }

});