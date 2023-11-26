DROP TRIGGER IF EXISTS apply_discount_trigger ON cart_item;

CREATE TRIGGER apply_discount_trigger
AFTER
INSERT ON cart_item FOR EACH ROW EXECUTE FUNCTION apply_discount_to_cart_item();