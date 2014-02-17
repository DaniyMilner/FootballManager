define([], function () {

    function UserModel(spec) {
        this.username = spec.username;
        this.email = spec.email;
        this.skype = spec.skype;
        this.birthday = spec.birthday;
        this.city = spec.city;
        this.sex = spec.sex;
        this.aboutmyself = spec.aboutmyself;
        this.parentid = spec.parentid;
    }

    return UserModel;

})