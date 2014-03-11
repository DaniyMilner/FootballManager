define(['plugins/router', 'routing/routes', 'userContext', 'localization/localizationManager'],
    function (router, routes, userContext, localizationManager) {

        var activate = function () {
            return userContext.initialize().then(function () {
                return localizationManager.initialize().then(function () {
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
            activate: activate
        };

    });