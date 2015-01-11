define(['durandal/app', 'plugins/http', 'constants'],
    function (app, http, constants) {

        function parseDateString(str) {
            return str != null ? new Date(parseInt(str.substr(6), 10)) : null;
        }

        var
            isAuthenticated = false,
            hasPlayer = false,
            user = {},
            isCoatch = false,
            initialize = function () {
                var that = this;
                return $.ajax({
                    url: 'api/user/getuserinfo',
                    type: 'POST',
                    contentType: 'application/json',
                    dataType: 'json'
                }).then(function (response) {
                    if (_.isNull(response.data.User) || _.isUndefined(response.data.User)) {
                        that.isAuthenticated = false;
                    } else {
                        that.isAuthenticated = true;
                        that.hasPlayer = response.data.User.PlayerCollection.length != 0;
                        that.user = {
                            id: response.data.User.Id,
                            email: response.data.User.Email,
                            username: response.data.User.UserName,
                            skype: response.data.User.Skype,
                            parentId: response.data.User.ParentId,
                            birthday: parseDateString(response.data.User.Birthday),
                            city: response.data.User.City,
                            aboutmyself: response.data.User.AboutMySelf,
                            sex: response.data.User.Sex,
                            publicId: response.data.User.PublicId,
                            playersCollection: _.map(response.data.User.PlayerCollection, function(item) {
                                return {
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
                                    salary: item.Salary,
                                    weight: item.Weight,
                                    teamId: item.TeamId,
                                    country: _.map(item.Country, function(country) {
                                        return {
                                            id: country.Id,
                                            name: country.Name,
                                            publicId: country.PublicId
                                        };
                                    }),
                                    illness: _.map(item.Illness, function (illness) {
                                        return {
                                            id: illness.Id,
                                            name: illness.IllnessName,
                                            timeForRecovery: illness.TimeForRecovery
                                        };
                                    }),
                                    position: _.map(item.Position, function (position) {
                                        return {
                                            id: position.Id,
                                            name: position.Name,
                                            publicId: position.PublicId
                                        };
                                    })
                                };
                            })
                        }
                    }
                }).then(function() {
                    return $.ajax({
                        url: 'api/player/iscoatch',
                        type: 'POST',
                        contentType: 'application/json',
                        dataType: 'json'
                    }).then(function (response) {
                        if (response.success) {
                            that.isCoatch = true;
                        } else {
                            that.isCoatch = false;
                        }
                    });
                });
            };

        return {
            initialize: initialize,
            isAuthenticated: isAuthenticated,
            user: user,
            hasPlayer: hasPlayer,
            isCoatch: isCoatch
        };
    });