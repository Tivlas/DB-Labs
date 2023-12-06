CREATE OR REPLACE FUNCTION get_reviews() RETURNS TABLE (
        review_id INT,
        product_id INT,
        client_id INT,
        content TEXT,
        rating INT,
        date TIMESTAMP,
        client_first_name VARCHAR(30),
        client_last_name VARCHAR(30),
        client_phone CHAR(13),
        client_email VARCHAR(30),
        product_name VARCHAR(30),
        product_price NUMERIC(7, 2),
        product_category_name VARCHAR(30)
    ) AS $$ BEGIN RETURN QUERY
SELECT r.review_id,
    r.product_id,
    r.client_id,
    r.content,
    r.rating,
    r.date,
    c.first_name AS client_first_name,
    c.last_name AS client_last_name,
    c.phone AS client_phone,
    c.email AS client_email,
    p.name AS product_name,
    p.price AS product_price,
    pc.name AS product_category_name
FROM review r
    INNER JOIN client c ON r.client_id = c.client_id
    INNER JOIN product p ON r.product_id = p.product_id
    INNER JOIN product_category pc ON p.product_category_id = pc.product_category_id;

RETURN;

END;

$$ LANGUAGE plpgsql;