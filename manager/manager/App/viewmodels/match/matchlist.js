define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            todayMatches: ko.observableArray([]),
            notTodayMatches: ko.observableArray([]),
            activate: activate,
            getMatches: function() {
                return viewmodel.getTodayMatches(viewmodel.getNotTodayMatches);
            },
            getTodayMatches: function(callback) {
                return httpWrapper.post('api/generator/getTodayMatches').then(function (response) {
                    if (response.success) {
                        if (typeof response.data == 'object') {
                            viewmodel.todayMatches([]);
                            for (var i = 0; i < response.data.length; i++) {
                                viewmodel.todayMatches.push(viewmodel.getNextMatchResponse(response.data[i]));
                            }
                        }
                    }
                    if (callback) {
                        callback();
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            },
            getNotTodayMatches: function() {
                return httpWrapper.post('api/generator/getNotTodayMatches').then(function (response) {
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
            return viewmodel.getMatches();
        }

        return viewmodel;

    });