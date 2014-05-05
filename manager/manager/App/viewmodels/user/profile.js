define(['httpWrapper', 'userContext', 'plugins/router'], function (httpWrapper, userContext, router) {

    var viewmodel = {
        user: ko.observable({}),
        isEditMode: ko.observable(false),
        isCurrentUser: ko.observable(false),
        changeEditMode: changeEditMode,
        saveChanges: saveChanges,
        activate: activate,

        /*Change profile section*/
        password: ko.observable(''),
        confirmPassword: ko.observable(''),
        birthday: ko.observable(''),
        sex: ko.observable(''),
        skype: ko.observable(''),
        aboutmyself: ko.observable(''),
        city: ko.observable('')
    };

    return viewmodel;

    function changeEditMode() {
        if (!viewmodel.isEditMode() && viewmodel.isCurrentUser()) {
            viewmodel.birthday(viewmodel.user().birthday);
            viewmodel.sex(viewmodel.user().sex);
            viewmodel.skype(viewmodel.user().skype);
            viewmodel.aboutmyself(viewmodel.user().aboutmyself);
            viewmodel.city(viewmodel.user().city);
        }
        viewmodel.isEditMode(!viewmodel.isEditMode());
    }

    function saveChanges() {
        //httpWrapper.post('api/user/updateuserprofile');
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