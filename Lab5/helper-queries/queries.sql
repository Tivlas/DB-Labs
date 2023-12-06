-- @block
INSERT INTO cart_item (
        cart_id,
        product_id,
        product_quantity,
        product_price
    )
VALUES (1, 1, 2, 2799.00),
    (2, 3, 1, 1891.60);

-- @block
INSERT INTO cart_item (
        cart_id,
        product_id,
        product_quantity,
        product_price
    )
VALUES (2, 2, 3, 2699.00),
    (1, 2, 3, 2699.00);

-- @block
SELECT *
FROM cart;

-- @block
SELECT *
FROM cart_item;

-- @block
INSERT INTO "order" (client_id, price)
VALUES (1, 13695);

-- @block
SELECT *
FROM "order";