import { useEffect, useMemo, useState } from "react";
import ProductCard from "../components/ProductCard";
import api from "../services/api";

function CustomerProducts() {
  const [products, setProducts] = useState([]);
  const [query, setQuery] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api
      .get("/catalog/products/preview")
      .then(({ data }) => setProducts(data))
      .finally(() => setLoading(false));
  }, []);

  const filteredProducts = useMemo(() => {
    const value = query.trim().toLowerCase();

    if (!value) {
      return products;
    }

    return products.filter((product) =>
      [product.name, product.brand, product.categoryName, product.tags]
        .filter(Boolean)
        .some((field) => field.toLowerCase().includes(value))
    );
  }, [products, query]);

  return (
    <div className="page page-spacious">
      <section className="hero">
        <div>
          <p className="eyebrow">Storefront Preview</p>
          <h1>Published product experience</h1>
          <p>
            Browse what customers would actually see after a product clears review and
            publishing.
          </p>
        </div>

        <div className="search-card">
          <label>
            Search preview catalog
            <input
              value={query}
              onChange={(event) => setQuery(event.target.value)}
              placeholder="Try brand, category, or SKU"
            />
          </label>
        </div>
      </section>

      {loading ? <p className="screen-message">Loading storefront preview...</p> : null}

      {!loading && filteredProducts.length === 0 ? (
        <p className="screen-message">No published products match your search.</p>
      ) : null}

      <div className="card-grid">
        {filteredProducts.map((product) => (
          <ProductCard key={product.id} product={product} mode="customer" />
        ))}
      </div>
    </div>
  );
}

export default CustomerProducts;
