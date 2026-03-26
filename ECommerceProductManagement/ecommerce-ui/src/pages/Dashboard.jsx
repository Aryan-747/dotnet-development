import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../services/api";

function Dashboard() {
  const [summary, setSummary] = useState(null);
  const [products, setProducts] = useState([]);
  const [queue, setQueue] = useState([]);
  const [alerts, setAlerts] = useState([]);

  useEffect(() => {
    Promise.all([
      api.get("/admin/dashboard"),
      api.get("/catalog/products"),
      api.get("/workflow/queue"),
      api.get("/admin/alerts"),
    ]).then(([dashboardResponse, productsResponse, queueResponse, alertsResponse]) => {
      setSummary(dashboardResponse.data);
      setProducts(productsResponse.data);
      setQueue(queueResponse.data);
      setAlerts(alertsResponse.data);
    });
  }, []);

  if (!summary) {
    return <div className="screen-message">Loading admin dashboard...</div>;
  }

  const published = products.filter((product) => product.isPublished).length;

  return (
    <div className="page page-spacious">
      <section className="hero compact">
        <div>
          <p className="eyebrow">Home Product Console</p>
          <h1>Operational health at a glance</h1>
          <p>
            Track product readiness, approval workload, and recent audit signals from one
            admin landing page.
          </p>
        </div>
      </section>

      <section className="metric-grid">
        <article className="card metric-card">
          <span>Total products</span>
          <strong>{products.length}</strong>
        </article>
        <article className="card metric-card">
          <span>Published preview items</span>
          <strong>{published}</strong>
        </article>
        <article className="card metric-card">
          <span>Approval queue</span>
          <strong>{queue.length}</strong>
        </article>
        <article className="card metric-card">
          <span>Audit activity today</span>
          <strong>{summary.auditsToday}</strong>
        </article>
      </section>

      <div className="split-grid">
        <section className="card">
          <div className="section-header">
            <div>
              <p className="eyebrow">Quick links</p>
              <h2>Action board</h2>
            </div>
          </div>
          <div className="stack">
            <Link className="action-link" to="/admin/products">
              Manage product catalog
            </Link>
            <Link className="action-link" to="/admin/workflow">
              Review approval queue
            </Link>
            <Link className="action-link" to="/admin/reports">
              Open reports and exports
            </Link>
          </div>
        </section>

        <section className="card">
          <div className="section-header">
            <div>
              <p className="eyebrow">Recent activity</p>
              <h2>Audit feed</h2>
            </div>
          </div>
          <div className="stack">
            {summary.recentActivities.map((activity) => (
              <article className="timeline-item" key={activity.id}>
                <strong>{activity.action}</strong>
                <p>{activity.details}</p>
                <span className="muted">{activity.actorEmail}</span>
              </article>
            ))}
          </div>
        </section>
      </div>

      <section className="card">
        <div className="section-header">
          <div>
            <p className="eyebrow">Alert panel</p>
            <h2>Pending operational signals</h2>
          </div>
        </div>

        <div className="stack">
          {alerts.length === 0 ? <p className="muted">No alerts need attention.</p> : null}
          {alerts.map((alert) => (
            <article className="timeline-item" key={alert.id}>
              <strong>{alert.action}</strong>
              <p>{alert.details}</p>
              <span className="muted">{new Date(alert.createdAt).toLocaleString()}</span>
            </article>
          ))}
        </div>
      </section>
    </div>
  );
}

export default Dashboard;
