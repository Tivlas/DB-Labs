CREATE OR REPLACE PROCEDURE insert_review(
        p_product_id INT,
        p_client_id INT,
        p_content TEXT,
        p_rating INT
    ) AS $$ BEGIN
INSERT INTO review (product_id, client_id, content, rating)
VALUES (p_product_id, p_client_id, p_content, p_rating);

END;

$$ LANGUAGE plpgsql;