import { Link } from "react-router-dom";
import { useCart } from "../hooks/useCart";

function CartPreview() {
  const { items, totalItems, totalPrice, removeItem, decreaseItem, addItem } = useCart();
  const estimatedTax = Math.round(totalPrice * 0.18);
  const grandTotal = totalPrice + estimatedTax;

  return (
    <div className="page page-spacious">
      <div className="section-header">
        <div>
          <p className="eyebrow">Cart Storefront Preview</p>
          <h1>Your shopping cart</h1>
          <p className="muted">Review items, adjust quantities, and preview order value.</p>
        </div>
      </div>

      {items.length === 0 ? (
        <div className="empty-panel">
          <p>Your preview cart is empty.</p>
          <Link className="ghost-link" to="/customer/products">
            Browse published products
          </Link>
        </div>
      ) : (
        <div className="cart-layout">
          <section className="cart-items">
            {items.map((item) => (
              <article className="cart-item-card" key={item.id}>
                <img alt={item.name} className="cart-item-image" src={item.primaryImageUrl} />

                <div className="cart-item-copy">
                  <span className="cart-item-chip">In cart</span>
                  <h3>{item.name}</h3>
                  <p className="muted">{item.brand}</p>
                  <p className="price">Rs. {Number(item.sellingPrice || 0).toLocaleString("en-IN")}</p>
                  <p className={item.stockQuantity > 0 ? "stock-good" : "stock-low"}>
                    {item.stockQuantity > 0 ? "Eligible for fast delivery" : "Out of stock"}
                  </p>
                  <div className="cart-item-meta">
                    <span>Unit price: Rs. {Number(item.sellingPrice || 0).toLocaleString("en-IN")}</span>
                    <span>Line total: Rs. {Number((item.sellingPrice || 0) * item.quantity).toLocaleString("en-IN")}</span>
                  </div>
                </div>

                <div className="cart-item-actions">
                  <div className="cart-qty-group" aria-label={`Quantity controls for ${item.name}`}>
                    <button
                      aria-label={`Decrease quantity of ${item.name}`}
                      className="cart-qty-button"
                      onClick={() => decreaseItem(item.id)}
                      type="button"
                    >
                      -
                    </button>
                    <span className="cart-qty-value">{item.quantity}</span>
                    <button
                      aria-label={`Increase quantity of ${item.name}`}
                      className="cart-qty-button"
                      onClick={() => addItem(item)}
                      type="button"
                    >
                      +
                    </button>
                  </div>
                  <button className="cart-remove-link" onClick={() => removeItem(item.id)} type="button">
                    Remove from cart
                  </button>
                </div>
              </article>
            ))}
          </section>

          <aside className="card cart-summary-card">
            <p className="eyebrow">Order summary</p>
            <h2>Subtotal ({totalItems} items)</h2>
            <p className="price-large">Rs. {totalPrice.toLocaleString("en-IN")}</p>
            <div className="summary-line">
              <span>Shipping</span>
              <strong>Free</strong>
            </div>
            <div className="summary-line">
              <span>Estimated tax</span>
              <strong>Rs. {estimatedTax.toLocaleString("en-IN")}</strong>
            </div>
            <div className="summary-line summary-line-total">
              <span>Order total</span>
              <strong>Rs. {grandTotal.toLocaleString("en-IN")}</strong>
            </div>
            <button type="button">Proceed to checkout preview</button>
            <Link className="ghost-link" to="/customer/products">
              Continue shopping
            </Link>
          </aside>
        </div>
      )}
    </div>
  );
}

export default CartPreview;
