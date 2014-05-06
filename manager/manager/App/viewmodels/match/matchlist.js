define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            todayMatches: ko.observableArray([]),
            notTodayMatches: ko.observableArray([]),
            activate: activate,
            getTodayMatches: function() {
                httpWrapper.post('api/generator/getTodayMatches').then(function (response) {
                    if (response.success) {
                        if (typeof response.data == 'object') {
                            viewmodel.todayMatches([]);
                            for (var i = 0; i < response.data.length; i++) {
                                viewmodel.todayMatches.push(viewmodel.getNextMatchResponse(response.data[i]));
                            }
                        }
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            },
            getNotTodayMatches: function() {
                httpWrapper.post('api/generator/getNotTodayMatches').then(function (response) {
                    if (response.success) {
                        if (typeof response.data == 'object') {
                            viewmodel.notTodayMatches([]);
                            for (var i = 0; i < response.data.length; i++) {
                                viewmodel.notTodayMatches.push(viewmodel.getNextMatchResponse(response.data[i]));
                            }
                        }
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            },
            getNextMatchResponse: function (data) {
                return new Object({
                    id: data.id,
                    homeName: data.homeName,
                    guestName: data.guestName,
                    dateStart: new Date(parseInt(data.dateStart.replace('/Date(', '').replace(')/', ''))).toLocaleDateString(),
                    isGenerated: data.isGenerated,
                    homeGoal: data.homeGoal,
                    guestGoal: data.guestGoal,
                    publicId: data.publicId,
                    homeShortName: data.homeShortName,
                    guestShortName: data.guestShortName
                });
            }
        };

        function activate() {
            return Q.fcall(function () {
                viewmodel.getTodayMatches();
                viewmodel.getNotTodayMatches();
            });
        }

        return viewmodel;

    });