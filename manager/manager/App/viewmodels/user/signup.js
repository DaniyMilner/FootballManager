define(['plugins/router', 'httpWrapper', 'constants', 'localization/localizationManager', 'userContext'],
    function (router, httpWrapper, constants, localizationManager, userContext) {

        var viewmodel =  {
            username: ko.observable(''),
            email: ko.observable(''),
            password: ko.observable(''),
            confirmPassword: ko.observable(''),
            skype: ko.observable(''),
            birthday: ko.observable(''),
            city: ko.observable(''),
            sex: ko.observable(true),
            sexs: [{
                value: true,
                text: localizationManager.localize('male')
            },{
                value: false,
                text: localizationManager.localize('female')
            }],
            aboutmyself: ko.observable(''),
            parentid: ko.observable(''),
            isUserNameExist: isUserNameExist,
            isEmailExist: isEmailExist, 
            loaderForUserName: ko.observable(false),
            loaderForEmail: ko.observable(false),
            isValidForm: isValidForm,
            submit: submit,
            activate: activate
        };

        viewmodel.username.isValid = ko.computed(function () {
            if (viewmodel.username().length == 0) {
                return true;
            }
            return constants.regex.loginRegex.test(viewmodel.username());
        });

        viewmodel.username.hasError = ko.observable(false);

        viewmodel.email.isValid = ko.computed(function () {
            if (viewmodel.email().length == 0) {
                return true;
            }
            return constants.regex.emailRegex.test(viewmodel.email());
        });

        viewmodel.email.hasError = ko.observable(false);

        viewmodel.password.isValid = ko.computed(function () {
            if (viewmodel.password().length == 0) {
                return true;
            }
            return constants.regex.passwordRegex.test(viewmodel.password());
        });

        viewmodel.confirmPassword.isValid = ko.computed(function () {
            if (viewmodel.confirmPassword().length == 0) {
                return true;
            }
            return viewmodel.password() == viewmodel.confirmPassword();
        });

        return viewmodel;

        function isUserNameExist() {
            if (!viewmodel.username.isValid() || viewmodel.username().length == 0) {
                return;
            }
            viewmodel.loaderForUserName(true);
            httpWrapper.post('api/user/usernameexists', { username: viewmodel.username() }).then(function (response) {
                if (!response.success) {
                    viewmodel.username.hasError(true);
                }
            }).fin(function () {
                viewmodel.loaderForUserName(false);
            });
        }

        function isEmailExist() {
            if (!viewmodel.email.isValid() || viewmodel.email().length == 0) {
                return;
            }
            viewmodel.loaderForEmail(true);
            httpWrapper.post('api/user/emailexists', { email: viewmodel.email() }).then(function (response) {
                if (!response.success) {
                    viewmodel.email.hasError(true);
                }
            }).fin(function () {
                viewmodel.loaderForEmail(false);
            });
        }

        function isValidForm() {
            return viewmodel.username.isValid() && viewmodel.username().length != 0
                    && viewmodel.email.isValid() && viewmodel.email().length != 0
                    && viewmodel.password.isValid() && viewmodel.password().length != 0
                    && viewmodel.confirmPassword.isValid();
        }

        function submit() {
            if (!viewmodel.isValidForm()) {
                return;
            }
            var data = {
                username: viewmodel.username(),
                email: viewmodel.email(),
                password: viewmodel.password(),
                skype: viewmodel.skype(),
                birthday: viewmodel.birthday(),
                city: viewmodel.city(),
                sex: viewmodel.sex() ? viewmodel.sex() : viewmodel.sex().value,
                aboutmyself: viewmodel.aboutmyself(),
                parentId: viewmodel.parentid()
            };
            httpWrapper.post('api/user/signup', data).then(function (response) {
                if (response) {
                    router.reloadLocation();
                }
            });
        }

        function activate() {
            if (userContext.isAuthenticated) {
                router.navigate('createplayer');
            }
            return Q.fcall(function () {

            });
        }
    });