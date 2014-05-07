define([], function () {

    var viewmodel = {
        activate: activate
    };

    function activate(id) {
        return Q.fcall(function () {
            console.log(id);
        });
    }

    return viewmodel;

});