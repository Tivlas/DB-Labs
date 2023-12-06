DROP FUNCTION IF EXISTS get_products_with_categories_by_category_id;

CREATE OR REPLACE FUNCTION get_products_with_categories_by_category_id(p_product_category_id INT) RETURNS TABLE (
        product_id INT,
        product_category_id INT,
        price NUMERIC,
        name VARCHAR,
        production_date DATE,
        quantity INT,
        brand VARCHAR,
        description TEXT,
        category_name VARCHAR
    ) AS $$ BEGIN RETURN QUERY
SELECT p.product_id,
    p.product_category_id,
    p.price,
    p.name,
    p.production_date,
    p.quantity,
    p.brand,
    p.description,
    pc.name AS category_name
FROM product p
    INNER JOIN product_category pc ON pc.product_category_id = p.product_category_id
WHERE p.product_category_id = p_product_category_id;

RETURN;

END;

$$ LANGUAGE plpgsql;