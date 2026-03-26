import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../services/api";

function Workflow() {
  const [queue, setQueue] = useState([]);

  useEffect(() => {
    api.get("/workflow/queue").then(({ data }) => setQueue(data));
  }, []);

  return (
    <div className="page page-spacious">
      <div className="section-header">
        <div>
          <p className="eyebrow">Approval Queue</p>
          <h1>Review and publish pipeline</h1>
        </div>
      </div>

      <div className="stack">
        {queue.map((item) => (
          <article className="card timeline-item" key={item.id}>
            <strong>{item.status}</strong>
            <p>{item.remarks || "Awaiting reviewer notes."}</p>
            <p className="muted">Requested by: {item.requestedBy || "Unknown"}</p>
            <Link className="ghost-link" to={`/admin/products/${item.productId}`}>
              Open product workspace
            </Link>
          </article>
        ))}
      </div>
    </div>
  );
}

export default Workflow;
