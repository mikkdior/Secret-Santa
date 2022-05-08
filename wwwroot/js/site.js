document.addEventListener('DOMContentLoaded', function () {

    //ajax. регистрация ajax запроса на кнопки добавления/удаление сотрудника
    document.querySelectorAll('.santa-buttons__button').forEach((main) => {
        let req = new XMLHttpRequest();

        // реагистрация обработчика после отправки
        req.onload = () => {
            setNewTickets(req)
        };

        // Привязываем отправку action
        main.addEventListener('click', () => {
            var action = main.getAttribute('acttype');
            req.open('post', `/update/${action}`);
            req.send();
        });
    });

    // принимаем результат запроса и назначаем новый список билетов
    function setNewTickets(req) {
        if (req.status != 200) return;
        if (req.response == undefined) return;

        let santa_tickets = document.querySelector('.santa-tickets');
        let parsed_tickets = JSON.parse(req.response);
        let new_tickets = getTickets(parsed_tickets);
        santa_tickets.innerHTML = new_tickets.innerHTML;
    }

    // собираем новый список билетов
    function getTickets(json_list) {
        let tickets = document.createElement('ul');
        tickets.className = 'santa-tickets';

        var template = document.querySelector('.santa-tickets__ticket-template');

        for (var i = 0; i < json_list.length; i++){

            let sketch = template.cloneNode(true);
            sketch.className = '';

            var emp_from_name = sketch.querySelector('.santa-ticket__emp-from-name');
            var gift_name = sketch.querySelector('.santa-ticket__gift-name');
            var emp_to_name = sketch.querySelector('.santa-ticket__emp-to-name');

            emp_from_name.innerHTML = json_list[i].empFrom.firstName + '&#32;' + json_list[i].empFrom.lastName;
            gift_name.innerHTML = json_list[i].giftName;
            emp_to_name.innerHTML = json_list[i].empTo.firstName + '&#32;' + json_list[i].empTo.lastName;

            tickets.appendChild(sketch);
        }

        tickets.appendChild(template);
        var tickets_count = document.querySelector('.tickets-count .tickets-count__count');
        tickets_count.innerHTML = json_list.length;

        return tickets;
    }
});