import { useEffect, useState } from "react";
import api from "../services/api";
import ProductCard from "../components/ProductCard";

function ProductList() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    api.get("/catalog/products")
      .then((res) => {
        setProducts(res.data);
        setLoading(false);
      })
      .catch(() => {
        setError("Failed to load products");
        setLoading(false);
      });
  }, []);

  // 🔄 Loading state
  if (loading) {
    return (
      <div className="page">
        <h2>Products</h2>
        <p>Loading products...</p>
      </div>
    );
  }

  // ❌ Error state
  if (error) {
    return (
      <div className="page">
        <h2>Products</h2>
        <p style={{ color: "red" }}>{error}</p>
      </div>
    );
  }

  // 📦 Empty state
  if (products.length === 0) {
    return (
      <div className="page">
        <h2>Products</h2>
        <p>No products found</p>
      </div>
    );
  }

  return (
    <div className="page">
      <h2>Products</h2>

      <div className="card-grid">
        {products.map((p) => (
          <ProductCard key={p.id} product={p} />
        ))}
      </div>
    </div>
  );
}

export default ProductList;
