define(['plugins/router', 'routing/routes', 'dataContext', 'localization/localizationManager'],
    function (router, routes, dataContext, localizationManager) {

        var activate = function () {
            return dataContext.initialize().then(function () {
                return localizationManager.initialize().then(function() {
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