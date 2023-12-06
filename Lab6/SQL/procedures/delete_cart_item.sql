CREATE OR REPLACE PROCEDURE delete_cart_item_by_id(p_cart_item_id INT) AS $$ BEGIN
DELETE FROM cart_item
WHERE cart_item_id = p_cart_item_id;

END;

$$ LANGUAGE plpgsql;