define(['durandal/app', 'plugins/http', 'constants', 'models/user'],
    function (app, http, constants, UserModel) {

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
                    if (_.isNull(response.data)) {
                        that.isAuthenticated = false;
                    } else {
                        that.isAuthenticated = true;
                    }
                });
            };

        return {
            initialize: initialize,
            isAuthenticated: isAuthenticated,
            user: user
        };
    });