CREATE OR REPLACE FUNCTION get_discount_by_id(p_discount_id INT) RETURNS TABLE (
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
FROM discount d
WHERE d.discount_id = p_discount_id;

RETURN;

END;

$$ LANGUAGE plpgsql;