define(['httpWrapper'], function (httpWrapper) {

    var viewmodel = {
        activate: activate,
        isNotHavePlayer: ko.observable(false)
    };

    return viewmodel;

    function activate(id) {
        if (_.isNull(id) || _.isUndefined(id)) {
            viewmodel.isNotHavePlayer(true);
        }
    }

});