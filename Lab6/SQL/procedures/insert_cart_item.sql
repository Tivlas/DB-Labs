CREATE OR REPLACE PROCEDURE insert_cart_item(
        p_cart_id INT,
        p_product_id INT,
        p_product_quantity INT,
        p_product_price NUMERIC(7, 2)
    ) AS $$ BEGIN
INSERT INTO cart_item (
        cart_id,
        product_id,
        product_quantity,
        product_price
    )
VALUES (
        p_cart_id,
        p_product_id,
        p_product_quantity,
        p_product_price
    );

END;

$$ LANGUAGE plpgsql;