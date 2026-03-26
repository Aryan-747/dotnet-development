import { useNavigate } from "react-router-dom";

function ProductCard({ product }) {
  const navigate = useNavigate();

  return (
    <div
      className="card product-card"
      onClick={() => navigate(`/products/${product.id}`)}
    >
      <h3>{product.name}</h3>
      <p><strong>SKU:</strong> {product.sku}</p>
      <p><strong>Status:</strong> {product.status}</p>
    </div>
  );
}

export default ProductCard;
