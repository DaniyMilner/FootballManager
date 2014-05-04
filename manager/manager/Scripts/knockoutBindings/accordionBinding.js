ko.bindingHandlers.accordion = {
    cssClasses: {
        item: 'accordion-item',
        conteiner: 'accordion-container'
    },
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $element = $(element),
            cssClasses = ko.bindingHandlers.accordion.cssClasses;

        var containers = $element.find(cssClasses.conteiner);
        var headers = $element.find('h3');

        $(headers).on('click', function () {
            $(this).toggleClass('active');
            $(this).next().toggle();
        });

    },
    update: function (element, valueAccessor) {
    }
};