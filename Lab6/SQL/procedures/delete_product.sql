DROP PROCEDURE IF EXISTS delete_product_by_id;

CREATE OR REPLACE PROCEDURE delete_product_by_id(p_product_id INT) LANGUAGE plpgsql AS $$ BEGIN
DELETE FROM product
WHERE product_id = p_product_id;

END;

$$;