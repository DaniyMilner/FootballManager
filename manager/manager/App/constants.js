define([], function() {

    return {        
        regex: {
            nameRegex: /^[A-Za-zА-Яа-яІіЇї]{2,20}$/,
            emailRegex: /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$/,
            passwordRegex: /^([a-zA-Z0-9~!@#$%^&.,*_+=-]+){7,}$/,
            loginRegex: /^[A-Za-z0-9]{5,20}$/
        }
    };

})