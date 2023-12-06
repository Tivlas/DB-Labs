CREATE OR REPLACE PROCEDURE update_review(
        p_review_id INT,
        p_content TEXT,
        p_rating INT
    ) AS $$ BEGIN
UPDATE review
SET content = p_content,
    rating = p_rating
WHERE review_id = p_review_id;

END;

$$ LANGUAGE plpgsql;