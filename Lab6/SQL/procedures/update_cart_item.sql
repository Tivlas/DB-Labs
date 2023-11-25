CREATE OR REPLACE PROCEDURE update_cart_item_product_quantity(
        p_cart_item_id INT,
        p_product_quantity INT
    ) AS $$ BEGIN
UPDATE cart_item
SET product_quantity = p_product_quantity
WHERE cart_item_id = p_cart_item_id;

END;

$$ LANGUAGE plpgsql;