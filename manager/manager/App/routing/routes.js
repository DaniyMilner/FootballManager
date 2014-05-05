define([],
    function() {

        return [{            
            route: ['', 'home'],
            moduleId: 'viewmodels/home/home',
            title: 'Home page'
        }, {
            route: 'signup',
            moduleId: 'viewmodels/user/signup',
            title: 'Signup'
        }, {
            route: 'createplayer',
            moduleId: 'viewmodels/player/createplayer',
            title: 'Create player'
        }, {
            route: 'generator',
            moduleId: 'viewmodels/generator/generator',
            title: 'Generator'
        }, {
            route: 'userprofile/:id',
            moduleId: 'viewmodels/user/profile',
            title: 'User profile'
        }, {
            route: 'match/:id',
            moduleId: 'viewmodels/match/match',
            title: 'Match'
        }, {
            route: 'playerprofile(/:id)',
            moduleId: 'viewmodels/player/profile',
            title: 'Match'
        }, {
            route: 'backoffice',
            moduleId: 'viewmodels/backoffice/backoffice',
            title: 'Backoffice'
        }];

    });