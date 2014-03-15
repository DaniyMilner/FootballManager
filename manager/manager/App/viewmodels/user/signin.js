define(['plugins/router', 'httpWrapper', 'constants', 'localization/localizationManager', 'userContext'],
    function (router, httpWrapper, constants, localizationManager, userContext) {

        var viewmodel = {
            username: ko.observable(''),
            password: ko.observable(''),
            remeberMe: ko.observable(false),
            isErrorSignIn: ko.observable(false),
            submit: submit,
            onFocus: onFocus,
            activate: activate
        };

        return viewmodel;

        function onFocus() {
            viewmodel.isErrorSignIn(false);
        }

        function submit() {
            if (viewmodel.username().length == 0 || viewmodel.password().length == 0) {
                return;
            }
            var data = {
                username: viewmodel.username(),
                password: viewmodel.password(),
                rememberMe: viewmodel.remeberMe()
            };
            httpWrapper.post('api/user/signin', data).then(function (response) {
                if (response.success) {
                    router.reloadLocation();
                } else {
                    viewmodel.isErrorSignIn(true);
                }
            }).fail(function () {
                viewmodel.isErrorSignIn(true);
            });
        }

        function activate() {
            return Q.fcall(function () {

            });
        }
    });