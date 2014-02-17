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