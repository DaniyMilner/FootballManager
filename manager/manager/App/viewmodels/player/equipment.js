define(['plugins/router', 'userContext', 'constants', 'basketContext'],
    function (router, userContext, constants, basketContext) {

    var viewmodel = {
        activate: activate,
        userName: userContext.user.username,
        goToGlovesShop: goToGlovesShop,
        goToBootsShop: goToBootsShop,
        goToShieldsShop: goToShieldsShop
    };

    return viewmodel;

    function activate() {
        
    }

    function goToGlovesShop() {
        router.navigate('shop/' + constants.shop.gloves);
    }

    function goToBootsShop() {
        router.navigate('shop/' + constants.shop.boots);
    }

    function goToShieldsShop() {
        router.navigate('shop/' + constants.shop.shield);
    }

});