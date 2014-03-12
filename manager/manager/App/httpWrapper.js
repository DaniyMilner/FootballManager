define(['plugins/http', 'durandal/app', 'localization/localizationManager'],
    function (http, app, localizationManager) {

        var
            post = function (url, data) {
                var deferred = Q.defer();
                app.trigger('httpWrapper:post-begin');
                http.post(url, data)
                    .done(function (response) {
                        deferred.resolve(response);
                    })
                    .always(function () {
                        app.trigger('httpWrapper:post-end');
                    });

                return deferred.promise;
            }
        ;

        return {
            post: post
        };
    });