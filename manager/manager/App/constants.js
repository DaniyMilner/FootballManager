define([], function () {

    return {
        regex: {
            nameRegex: /^[A-Za-zА-Яа-яІіЇї]{2,20}$/,
            emailRegex: /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$/,
            passwordRegex: /^([a-zA-Z0-9~!@#$%^&.,*_+=-]+){7,}$/,
            loginRegex: /^[A-Za-z0-9]{5,20}$/
        },

        positions: {
            FW: 'FW',
            DEF: 'DEF',
            GK: 'GK',
            MID: 'MID'
        },

        skills: {
            1: {
                abbr: 'Cк.',
                name: 'Скорость'
            },
            2: {
                abbr: 'ИГ',
                name: 'Игра головой'
            },
            3: {
                abbr: 'СУ',
                name: 'Сила удара'
            },
            4: {
                abbr: 'Пас',
                name: 'Пас'
            },
            5: {
                abbr: 'ТУ',
                name: 'Точность удара'
            },
            6: {
                abbr: 'Отбор',
                name: 'Отбор'
            },
            7: {
                abbr: 'Др.',
                name: 'Дриблинг'
            },
            8: {
                abbr: 'Вл.м.',
                name: 'Владение мячом'
            },
            9: {
                abbr: 'Реакц.',
                name: 'Реакция'
            },
            10: {
                abbr: 'ИвВ',
                name: 'Игра в воздухе'
            },
            11: {
                abbr: 'Пр.',
                name: 'Прыжок'
            },
            12: {
                abbr: 'ВП',
                name: 'Выбор позиции'
            },
            13: {
                abbr: 'Вын.',
                name: 'Выносливость'
            },
            14: {
                abbr: 'Лидерство',
                name: 'Лид.'
            },
        }
    };

})