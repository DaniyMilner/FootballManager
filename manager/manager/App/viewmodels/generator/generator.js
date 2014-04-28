define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            done: ko.observable(''),
            time: ko.observable(''),
            run: function () {
                viewmodel.done('start');
                httpWrapper.post('api/generator/run').then(function (response) {
                    if (response.success) {
                        viewmodel.done('success');
                        viewmodel.time(response.data.date);
                    }
                    console.log(response.data);
                }).fail(function (response) {
                    console.log(response.data);
                });
            },
            activate: activate
        };

        return viewmodel;
        
        function activate() {
            return Q.fcall(function () {
                
            });
        }
    });