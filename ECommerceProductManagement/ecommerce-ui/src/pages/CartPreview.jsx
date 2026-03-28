import { Link } from "react-router-dom";
import { useCart } from "../hooks/useCart";

function CartPreview() {
  const { items, totalItems, totalPrice, removeItem, decreaseItem } = useCart();

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
                  <h3>{item.name}</h3>
                  <p className="muted">{item.brand}</p>
                  <p className="price">Rs. {Number(item.sellingPrice || 0).toLocaleString("en-IN")}</p>
                  <p className={item.stockQuantity > 0 ? "stock-good" : "stock-low"}>
                    {item.stockQuantity > 0 ? "Eligible for fast delivery" : "Out of stock"}
                  </p>
                </div>

                <div className="cart-item-actions">
                  <span className="cart-qty">Qty: {item.quantity}</span>
                  <button onClick={() => decreaseItem(item.id)} type="button">
                    Remove one
                  </button>
                  <button className="ghost-link" onClick={() => removeItem(item.id)} type="button">
                    Remove item
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
              <strong>Rs. {(totalPrice * 0.18).toLocaleString("en-IN")}</strong>
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
