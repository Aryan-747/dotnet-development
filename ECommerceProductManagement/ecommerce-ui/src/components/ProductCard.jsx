import { useNavigate } from "react-router-dom";

function ProductCard({ product, mode = "admin" }) {
  const navigate = useNavigate();
  const destination =
    mode === "customer"
      ? `/customer/products/${product.id}`
      : `/admin/products/${product.id}`;
  const rating = (4 + ((product.sellingPrice || 0) % 10) / 20).toFixed(1);
  const reviewCount = 120 + ((product.stockQuantity || 0) * 7);
  const isCustomer = mode === "customer";

  return (
    <article
      className={isCustomer ? "product-card marketplace-card" : "card product-card"}
      onClick={() => navigate(destination)}
    >
      <div className="product-image-shell">
        <img alt={product.name} className="product-image" src={product.primaryImageUrl} />
        {isCustomer ? <span className="deal-badge">Limited time deal</span> : null}
      </div>

      <div className="product-copy">
        <div className="pill-row">
          <span className="pill">{product.categoryName || "General"}</span>
          <span className={`pill status-${String(product.status).toLowerCase()}`}>
            {product.status}
          </span>
        </div>

        <h3 className="product-title">{product.name}</h3>
        <p className="muted product-brand">{product.brand}</p>

        {isCustomer ? (
          <div className="rating-row">
            <span className="rating-score">{rating}</span>
            <span className="rating-stars">★★★★★</span>
            <span className="muted">({reviewCount})</span>
          </div>
        ) : null}

        <p className="price">Rs. {Number(product.sellingPrice || 0).toLocaleString("en-IN")}</p>
        <p className="muted">SKU: {product.sku}</p>
        <p className={product.stockQuantity > 0 ? "stock-good" : "stock-low"}>
          {product.stockQuantity > 0
            ? `In stock: ${product.stockQuantity} units`
            : "Currently unavailable"}
        </p>
      </div>
    </article>
  );
}

export default ProductCard;
