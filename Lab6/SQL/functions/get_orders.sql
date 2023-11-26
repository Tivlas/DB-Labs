DROP FUNCTION IF EXISTS get_orders;

CREATE OR REPLACE FUNCTION get_orders() RETURNS TABLE (
        order_id INT,
        client_id INT,
        "date" TIMESTAMP,
        price NUMERIC(7, 2),
        first_name VARCHAR(30),
        last_name VARCHAR(30),
        phone CHAR(13),
        email VARCHAR(30),
        PASSWORD varchar,
        product_id INT,
        quantity INT,
        product_price NUMERIC(7, 2),
        name VARCHAR(30),
        product_category_id INT,
        product_category_name VARCHAR(30),
        brand VARCHAR(30),
        description TEXT
    ) AS $$ BEGIN RETURN QUERY
SELECT o.order_id,
    o.client_id,
    o.date,
    o.price,
    c.first_name,
    c.last_name,
    c.phone,
    c.email,
    c.password,
    oi.product_id,
    oi.product_quantity,
    oi.product_price,
    p.name,
    p.product_category_id,
    pc.name,
    p.brand,
    p.description
FROM "order" o
    JOIN "client" c ON o.client_id = c.client_id
    JOIN "order_item" oi ON o.order_id = oi.order_id
    JOIN "product" p ON oi.product_id = p.product_id
    JOIN "product_category" pc ON p.product_category_id = pc.product_category_id;

END;

$$ LANGUAGE plpgsql;