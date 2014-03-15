define(['plugins/router', 'userContext'], function (router, userContext) {

    var
        isAuthenticated = ko.observable(false)
        activate = function () {
            this.isAuthenticated(userContext.isAuthenticated)
        };

    return {
        isAuthenticated: isAuthenticated,
        activate: activate
    };

})