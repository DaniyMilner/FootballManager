ko.bindingHandlers.showHideElement = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var
            $element = $(element),
            value = valueAccessor().value;
        $element.click(function (e) {
            value(!value());
            e.stopPropagation();
        });        
    }
};