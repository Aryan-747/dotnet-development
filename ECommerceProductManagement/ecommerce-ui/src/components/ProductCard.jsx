import { useNavigate } from "react-router-dom";

function ProductCard({ product, mode = "admin" }) {
  const navigate = useNavigate();
  const destination =
    mode === "customer"
      ? `/customer/products/${product.id}`
      : `/admin/products/${product.id}`;

  return (
    <article className="card product-card" onClick={() => navigate(destination)}>
      <img alt={product.name} className="product-image" src={product.primaryImageUrl} />
      <div className="product-copy">
        <div className="pill-row">
          <span className="pill">{product.categoryName || "General"}</span>
          <span className={`pill status-${String(product.status).toLowerCase()}`}>
            {product.status}
          </span>
        </div>
        <h3>{product.name}</h3>
        <p>{product.brand}</p>
        <p className="muted">SKU: {product.sku}</p>
        <p className="price">Rs. {product.sellingPrice}</p>
      </div>
    </article>
  );
}

export default ProductCard;
