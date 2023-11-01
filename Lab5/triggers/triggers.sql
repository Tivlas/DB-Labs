-- @block
DROP TRIGGER IF EXISTS update_cart_price_trigger ON cart_item;
DROP TRIGGER IF EXISTS insert_cart_for_new_client ON client;
DROP TRIGGER IF EXISTS copy_cart_items_to_order_items_trigger ON "order";

-- @block
CREATE TRIGGER update_cart_price_trigger
AFTER
INSERT OR DELETE ON cart_item FOR EACH ROW EXECUTE PROCEDURE update_cart_price();

CREATE TRIGGER insert_cart_for_new_client
AFTER
INSERT ON client FOR EACH ROW EXECUTE PROCEDURE create_cart_for_new_client();

CREATE TRIGGER copy_cart_items_to_order_items_trigger
AFTER
INSERT ON "order" FOR EACH ROW EXECUTE PROCEDURE copy_cart_items_to_order_items();