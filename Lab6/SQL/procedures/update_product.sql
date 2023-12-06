DROP PROCEDURE IF EXISTS update_product;

CREATE OR REPLACE PROCEDURE update_product(
        p_product_id INT,
        product_category_id INT,
        price NUMERIC(7, 2),
        name VARCHAR(30),
        production_date DATE,
        quantity INT,
        brand VARCHAR(30),
        description TEXT
    ) LANGUAGE plpgsql AS $$ BEGIN
UPDATE product
SET product_category_id = update_product.product_category_id,
    price = update_product.price,
    name = update_product.name,
    production_date = update_product.production_date,
    quantity = update_product.quantity,
    brand = update_product.brand,
    description = update_product.description
WHERE product_id = p_product_id;

END;

$$;