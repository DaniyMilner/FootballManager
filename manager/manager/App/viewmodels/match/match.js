define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            matchId: '',
            activate: activate
        };

        return viewmodel;

        function activate(id) {
            if (id) {
                viewmodel.matchId = id;
                httpWrapper.post('api/match/getmatchresult', { id: viewmodel.matchId }).then(function (response) {
                    if (response.success) {
                        console.log(response);
                    } else {
                        router.navigate('home');
                    }
                    
                }).fail(function (response) {
                    console.log(response);
                });
            }
            return Q.fcall(function () {

            });
        }
    });