CREATE OR REPLACE PROCEDURE insert_product(
        product_category_id INT,
        price NUMERIC(7, 2),
        name VARCHAR(30),
        production_date DATE,
        quantity INT,
        brand VARCHAR(30),
        description TEXT
    ) LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO product (
        product_category_id,
        price,
        name,
        production_date,
        quantity,
        brand,
        description
    )
VALUES (
        product_category_id,
        price,
        name,
        production_date,
        quantity,
        brand,
        description
    );

END;

$$;