CREATE OR REPLACE FUNCTION get_cart_items_by_cart_id(p_cart_id INT) RETURNS TABLE (
        cart_item_id INT,
        cart_id INT,
        product_id INT,
        product_quantity INT,
        product_price NUMERIC(7, 2),
        client_id INT,
        price NUMERIC(7, 2),
        product_category_id INT,
        name VARCHAR(30),
        production_date DATE,
        quantity INT,
        brand VARCHAR(30),
        description TEXT
    ) AS $$ BEGIN RETURN QUERY
SELECT ci.cart_item_id,
    ci.cart_id,
    ci.product_id,
    ci.product_quantity,
    ci.product_price,
    c.client_id,
    c.price,
    p.product_category_id,
    p.name,
    p.production_date,
    p.quantity,
    p.brand,
    p.description
FROM cart_item ci
    JOIN cart c ON ci.cart_id = c.cart_id
    JOIN product p ON ci.product_id = p.product_id
WHERE ci.cart_id = p_cart_id;

END;

$$ LANGUAGE plpgsql;