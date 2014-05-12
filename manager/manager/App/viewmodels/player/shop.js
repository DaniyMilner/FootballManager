define(['httpWrapper', 'constants', 'plugins/router', 'basketContext'],
    function (httpWrapper, constants, router, basketContext) {

        var viewmodel = {
            activate: activate,
            shopItems: ko.observableArray([]),
            addToBasket: addToBasket,
            typeName: ko.observable(''),
            goToBasket: goToBasket
        };

        return viewmodel;

        function activate(id) {
            if (_.isNull(id) || _.isUndefined(id)) {
                router.navigate('home');
                return;
            }
            viewmodel.shopItems([]);
            viewmodel.typeName(getTypeName(id));
            httpWrapper.post('api/equipment/getbytype', { type: id }).then(function (response) {
                if (response.success) {
                    parseData(response.data);
                }
            });
        }

        function goToBasket() {
            router.navigate('basket');
        }

        function parseData(data) {
            viewmodel.shopItems(_.chain(data).map(function (item) {
                return {
                    id: item.id,
                    name: item.name,
                    price: item.price,
                    countOfMatch: item.countOfMatch,
                    amountOfSkills: item.amountOfSkills,
                    type: item.type,
                    weatherType: item.weatherType,
                    image: getImageUrl(item.type, item.index)
                }
            }).value());
        }

        function getImageUrl(type, index) {
            switch (type) {
                case constants.shop.gloves:
                    return 'Content/img/equipment/gloves_' + index + '.png';
                case constants.shop.boots:
                    return 'Content/img/equipment/boots_' + index + '.png';
                case constants.shop.shield:
                    return 'Content/img/equipment/shield_' + index + '.png';
            }
        }

        function getTypeName(type) {
            var t = parseInt(type);
            switch (t) {
                case constants.shop.gloves:
                    return 'Перчатки';
                case constants.shop.boots:
                    return 'Бутсы';
                case constants.shop.shield:
                    return 'Щитки';
            }
        }

        function addToBasket(item) {
            basketContext.push(item);
        }

    });