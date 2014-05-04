define(['plugins/router', 'routing/routes', 'userContext', 'localization/localizationManager', 'plugins/http'],
    function (router, routes, userContext, localizationManager, http) {

        var
            isAuthenticated = ko.observable(false),

            isShowSignInForm = ko.observable(false),

            activate = function () {
                var that = this;
                http.get('http://football.ua/').then(function(response) {
                    debugger;
                });
                return userContext.initialize().then(function () {
                    return localizationManager.initialize().then(function () {
                        that.isAuthenticated(userContext.isAuthenticated);

                        router.reloadLocation = function () {
                            document.location.reload();
                        };

                        return router.map(routes)
                            .buildNavigationModel()
                            .activate('home');
                    });
            });
        };

        return {
            isAuthenticated: isAuthenticated,
            isShowSignInForm: isShowSignInForm,
            activate: activate
        };

    });