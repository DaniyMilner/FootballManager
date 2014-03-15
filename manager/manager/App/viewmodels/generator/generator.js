define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            run: function () {
                httpWrapper.post('api/generator/run').then(function (response) {
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