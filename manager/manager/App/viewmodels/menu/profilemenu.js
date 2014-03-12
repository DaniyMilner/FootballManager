define(['plugins/router', 'httpWrapper', 'constants', 'localization/localizationManager', 'userContext'],
    function (router, httpWrapper, constants, localizationManager, userContext) {

        var viewmodel = {
            signOut: signOut,
            activate: activate
        };

        return viewmodel;

        function signOut() {
            httpWrapper.post('api/user/signout').then(function () {
                router.reloadLocation();
            });
        }

        function activate() {
            return Q.fcall(function () {

            });
        }
    });