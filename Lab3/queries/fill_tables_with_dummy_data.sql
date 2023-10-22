INSERT INTO product_category(name)
VALUES ('Планшеты'),
    ('Смартфоны'),
    ('Оптические приборы');

INSERT INTO product(
        product_category_id,
        price,
        name,
        production_date,
        quantity,
        brand,
        description
    )
VALUES (
        2,
        2799.00,
        'Xiaomi 13T pro',
        '2023-01-01',
        123,
        'Xiaomi',
        'Android, экран 6.67 AMOLED (1220x2712) 144 Гц, Mediatek Dimensity 9200+, ОЗУ 12 ГБ, память 512 ГБ, камера 50 Мп, аккумулятор 5000 мАч, 2 SIM (nano-SIM/eSIM), влагозащита IP68'
    ),
    (
        1,
        2699.00,
        'iPhone 14',
        '2022-01-04',
        153,
        'Apple',
        'Apple iOS, экран 6.1 OLED (1170x2532) 60 Гц, Apple A15 Bionic, ОЗУ 6 ГБ, память 128 ГБ, камера 12 Мп, 1 SIM (nano-SIM/eSIM), влагозащита IP68'
    ),
    (
        3,
        1891.60,
        'Sky-Watcher',
        '2020-05-06',
        18,
        'Sky-Wathcer',
        'рефлектор Ньютона, диаметр: 130 мм, фокусное расстояние: 900 мм, относительное отверстие: 1/6.9, экваториальная (EQ) монтировка, окуляры 10 мм/25 мм'
    );

INSERT INTO client (first_name, last_name, phone, PASSWORD, email)
VALUES (
        'Ваня',
        'Огуречин',
        '+375291234567',
        'password1',
        'email1@gmail.com'
    ),
    (
        'Петр',
        'Грозный',
        '+375291234567',
        'password3',
        'email2@gmail.com'
    ),
    (
        'Дэн',
        'Фомичевский',
        '+375291234567',
        'password3',
        'email3@gmail.com'
    );

INSERT INTO employee_role(name)
VALUES ('Менеджер продуктов'),
    ('Менеджер скидок'),
    ('Менеджер пользователей');

INSERT INTO employee (
        employee_role_id,
        first_name,
        last_name,
        salary,
        phone,
        position,
        PASSWORD,
        email
    )
VALUES (
        1,
        'Ваня',
        'Огуречин',
        5314.01,
        '+375291234567',
        'Обычный сотрудник',
        'password1',
        'email1@gmail.com'
    ),
    (
        2,
        'Петр',
        'Грозный',
        141.43,
        '+375291234567',
        'Особый сотрудник',
        'password3',
        'email2@gmail.com'
    ),
    (
        3,
        'Дэн',
        'Фомичевский',
        123.12,
        '+375291234567',
        'Временный сотрудник',
        'password3',
        'email3@gmail.com'
    );

INSERT INTO cart(client_id, price)
VALUES (1, 0),
    (2, 0),
    (3, 0);

INSERT INTO review(product_id, client_id, content, rating, "date")
VALUES (
        1,
        1,
        'Отличный телефон',
        4,
        '2023-10-17 15:00:00'
    ),
    (
        1,
        2,
        'Такой себе телефон',
        2,
        '2023-10-17 14:00:23'
    ),
    (3, 3, 'Видно Луну', 5, '2023-10-17 13:00:00');

INSERT INTO discount (description, name, percent, is_active)
VALUES ('10% на телефон', 'phone-10', 10, TRUE),
    ('20% на телеском', 'sky-watcher-20', 20, TRUE);

INSERT INTO product_discount (product_id, discount_id)
VALUES (1, 1),
    (3, 2);

INSERT INTO "order" (client_id, "date", price)
VALUES (1, '2023-10-17 10:00:00', 5598.00),
    (2, '2023-10-17 11:30:00', 8097.00),
    (2, '2023-10-17 11:36:00', 8097.00),
    (3, '2023-10-17 15:45:00', 1891.60);

INSERT INTO order_item (
        order_id,
        product_id,
        product_quantity,
        product_price
    )
VALUES (1, 1, 2, 2799.00),
    (2, 2, 3, 2699.00),
    (3, 2, 3, 2699.00),
    (4, 3, 1, 1891.60);