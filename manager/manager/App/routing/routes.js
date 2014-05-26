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
            title: 'Player profile'
        }, {
            route: 'equipment',
            moduleId: 'viewmodels/player/equipment',
            title: 'Equipment'
        }, {
            route: 'team(/:id)',
            moduleId: 'viewmodels/team/composition',
            title: 'Composition'
        }, {
            route: 'backoffice',
            moduleId: 'viewmodels/backoffice/backoffice',
            title: 'Backoffice'
        }, {
            route: 'season/:id',
            moduleId: 'viewmodels/season/season',
            title: 'Season'
        }, {
            route: 'tournament/:id',
            moduleId: 'viewmodels/tournament/tournament',
            title: 'Tournament'
        }, {
            route: 'rules',
            moduleId: 'viewmodels/rules/rules',
            title: 'Rules'
        }, {
            route: 'teamsettings',
            moduleId: 'viewmodels/team/teamsettings',
            title: 'Team Settings'
        }, {
            route: 'teams(/:id)',
            moduleId: 'viewmodels/teams/teams',
            title: 'Teams'
        }, {
            route: 'raiting(/:id)',
            moduleId: 'viewmodels/raitings/raiting',
            title: 'Raiting'
        }, {
            route: 'training',
            moduleId: 'viewmodels/player/training',
            title: 'Training'
        }, {
            route: 'shop/:id',
            moduleId: 'viewmodels/player/shop',
            title: 'Shop'
        }, {
            route: 'basket',
            moduleId: 'viewmodels/player/basket',
            title: 'Basket'
        }, {
            route: 'coach',
            moduleId: 'viewmodels/player/coachRoom',
            title: 'Coach room'
        }, {
            route: 'city',
            moduleId: 'viewmodels/player/city',
            title: 'City'
        }];

    });