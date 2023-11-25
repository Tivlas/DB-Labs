CREATE OR REPLACE PROCEDURE update_category(id INT, new_name VARCHAR(30)) LANGUAGE plpgsql AS $$ BEGIN
UPDATE product_category
SET name = new_name
WHERE product_category_id = id;

END;

$$;