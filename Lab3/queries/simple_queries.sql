SELECT *
FROM product;

SELECT *
FROM discount;

SELECT *
FROM product
WHERE quantity > 150;

SELECT *
FROM "order";

SELECT first_name || ' ' || last_name AS name,
    COUNT(*) AS "order count",
    sum(price) AS "Total order price"
FROM client
    INNER JOIN "order" ON client.client_id = "order".client_id
GROUP BY name
ORDER BY name;

SELECT product.name,
    price
FROM product
    JOIN product_category USING(product_category_id)
WHERE product_category.name LIKE 'Смарт%';