import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import ProductCard from "../components/ProductCard";
import api from "../services/api";

const emptyProduct = {
  name: "",
  sku: "",
  brand: "",
  description: "",
  categoryName: "",
  seoTitle: "",
  seoDescription: "",
  tags: "",
  primaryImageUrl: "",
  sellingPrice: 0,
  stockQuantity: 0,
};

function ProductList() {
  const [products, setProducts] = useState([]);
  const [query, setQuery] = useState("");
  const [form, setForm] = useState(emptyProduct);
  const [error, setError] = useState("");
  const [saving, setSaving] = useState(false);
  const { user } = useAuth();
  const navigate = useNavigate();

  const loadProducts = async () => {
    const { data } = await api.get("/catalog/products");
    setProducts(data);
  };

  useEffect(() => {
    loadProducts();
  }, []);

  const filteredProducts = useMemo(() => {
    const value = query.trim().toLowerCase();

    if (!value) {
      return products;
    }

    return products.filter((product) =>
      [product.name, product.sku, product.brand, product.categoryName, product.tags]
        .filter(Boolean)
        .some((field) => field.toLowerCase().includes(value))
    );
  }, [products, query]);

  const onCreate = async (event) => {
    event.preventDefault();
    setSaving(true);
    setError("");

    try {
      const { data } = await api.post("/catalog/products", form);
      await api.post("/admin/audit", {
        productId: data.id,
        action: "ProductCreated",
        entityName: "Catalog",
        details: `${data.name} was created from the admin product console.`,
      });
      setForm(emptyProduct);
      await loadProducts();
      navigate(`/admin/products/${data.id}`);
    } catch (requestError) {
      setError(requestError?.response?.data || "Unable to create product.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="page page-spacious">
      <section className="hero compact">
        <div>
          <p className="eyebrow">Admin Product Management</p>
          <h1>Catalog operations workspace</h1>
          <p>
            Create products, enrich metadata, and move items into pricing, inventory, and
            approval stages.
          </p>
        </div>
      </section>

      <div className="split-grid">
        <section className="card">
          <div className="section-header">
            <div>
              <p className="eyebrow">Create product</p>
              <h2>New onboarding entry</h2>
            </div>
          </div>

          <form className="form-grid" onSubmit={onCreate}>
            <label>
              Product name
              <input
                value={form.name}
                onChange={(event) => setForm({ ...form, name: event.target.value })}
                required
              />
            </label>

            <label>
              SKU
              <input
                value={form.sku}
                onChange={(event) => setForm({ ...form, sku: event.target.value })}
                required
              />
            </label>

            <label>
              Brand
              <input
                value={form.brand}
                onChange={(event) => setForm({ ...form, brand: event.target.value })}
              />
            </label>

            <label>
              Category
              <input
                value={form.categoryName}
                onChange={(event) => setForm({ ...form, categoryName: event.target.value })}
              />
            </label>

            <label className="full-span">
              Description
              <textarea
                rows="4"
                value={form.description}
                onChange={(event) => setForm({ ...form, description: event.target.value })}
              />
            </label>

            <label>
              Primary image URL
              <input
                value={form.primaryImageUrl}
                onChange={(event) =>
                  setForm({ ...form, primaryImageUrl: event.target.value })
                }
              />
            </label>

            <label>
              Tags
              <input
                value={form.tags}
                onChange={(event) => setForm({ ...form, tags: event.target.value })}
              />
            </label>

            <label>
              SEO title
              <input
                value={form.seoTitle}
                onChange={(event) => setForm({ ...form, seoTitle: event.target.value })}
              />
            </label>

            <label>
              SEO description
              <input
                value={form.seoDescription}
                onChange={(event) =>
                  setForm({ ...form, seoDescription: event.target.value })
                }
              />
            </label>

            <label>
              Selling price
              <input
                min="0"
                step="0.01"
                type="number"
                value={form.sellingPrice}
                onChange={(event) =>
                  setForm({ ...form, sellingPrice: Number(event.target.value) })
                }
              />
            </label>

            <label>
              Opening stock
              <input
                min="0"
                type="number"
                value={form.stockQuantity}
                onChange={(event) =>
                  setForm({ ...form, stockQuantity: Number(event.target.value) })
                }
              />
            </label>

            {error ? <p className="banner error full-span">{error}</p> : null}

            <button className="full-span" disabled={saving} type="submit">
              {saving ? "Creating..." : `Create as ${user?.role}`}
            </button>
          </form>
        </section>

        <section className="card">
          <div className="section-header">
            <div>
              <p className="eyebrow">Product list</p>
              <h2>Existing catalog</h2>
            </div>
            <input
              className="inline-search"
              value={query}
              onChange={(event) => setQuery(event.target.value)}
              placeholder="Search products"
            />
          </div>

          <div className="card-grid">
            {filteredProducts.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </section>
      </div>
    </div>
  );
}

export default ProductList;
