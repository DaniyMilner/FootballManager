define(['localization/resources', 'httpWrapper'],
    function (resources, httpWrapper) {
        "use strict";

        var
            defaultCulture = 'ru',
            supportedCultures = [
                'en', 'ru', 'uk'
            ],
            currentLanguage = defaultCulture,

            localize = function (key, culture) {
                var item = resources[key];
                if (_.isEmpty(item)) {
                    throw 'A resource with key ' + key + ' was not found';
                }
                var cultureInfo = this.currentLanguage;
                return item[cultureInfo] || item[defaultCulture];
            },

            hasKey = function (key) {
                return resources.hasOwnProperty(key);
            },

            initialize = function () {
                var that = this;
                return httpWrapper.post('api/user/getuserlanguage').then(function (response) {
                    if ((!_.isNull(response)) && (!_.isNull(response.Language))) {
                        that.currentLanguage = response.Language;
                    }
                });
            };


        (function () {
            ko.bindingHandlers.localize = {
                init: function (element, valueAccessor) {
                    localizeValue(element, valueAccessor);
                },
                update: function (element, valueAccessor) {
                    localizeValue(element, valueAccessor);
                }
            };

            function localizeValue(element, valueAccessor) {
                var value = valueAccessor();

                if (_.isEmpty(value)) {
                    return;
                }

                var localizationManager = require("localization/localizationManager");

                if (_.isString(value['text'])) {
                    $(element).text(localizationManager.localize(value['text']));
                }
                if (_.isString(value['placeholder'])) {
                    $(element).prop('placeholder', localizationManager.localize(value['placeholder']));
                }
                if (_.isString(value['value'])) {
                    $(element).prop('value', localizationManager.localize(value['value']));
                }
                if (_.isString(value['title'])) {
                    $(element).prop('title', localizationManager.localize(value['title']));
                }
                if (_.isString(value['html'])) {
                    $(element).html(localizationManager.localize(value['html']));
                }
            };
        })();

        return {
            initialize: initialize,
            currentLanguage: currentLanguage,
            localize: localize,
            defaultCulture: defaultCulture,
            supportedCultures: supportedCultures,
            hasKey: hasKey
        };
    });