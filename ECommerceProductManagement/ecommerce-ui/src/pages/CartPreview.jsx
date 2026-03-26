import { Link } from "react-router-dom";

function CartPreview() {
  const items = JSON.parse(localStorage.getItem("preview-cart") || "[]");
  const total = items.reduce((sum, item) => sum + Number(item.sellingPrice || 0), 0);

  return (
    <div className="page page-spacious">
      <div className="section-header">
        <div>
          <p className="eyebrow">Cart Storefront Preview</p>
          <h1>Preview checkout readiness</h1>
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
        <div className="split-grid">
          <div className="stack">
            {items.map((item) => (
              <article className="card" key={item.id}>
                <h3>{item.name}</h3>
                <p>{item.brand}</p>
                <p>Rs. {item.sellingPrice}</p>
                <p>{item.stockQuantity > 0 ? "Ready to buy" : "Out of stock"}</p>
              </article>
            ))}
          </div>

          <aside className="card summary-card">
            <h2>Order summary</h2>
            <p>{items.length} items selected for preview</p>
            <p className="price-large">Rs. {total.toFixed(2)}</p>
            <p className="muted">
              This flow is intentionally lightweight and meant for storefront validation.
            </p>
          </aside>
        </div>
      )}
    </div>
  );
}

export default CartPreview;
