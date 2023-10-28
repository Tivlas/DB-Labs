CREATE OR REPLACE PROCEDURE update_cart_price_delete() LANGUAGE plpgsql AS $$ BEGIN
UPDATE cart
SET price = price - OLD.product_price * OLD.product_quantity
WHERE cart_id = OLD.cart_id;

END;

$$;

-----------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE update_cart_price_insert() LANGUAGE plpgsql AS $$ BEGIN
UPDATE cart
SET price = price + NEW.product_price * NEW.product_quantity
WHERE cart_id = NEW.cart_id;

END;

$$;

-----------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE create_cart_for_new_client() LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO cart (client_id, price)
VALUES (NEW.client_id, 0.0);

END;

$$;

-----------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE copy_cart_items_to_order_items() LANGUAGE plpgsql AS $$
DECLARE cur_cart_id INT;

BEGIN
SELECT cart_id INTO cur_cart_id
FROM cart
WHERE client_id = NEW.client_id;

INSERT INTO order_item (
    order_id,
    product_id,
    product_quantity,
    product_price
  )
SELECT order_id,
  product_id,
  product_quantity,
  product_price
FROM cart_item
WHERE cart_id = cur_cart_id;

DELETE FROM cart_item
WHERE cart_id = cur_cart_id;

UPDATE cart
SET price = 0
WHERE client_id = NEW.client_id;

END;

$$;

-----------------------------------------------------------------------------------