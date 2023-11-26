CREATE OR REPLACE FUNCTION apply_discount_to_cart_item() RETURNS TRIGGER AS $$
DECLARE total_discount_percent numeric;

BEGIN
SELECT COALESCE(SUM(d.percent), 0) INTO total_discount_percent
FROM discount d
    JOIN product_discount pd ON pd.discount_id = d.discount_id
WHERE pd.product_id = NEW.product_id
    AND d.is_active = TRUE;

total_discount_percent = total_discount_percent % 50;

UPDATE cart_item
SET product_price = NEW.product_price * (1 - total_discount_percent / 100)
WHERE cart_item_id = NEW.cart_item_id;

RETURN NEW;

END;

$$ LANGUAGE plpgsql;