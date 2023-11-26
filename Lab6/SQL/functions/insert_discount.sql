CREATE OR REPLACE FUNCTION insert_discount(
        p_description VARCHAR(50),
        p_name VARCHAR(30),
        p_percent INT,
        p_is_active BOOLEAN
    ) RETURNS INT AS $$
DECLARE r_discount_id INT;

BEGIN
INSERT INTO discount (description, name, percent, is_active)
VALUES (p_description, p_name, p_percent, p_is_active)
RETURNING discount_id INTO r_discount_id;

RETURN r_discount_id;

END;

$$ LANGUAGE plpgsql;