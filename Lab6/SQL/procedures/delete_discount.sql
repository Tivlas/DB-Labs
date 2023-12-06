CREATE OR REPLACE PROCEDURE delete_discount_by_id(p_discount_id INT) AS $$ BEGIN
DELETE FROM discount
WHERE discount_id = p_discount_id;

END;

$$ LANGUAGE plpgsql;