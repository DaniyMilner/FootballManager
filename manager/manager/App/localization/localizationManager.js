define(['localization/resources'],
    function (resources) {
        "use strict";

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
        };
    });