-- @block
DROP TABLE IF EXISTS review_log;

CREATE TABLE review_log (
    log_id serial PRIMARY KEY,
    operation text NOT NULL,
    review_id int NOT NULL,
    product_id int NOT NULL,
    client_id int NOT NULL,
    content text NOT NULL,
    rating int NOT NULL,
    "date" timestamp NOT NULL,
    modified_date timestamp DEFAULT CURRENT_TIMESTAMP
);

-- @block
CREATE OR REPLACE FUNCTION review_trigger_function() RETURNS TRIGGER AS $$ BEGIN IF (TG_OP = 'INSERT') THEN
INSERT INTO review_log (
        operation,
        review_id,
        product_id,
        client_id,
        content,
        rating,
        "date"
    )
VALUES (
        'INSERT',
        NEW.review_id,
        NEW.product_id,
        NEW.client_id,
        NEW.content,
        NEW.rating,
        NEW.date
    );

ELSIF (TG_OP = 'UPDATE') THEN
INSERT INTO review_log (
        operation,
        review_id,
        product_id,
        client_id,
        content,
        rating,
        "date"
    )
VALUES (
        'UPDATE',
        NEW.review_id,
        NEW.product_id,
        NEW.client_id,
        NEW.content,
        NEW.rating,
        NEW.date
    );

ELSIF (TG_OP = 'DELETE') THEN
INSERT INTO review_log (
        operation,
        review_id,
        product_id,
        client_id,
        content,
        rating,
        "date"
    )
VALUES (
        'DELETE',
        OLD.review_id,
        OLD.product_id,
        OLD.client_id,
        OLD.content,
        OLD.rating,
        OLD.date
    );

END IF;

RETURN NEW;

END;

$$ LANGUAGE plpgsql;

-- @block
CREATE TRIGGER review_trigger
AFTER
INSERT
    OR
UPDATE
    OR DELETE ON review FOR EACH ROW EXECUTE FUNCTION review_trigger_function();

-- @block
SELECT *
FROM review_log;