import { useEffect, useMemo, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import api from "../services/api";

const steps = ["Basic", "Media", "Pricing", "Inventory", "Review", "Publish"];

function ProductDetails({ mode = "admin" }) {
  const { id } = useParams();
  const { user, isAuthenticated } = useAuth();
  const [product, setProduct] = useState(null);
  const [workflow, setWorkflow] = useState({ price: null, inventory: null, approval: null });
  const [form, setForm] = useState(null);
  const [step, setStep] = useState(0);
  const [message, setMessage] = useState("");

  useEffect(() => {
    const loadData = async () => {
      const productPath =
        mode === "customer"
          ? `/catalog/products/preview/${id}`
          : `/catalog/products/${id}`;
      const [{ data: productData }, workflowResponse] = await Promise.all([
        api.get(productPath),
        isAuthenticated
          ? api.get(`/workflow/product/${id}`).catch(() => ({ data: {} }))
          : Promise.resolve({ data: {} }),
      ]);

      setProduct(productData);
      setForm(productData);
      setWorkflow(workflowResponse.data);
    };

    loadData();
  }, [id, isAuthenticated, mode]);

  const loadData = async () => {
    const productPath =
      mode === "customer" ? `/catalog/products/preview/${id}` : `/catalog/products/${id}`;
    const [{ data: productData }, workflowResponse] = await Promise.all([
      api.get(productPath),
      isAuthenticated
        ? api.get(`/workflow/product/${id}`).catch(() => ({ data: {} }))
        : Promise.resolve({ data: {} }),
    ]);

    setProduct(productData);
    setForm(productData);
    setWorkflow(workflowResponse.data);
  };

  const canEdit = useMemo(
    () => mode === "admin" && ["Admin", "ProductManager", "ContentExecutive"].includes(user?.role),
    [mode, user]
  );

  if (!product || !form) {
    return <div className="screen-message">Loading product details...</div>;
  }

  const saveAudit = async (action, details) => {
    if (!isAuthenticated) {
      return;
    }

    await api.post("/admin/audit", {
      productId: id,
      action,
      entityName: mode === "customer" ? "Storefront" : "Admin",
      details,
    });
  };

  const saveProduct = async () => {
    await api.put(`/catalog/products/${id}`, form);
    await saveAudit("ProductUpdated", `${form.name} metadata updated.`);
    setMessage("Product details saved.");
    await loadData();
  };

  const savePricing = async () => {
    await api.put(`/workflow/pricing/${id}`, {
      mrp: Number(form.sellingPrice || 0) * 1.15,
      sellingPrice: Number(form.sellingPrice || 0),
    });
    await saveAudit("PricingUpdated", `Pricing updated for ${form.name}.`);
    setMessage("Pricing updated.");
    await loadData();
  };

  const saveInventory = async () => {
    await api.put(`/workflow/inventory/${id}`, {
      quantity: Number(form.stockQuantity || 0),
      availabilityMessage:
        Number(form.stockQuantity || 0) > 0 ? "In Stock" : "Out of Stock",
    });
    await saveAudit("InventoryUpdated", `Inventory updated for ${form.name}.`);
    setMessage("Inventory updated.");
    await loadData();
  };

  const submitForReview = async () => {
    await saveProduct();
    await savePricing();
    await saveInventory();
    await api.post(`/workflow/submit/${id}`);
    await api.put(`/catalog/products/${id}/status`, {
      status: "ReadyForReview",
      isPublished: false,
    });
    await saveAudit("SubmittedForReview", `${form.name} submitted for approval.`);
    setMessage("Product submitted for admin review.");
    await loadData();
  };

  const reviewAction = async (action, status, auditAction) => {
    await api.post(`/workflow/${action}/${id}`, { remarks: `${status} by admin` });
    await api.put(`/catalog/products/${id}/status`, {
      status,
      isPublished: status === "Published",
    });
    await saveAudit(auditAction, `${form.name} marked as ${status}.`);
    setMessage(`Product ${status.toLowerCase()}.`);
    await loadData();
  };

  const addToCart = async () => {
    const current = JSON.parse(localStorage.getItem("preview-cart") || "[]");
    localStorage.setItem("preview-cart", JSON.stringify([...current, product]));
    setMessage("Added to preview cart.");
  };

  return (
    <div className="page page-spacious">
      <div className="section-header">
        <div>
          <p className="eyebrow">
            {mode === "customer" ? "Product Details Preview" : "Product Details Edit"}
          </p>
          <h1>{product.name}</h1>
        </div>
        <div className="pill-row">
          <span className="pill">{product.categoryName}</span>
          <span className={`pill status-${String(product.status).toLowerCase()}`}>
            {product.status}
          </span>
        </div>
      </div>

      {message ? <p className="banner success">{message}</p> : null}

      <div className="split-grid">
        <section className="card media-card">
          <img alt={product.name} className="detail-image" src={form.primaryImageUrl} />
          <p className="price-large">Rs. {form.sellingPrice}</p>
          <p>{form.description}</p>
          {mode === "customer" ? (
            <button onClick={addToCart} type="button">
              Add to preview cart
            </button>
          ) : null}
        </section>

        <section className="card">
          <div className="wizard-steps">
            {steps.map((label, index) => (
              <button
                className={index === step ? "step-chip active" : "step-chip"}
                key={label}
                onClick={() => setStep(index)}
                type="button"
              >
                {label}
              </button>
            ))}
          </div>

          <div className="form-grid">
            <label>
              Name
              <input
                disabled={!canEdit}
                value={form.name}
                onChange={(event) => setForm({ ...form, name: event.target.value })}
              />
            </label>

            <label>
              SKU
              <input
                disabled={!canEdit}
                value={form.sku}
                onChange={(event) => setForm({ ...form, sku: event.target.value })}
              />
            </label>

            <label>
              Brand
              <input
                disabled={!canEdit}
                value={form.brand}
                onChange={(event) => setForm({ ...form, brand: event.target.value })}
              />
            </label>

            <label>
              Category
              <input
                disabled={!canEdit}
                value={form.categoryName}
                onChange={(event) => setForm({ ...form, categoryName: event.target.value })}
              />
            </label>

            <label className="full-span">
              Description
              <textarea
                disabled={!canEdit}
                rows="4"
                value={form.description}
                onChange={(event) => setForm({ ...form, description: event.target.value })}
              />
            </label>

            <label>
              Image URL
              <input
                disabled={!canEdit}
                value={form.primaryImageUrl}
                onChange={(event) =>
                  setForm({ ...form, primaryImageUrl: event.target.value })
                }
              />
            </label>

            <label>
              Tags
              <input
                disabled={!canEdit}
                value={form.tags}
                onChange={(event) => setForm({ ...form, tags: event.target.value })}
              />
            </label>

            <label>
              Selling price
              <input
                disabled={!canEdit}
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
              Stock quantity
              <input
                disabled={!canEdit}
                min="0"
                type="number"
                value={form.stockQuantity}
                onChange={(event) =>
                  setForm({ ...form, stockQuantity: Number(event.target.value) })
                }
              />
            </label>
          </div>

          {mode === "admin" ? (
            <div className="action-row">
              <button onClick={saveProduct} type="button">
                Save basic details
              </button>
              <button onClick={savePricing} type="button">
                Save pricing
              </button>
              <button onClick={saveInventory} type="button">
                Save inventory
              </button>
              {user?.role !== "ContentExecutive" ? (
                <button onClick={submitForReview} type="button">
                  Submit for review
                </button>
              ) : null}
              {user?.role === "Admin" ? (
                <>
                  <button onClick={() => reviewAction("approve", "Approved", "Approved")} type="button">
                    Approve
                  </button>
                  <button onClick={() => reviewAction("reject", "Rejected", "Rejected")} type="button">
                    Reject
                  </button>
                  <button
                    onClick={() => reviewAction("publish", "Published", "Published")}
                    type="button"
                  >
                    Publish
                  </button>
                </>
              ) : null}
            </div>
          ) : null}
        </section>
      </div>

      {isAuthenticated ? (
        <section className="card">
          <div className="section-header">
            <div>
              <p className="eyebrow">Workflow snapshot</p>
              <h2>Pricing, inventory, and approval</h2>
            </div>
          </div>

          <div className="metric-grid">
            <article className="metric-card card">
              <span>Pricing</span>
              <strong>
                {workflow.price ? `Rs. ${workflow.price.sellingPrice}` : "Not saved"}
              </strong>
            </article>
            <article className="metric-card card">
              <span>Inventory</span>
              <strong>
                {workflow.inventory ? workflow.inventory.quantity : "Not saved"}
              </strong>
            </article>
            <article className="metric-card card">
              <span>Approval</span>
              <strong>{workflow.approval?.status || "Not submitted"}</strong>
            </article>
          </div>
        </section>
      ) : null}
    </div>
  );
}

export default ProductDetails;
