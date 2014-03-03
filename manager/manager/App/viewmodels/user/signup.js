define(['plugins/router', 'httpWrapper', 'constants'],
    function (router, httpWrapper, constants) {

        var viewmodel =  {
            username: ko.observable(''),
            email: ko.observable(''),
            password: ko.observable(''),
            confirmPassword: ko.observable(''),
            skype: ko.observable(''),
            birthday: ko.observable(''),
            city: ko.observable(''),
            sex: ko.observable(''),
            aboutmyself: ko.observable(''),
            parentid: ko.observable(''),
            isUserNameExist: isUserNameExist,
            isEmailExist: isEmailExist, 
            loaderForUserName: ko.observable(false),
            loaderForEmail: ko.observable(false),
            activate: activate
        };

        viewmodel.username.isValid = ko.computed(function () {
            return constants.regex.loginRegex.test(viewmodel.username());
        });

        viewmodel.email.isValid = ko.computed(function () {
            return constants.regex.emailRegex.test(viewmodel.email());
        });

        viewmodel.password.isValid = ko.computed(function () {
            return constants.regex.passwordRegex.test(viewmodel.password());
        });

        return viewmodel;

        function isUserNameExist() {
            viewmodel.loaderForUserName(true);
            httpWrapper.post('api/user/usernameexists', { username: viewmodel.username() }).then(function (response) {
                if (response) {
                    viewmodel.loaderForUserName(false);
                }
            });
        }

        function isEmailExist() {
            viewmodel.loaderForEmail(true);
            httpWrapper.post('api/user/emailexists', { email: viewmodel.email() }).then(function() {

            });
        }

        function activate() {
            
        }
    });