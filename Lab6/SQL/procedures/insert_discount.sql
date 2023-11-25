CREATE OR REPLACE PROCEDURE insert_discount(
        p_description VARCHAR(50),
        p_name VARCHAR(30),
        p_percent INT,
        p_is_active BOOLEAN
    ) LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO discount (description, name, percent, is_active)
VALUES (p_description, p_name, p_percent, p_is_active);

END;

$$;