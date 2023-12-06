CREATE OR REPLACE PROCEDURE update_discount(
        p_discount_id INT,
        p_description VARCHAR(50),
        p_name VARCHAR(30),
        p_percent INT,
        p_is_active BOOLEAN
    ) LANGUAGE plpgsql AS $$ BEGIN
UPDATE discount
SET description = p_description,
    name = p_name,
    percent = p_percent,
    is_active = p_is_active
WHERE discount_id = p_discount_id;

END;

$$;