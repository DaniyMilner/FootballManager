define(['plugins/router', 'httpWrapper', 'constants', 'localization/localizationManager', 'userContext'],
    function (router, httpWrapper, constants, localizationManager, userContext) {

        var viewmodel = {
            signOut: signOut,
            navigateToUserProfile: navigateToUserProfile,
            activate: activate
        };

        return viewmodel;

        function signOut() {
            httpWrapper.post('api/user/signout').then(function () {
                router.reloadLocation();
            });
        }

        function navigateToUserProfile() {
            router.navigate('userprofile/');
        }

        function activate() {
            return Q.fcall(function () {

            });
        }
    });