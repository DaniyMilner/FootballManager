requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions'
    }
});

define('jquery', function () {
    return jQuery;
});

define('knockout', function () {
    return ko;
});

define(['durandal/system', 'durandal/app', 'durandal/viewLocator'],
    function (system, app, viewLocator) {
        system.debug(true);

        app.title = "footballmanager";
        
        app.configurePlugins({
            router: true,
            dialog: true,
            http: true
        });
        
        viewLocator.useConvention();
        
        app.start().then(function () {
            app.setRoot('viewmodels/shell');
        });

    }
);