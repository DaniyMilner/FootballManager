define(['durandal/app', 'plugins/http', 'constants'],
    function (app, http, constants) {

        function parseDateString(str) {
            return new Date(parseInt(str.substr(6), 10));
        }

        var
            initialize = function () {
                return Q.fcall(function() {

                });
            };

        return {
            initialize: initialize
        };
    });