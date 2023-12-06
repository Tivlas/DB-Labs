CREATE OR REPLACE PROCEDURE insert_order(
        p_client_id INT,
        p_price NUMERIC(7, 2)
    ) LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO "order" ("client_id", "price")
VALUES (p_client_id, p_price);

END;

$$;