define(['httpWrapper', 'basketContext', 'plugins/router'], function (httpWrapper, basketContext, router) {

    var viewmodel = {
        activate: activate,
        hasItemsInBasket: ko.observable(false),
        basket: ko.observableArray([]),
        goToEquipment: goToEquipment,
        sum: ko.observable(null),
        order: order,
        removeFromBasket: removeFromBasket
    };

    return viewmodel;

    function activate() {
        viewmodel.sum(null);
        viewmodel.basket(basketContext());
        viewmodel.hasItemsInBasket(basketContext().length != 0);
        _.each(viewmodel.basket(), function (item) {
            var temp = viewmodel.sum() + item.price;
            viewmodel.sum(temp);
        });
    }

    function removeFromBasket(item) {
        viewmodel.basket(_.without(viewmodel.basket(), item));
        basketContext(_.without(basketContext(), item));
        if (viewmodel.basket().length == 0) {
            viewmodel.hasItemsInBasket(false);
        } else {
            viewmodel.sum(null);
            _.each(viewmodel.basket(), function (item) {
                var temp = viewmodel.sum() + item.price;
                viewmodel.sum(temp);
            });
        }
    }

    function order() {
        var data = {
            orders: _.map(viewmodel.basket(), function(item) {
                return {
                    id: item.id,
                    Price: item.price
                }
            })
        };
        httpWrapper.post('api/order', data).then(function(response) {
            if (response.success) {
                viewmodel.basket([]);
                basketContext([]);
                viewmodel.hasItemsInBasket(false);
            }
        });
    }

    function goToEquipment() {
        router.navigate('equipment');
    }

});