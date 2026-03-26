import { useEffect, useState } from "react";
import api from "../services/api";

function Reports() {
  const [products, setProducts] = useState([]);
  const [selectedProductId, setSelectedProductId] = useState("");
  const [logs, setLogs] = useState([]);
  const [dashboard, setDashboard] = useState(null);

  useEffect(() => {
    Promise.all([api.get("/catalog/products"), api.get("/admin/dashboard")]).then(
      ([productsResponse, dashboardResponse]) => {
        setProducts(productsResponse.data);
        setDashboard(dashboardResponse.data);

        if (productsResponse.data[0]) {
          setSelectedProductId(productsResponse.data[0].id);
        }
      }
    );
  }, []);

  useEffect(() => {
    if (!selectedProductId) {
      return;
    }

    api.get(`/admin/audit/${selectedProductId}`).then(({ data }) => setLogs(data));
  }, [selectedProductId]);

  const exportAudit = async () => {
    const response = await api.get(`/admin/export/audit/${selectedProductId}`, {
      responseType: "blob",
    });

    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", `audit-${selectedProductId}.csv`);
    document.body.appendChild(link);
    link.click();
    link.remove();
  };

  return (
    <div className="page page-spacious">
      <div className="section-header">
        <div>
          <p className="eyebrow">Admin Reports</p>
          <h1>Audit history and exports</h1>
        </div>
      </div>

      {dashboard ? (
        <section className="metric-grid">
          <article className="card metric-card">
            <span>Total activity</span>
            <strong>{dashboard.totalActivities}</strong>
          </article>
          <article className="card metric-card">
            <span>Audits today</span>
            <strong>{dashboard.auditsToday}</strong>
          </article>
          <article className="card metric-card">
            <span>Pending alerts</span>
            <strong>{dashboard.pendingAlerts}</strong>
          </article>
        </section>
      ) : null}

      <section className="card">
        <div className="section-header">
          <div>
            <p className="eyebrow">Order History Product Audit</p>
            <h2>Timeline explorer</h2>
          </div>

          <div className="action-row">
            <select
              value={selectedProductId}
              onChange={(event) => setSelectedProductId(event.target.value)}
            >
              {products.map((product) => (
                <option key={product.id} value={product.id}>
                  {product.name}
                </option>
              ))}
            </select>
            <button onClick={exportAudit} type="button">
              Export CSV
            </button>
          </div>
        </div>

        <div className="stack">
          {logs.map((log) => (
            <article className="timeline-item" key={log.id}>
              <strong>{log.action}</strong>
              <p>{log.details}</p>
              <span className="muted">
                {log.actorEmail} · {new Date(log.createdAt).toLocaleString()}
              </span>
            </article>
          ))}
        </div>
      </section>
    </div>
  );
}

export default Reports;
