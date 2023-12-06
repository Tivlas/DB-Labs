CREATE OR REPLACE PROCEDURE delete_review(p_review_id INT) AS $$ BEGIN
DELETE FROM review
WHERE review_id = p_review_id;

END;

$$ LANGUAGE plpgsql;