import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../services/api";

function ProductDetails() {
  const { id } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    api.get(`/catalog/products/${id}`)
      .then((res) => setProduct(res.data))
      .catch(() => alert("Error loading product"));
  }, [id]);

  if (!product) return <p>Loading...</p>;

  return (
    <div className="page">
      <h2>Product Details</h2>

      <div className="card">
        <h3>{product.name}</h3>
        <p><strong>SKU:</strong> {product.sku}</p>
        <p><strong>Status:</strong> {product.status}</p>
      </div>
    </div>
  );
}

export default ProductDetails;
