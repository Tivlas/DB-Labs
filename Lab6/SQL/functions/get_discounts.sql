DROP FUNCTION IF EXISTS get_discounts;

CREATE OR REPLACE FUNCTION get_discounts() RETURNS TABLE (
        discount_id INT,
        description VARCHAR(50),
        name VARCHAR(30),
        percent INT,
        is_active BOOLEAN
    ) AS $$ BEGIN RETURN QUERY
SELECT d.discount_id,
    d.description,
    d.name,
    d.percent,
    d.is_active
FROM discount d;

RETURN;

END;

$$ LANGUAGE plpgsql;