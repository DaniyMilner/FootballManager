define(['plugins/router', 'userContext'], function (router, userContext) {

    var
        goToHome = function() {
            router.navigate('');
        },
        
        goToNews = function() {
            
        },

        goToRules = function() {
            router.navigate('generator');
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
        isAuthenticated: userContext.isAuthenticated,
        goToHome: goToHome,
        goToNews: goToNews,
        goToRules: goToRules,
        goToForum: goToForum,
        goToSignUp: goToSignUp,
        activate: activate
    };

});