define(['durandal/app', 'plugins/http', 'constants'],
    function (app, http, constants) {

        function parseDateString(str) {
            return new Date(parseInt(str.substr(6), 10));
        }

        var
            isAuthenticated = false,
            user = {},
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
                            publicId: response.data.User.PublicId
                        }
                    }
                });
            };

        return {
            initialize: initialize,
            isAuthenticated: isAuthenticated,
            user: user
        };
    });