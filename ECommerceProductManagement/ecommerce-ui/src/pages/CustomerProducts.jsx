import { useEffect, useMemo, useState } from "react";
import { useSearchParams } from "react-router-dom";
import ProductCard from "../components/ProductCard";
import api from "../services/api";

function CustomerProducts() {
  const [searchParams, setSearchParams] = useSearchParams();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const query = searchParams.get("q") ?? "";
  const category = searchParams.get("category") ?? "All";
  const sortBy = searchParams.get("sort") ?? "featured";

  useEffect(() => {
    api
      .get("/catalog/products/preview")
      .then(({ data }) => setProducts(data))
      .finally(() => setLoading(false));
  }, []);

  const updateParams = (next) => {
    const params = new URLSearchParams(searchParams);

    Object.entries(next).forEach(([key, value]) => {
      if (!value || value === "All" || value === "featured") {
        params.delete(key);
      } else {
        params.set(key, value);
      }
    });

    setSearchParams(params);
  };

  const categories = useMemo(() => {
    const values = ["All", ...new Set(products.map((product) => product.categoryName).filter(Boolean))];
    return values;
  }, [products]);

  const filteredProducts = useMemo(() => {
    const value = query.trim().toLowerCase();
    let next = products.filter((product) => {
      const matchesText =
        !value ||
        [product.name, product.brand, product.categoryName, product.tags, product.sku]
          .filter(Boolean)
          .some((field) => field.toLowerCase().includes(value));
      const matchesCategory = category === "All" || product.categoryName === category;
      return matchesText && matchesCategory;
    });

    if (sortBy === "price-low") {
      next = [...next].sort((a, b) => a.sellingPrice - b.sellingPrice);
    } else if (sortBy === "price-high") {
      next = [...next].sort((a, b) => b.sellingPrice - a.sellingPrice);
    } else if (sortBy === "newest") {
      next = [...next].sort(
        (a, b) => new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime()
      );
    }

    return next;
  }, [category, products, query, sortBy]);

  return (
    <div className="marketplace-page">
      <section className="market-hero">
        <div className="market-hero-card hero-main">
          <p className="eyebrow">Storefront Preview</p>
          <h1>Discover products the way customers shop online</h1>
          <p>
            Browse departments, compare prices, and review published items in a familiar
            ecommerce-style layout.
          </p>
          <div className="hero-actions">
            <button type="button">Top picks</button>
            <button className="ghost-link" type="button">
              Fast delivery
            </button>
          </div>
        </div>

        <div className="market-hero-card">
          <p className="eyebrow">Deals Snapshot</p>
          <h2>10+ live products ready for preview</h2>
          <p className="muted">Fresh additions across electronics, fashion, kitchen, fitness, and home.</p>
        </div>
      </section>

      <section className="market-shell">
        <aside className="market-sidebar">
          <div className="sidebar-card">
            <h3>Department</h3>
            <div className="sidebar-options">
              {categories.map((entry) => (
                <button
                  className={entry === category ? "sidebar-option active" : "sidebar-option"}
                  key={entry}
                  onClick={() => updateParams({ category: entry })}
                  type="button"
                >
                  {entry}
                </button>
              ))}
            </div>
          </div>

          <div className="sidebar-card">
            <h3>Shop by</h3>
            <p className="muted">Free delivery eligible</p>
            <p className="muted">Best rated picks</p>
            <p className="muted">Everyday essentials</p>
          </div>
        </aside>

        <div className="market-results">
          <div className="results-toolbar">
            <div>
              <h2>Results</h2>
              <p className="muted">
                {filteredProducts.length} items in {category === "All" ? "all departments" : category}
              </p>
            </div>

            <div className="toolbar-controls">
              <input
                value={query}
                onChange={(event) => updateParams({ q: event.target.value })}
                placeholder="Search by brand, category or SKU"
              />

              <select
                value={sortBy}
                onChange={(event) => updateParams({ sort: event.target.value })}
              >
                <option value="featured">Featured</option>
                <option value="newest">Newest arrivals</option>
                <option value="price-low">Price: Low to High</option>
                <option value="price-high">Price: High to Low</option>
              </select>
            </div>
          </div>

          <div className="promo-strip">
            <span>Top rated products</span>
            <span>Secure checkout preview</span>
            <span>Detailed product pages</span>
            <span>Operationally published catalog</span>
          </div>

          {loading ? <p className="screen-message">Loading storefront preview...</p> : null}

          {!loading && filteredProducts.length === 0 ? (
            <p className="screen-message">No published products match your filters.</p>
          ) : null}

          <div className="market-grid">
            {filteredProducts.map((product) => (
              <ProductCard key={product.id} product={product} mode="customer" />
            ))}
          </div>
        </div>
      </section>
    </div>
  );
}

export default CustomerProducts;
