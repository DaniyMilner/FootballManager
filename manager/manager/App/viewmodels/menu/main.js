define(['plugins/router'], function(router) {

    var
        goToHome = function() {
            router.navigate('');
        },
        
        goToNews = function() {
            
        },

        goToRules = function() {
            
        },
        
        goToForum = function() {
            router.navigate('createplayer');
        },

        goToSignUp = function() {
            router.navigate('signup');
        },
        
        activate = function () {

        };

    return {
        goToHome: goToHome,
        goToNews: goToNews,
        goToRules: goToRules,
        goToForum: goToForum,
        goToSignUp: goToSignUp,
        activate: activate
    };

});