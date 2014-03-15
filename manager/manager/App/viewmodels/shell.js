define(['plugins/router', 'routing/routes', 'userContext', 'localization/localizationManager'],
    function (router, routes, userContext, localizationManager) {

        var
            isAuthenticated = ko.observable(false),

            isShowSignInForm = ko.observable(false),

            activate = function () {
                var that = this;
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