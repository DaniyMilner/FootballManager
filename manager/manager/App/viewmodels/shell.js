define(['plugins/router', 'routing/routes'], function(router, routes) {

    var activate = function () {
        return router.map(routes)
                .buildNavigationModel()
                .activate('home');
    };

    return {        
      activate: activate
    };

});