CREATE OR REPLACE PROCEDURE insert_category(
    p_name VARCHAR(30)
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO product_category (name)
    VALUES (p_name);
END;
$$;