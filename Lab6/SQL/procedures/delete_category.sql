CREATE OR REPLACE PROCEDURE delete_category_by_id(category_id INT) LANGUAGE plpgsql AS $$ BEGIN
DELETE FROM product_category
WHERE product_category_id = category_id;

END;

$$;