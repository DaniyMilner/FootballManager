define(['plugins/router', 'httpWrapper', 'localization/localizationManager','constants'],
    function (router, httpWrapper, localizationManager, constants) {

    var viewModel = {
        activate: activate,
        name: ko.observable(''),
        surname: ko.observable(''),
        nationality: ko.observable(''),
        create: function () {
            var data = {
                name: this.name(),
                surname: this.surname(),
                countryId: this.selectedCountry().Id,
                positionId: this.selectedPosition().Id,
                weight: this.selectedWeight(),
                growth: this.selectedGrowth()
            };
            httpWrapper.post('api/player/createplayer', data).then(function (response) {
                console.log(response);
            }).fail(function (response) {
                console.log(response);
            });
        },
        isActiveButton: ko.observable(false),
        availableGrowth: [],
        selectedGrowth: ko.observable(),
        availableWeight: [],
        selectedWeight: ko.observable(),
        availablePosition: ko.observableArray([]),
        selectedPosition: ko.observable(),
        availableCountry: ko.observableArray([]),
        selectedCountry: ko.observable(),
        isStartName: ko.observable(false),
        isStartSurname: ko.observable(false)
    };

    viewModel.isValidName = ko.computed(function () {
        if (viewModel.name() == "") {
            viewModel.isStartName(false);
            return false;
        } else if (constants.regex.nameRegex.test(viewModel.name())) {
            viewModel.isStartName(true);
            return true;
        } else {
            viewModel.isStartName(true);
            return false;
        }
    });
    viewModel.isValidSurname = ko.computed(function () {
        if (viewModel.surname() == "") {
            viewModel.isStartSurname(false);
            return false;
        } else if (constants.regex.nameRegex.test(viewModel.surname())) {
            viewModel.isStartSurname(true);
            return true;
        } else {
            viewModel.isStartSurname(true);
            return false;
        }
    });
    viewModel.isActiveButton = ko.computed(function () {
        return viewModel.isValidName() && viewModel.isValidSurname();
    });
    viewModel.isEmptyName = ko.computed(function() {
        return !viewModel.name();
    });
    viewModel.isEmptySurname = ko.computed(function() {
        return !viewModel.surname();
    });
    
    return viewModel;

    function activate() {
        return httpWrapper.post('api/player/getallpositions').then(function (response) {
            response.forEach(function (item) {
                item.Name = localizationManager.localize('position_' + item.PublicId.toLowerCase());
            });
            viewModel.availablePosition(response);

            return httpWrapper.post('api/player/getallcountries').then(function (resp) {
                resp.forEach(function(item) {
                    item.Name = localizationManager.localize('country_' + item.PublicId.toLowerCase());
                });
                viewModel.availableCountry(resp);

                var tempArray = _.range(150, 205, 5);
                tempArray.forEach(function (item) {
                    viewModel.availableGrowth.push(item + ' ' + localizationManager.localize('centimeter'));
                });
                tempArray = _.range(60, 100, 5);
                tempArray.forEach(function (item) {
                    viewModel.availableWeight.push(item + ' ' + localizationManager.localize('kilogram'));
                });
            });
        });
    }

});