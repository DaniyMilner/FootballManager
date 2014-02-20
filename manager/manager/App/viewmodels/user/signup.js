define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var
            username = ko.observable(''),
            email = ko.observable(''),
            skype = ko.observable(''),
            birthday = ko.observable(''),
            city = ko.observable(''),
            sex = ko.observable(''),
            aboutmyself = ko.observable(''),
            parentid = '',
            activate = function () {
                
            };

        return {
            username: username,
            email: email,
            skype: skype,
            birthday: birthday,
            city: city,
            sex: sex,
            aboutmyself: aboutmyself,
            parentid: parentid,
            activate: activate
        };

    });