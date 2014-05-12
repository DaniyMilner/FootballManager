define(['httpWrapper', 'userContext', 'plugins/router', 'constants'],
    function (httpWrapper, userContext, router, constants) {

    var viewmodel = {
        user: ko.observable({}),
        isEditMode: ko.observable(false),
        isCurrentUser: ko.observable(false),
        changeEditMode: changeEditMode,
        saveChanges: saveChanges,
        activate: activate,

        /*Change profile section*/
        currentPassword: ko.observable(''),
        password: ko.observable(''),
        confirmPassword: ko.observable(''),
        birthday: ko.observable(''),
        sex: ko.observable(false),
        skype: ko.observable(''),
        aboutmyself: ko.observable(''),
        city: ko.observable(''),
        changePassword: changePassword,
        sexs: [{
            value: true,
            text: 'Мужской'
        }, {
            value: false,
            text: 'Женский'
        }],
        incorrectCurrentPassword: ko.observable(false),
        incorrectConfirmPassword: ko.observable(false),
        incorrectPassword: ko.observable(false)
    };

    return viewmodel;

    function changePassword() {
        debugger;
        if (viewmodel.password().length != 0 && viewmodel.confirmPassword().length != 0) {
            viewmodel.incorrectPassword(!constants.regex.passwordRegex.test(viewmodel.password()));
            viewmodel.incorrectConfirmPassword(viewmodel.password() != viewmodel.confirmPassword());
            if (viewmodel.incorrectPassword() || viewmodel.incorrectConfirmPassword()) {
                return;
            }
        }
        httpWrapper.post('api/user/changePassword', {
            currentPassword: viewmodel.currentPassword(),
            newPassword: viewmodel.password()
        }).then(function(response) {
            if (response.success) {
                viewmodel.changeEditMode();
            } else {
                viewmodel.incorrectCurrentPassword(true);
            }
        });
    }

    function changeEditMode() {
        if (!viewmodel.isEditMode() && viewmodel.isCurrentUser()) {
            viewmodel.birthday(viewmodel.user().birthday);
            viewmodel.sex(viewmodel.user().sex);
            viewmodel.skype(viewmodel.user().skype);
            viewmodel.aboutmyself(viewmodel.user().aboutmyself);
            viewmodel.city(viewmodel.user().city);
        } else {
            viewmodel.user().birthday = viewmodel.birthday();
            viewmodel.user().sex = viewmodel.sex().value;
            viewmodel.user().aboutmyself = viewmodel.aboutmyself();
            viewmodel.user().skype = viewmodel.skype();
            viewmodel.user().city = viewmodel.city();
        }
        viewmodel.isEditMode(!viewmodel.isEditMode());
    }

    function saveChanges() {
        httpWrapper.post('api/user/updateuserprofile', {
            Skype: viewmodel.skype(),
            Birthday: viewmodel.birthday(),
            City: viewmodel.city(),
            Sex: viewmodel.sex().value,
            AboutMySelf: viewmodel.aboutmyself()
        }).then(function(response) {
            if (response.success) {
                viewmodel.changeEditMode();
            }
        });
    }

    function activate(publicId) {
        if (_.isUndefined(publicId) || _.isNull(publicId)) {
            return;
        }
        if (publicId == userContext.user.publicId) {
            viewmodel.isCurrentUser(true);
            viewmodel.user(userContext.user);
        } else {
            httpWrapper.post('api/user/getuserinfobypublicId', publicId).then(function(response) {
                if (response && response.User) {
                    viewmodel.isCurrentUser(false);
                    viewmodel.user(response.User);
                }
            });
        }
    }

});