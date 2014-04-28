define(['httpWrapper', 'userContext', 'plugins/router'], function (httpWrapper, userContext, router) {

    var viewmodel = {
        user: ko.observable({}),
        isEditMode: ko.observable(false),
        isCurrentUser: ko.observable(false),
        changeEditMode: changeEditMode, 
        activate: activate
    };

    return viewmodel;

    function changeEditMode() {
        viewmodel.isEditMode(!viewmodel.isEditMode());
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