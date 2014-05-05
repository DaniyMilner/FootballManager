define(['plugins/router', 'httpWrapper'],
    function (router, httpWrapper) {

        var viewmodel = {
            menu: {
                activeItemIndex: ko.observable(0),
                setActiveIndex: function (i) {
                    viewmodel.menu.activeItemIndex(i);
                }
            },
            generator: {
                done: ko.observable(''),
                time: ko.observable(''),
                todayMatches: ko.observableArray([]),
                getTodayMatches: function () {
                    httpWrapper.post('api/generator/getTodayMatches').then(function (response) {
                        if (response.success) {
                            if (typeof response.data == 'object') {
                                viewmodel.generator.todayMatches([]);
                                for (var i = 0; i < response.data.length; i++) {
                                    viewmodel.generator.todayMatches.push({
                                        id: response.data[i].id,
                                        homeName: response.data[i].homeName,
                                        guestName: response.data[i].guestName,
                                        dateStart: new Date(parseInt(response.data[i].dateStart.replace('/Date(', '').replace(')/', ''))).toDateString(),
                                        isGenerated: response.data[i].isGenerated
                                    });
                                }
                            }
                        }
                    }).fail(function (response) {
                        console.log(response.data);
                    });
                },
                run: function () {
                    viewmodel.generator.done('start');
                    viewmodel.generator.time('');
                    httpWrapper.post('api/generator/run').then(function (response) {
                        if (response.success) {
                            viewmodel.generator.done('success');
                            viewmodel.generator.time(response.data.date);
                            viewmodel.generator.getTodayMatches();
                        }
                    }).fail(function (response) {
                        console.log(response.data);
                    });
                }
            },
            activate: activate
        };

        return viewmodel;

        function activate() {
            return Q.fcall(function () {
                viewmodel.generator.getTodayMatches();
            });
        }
    });