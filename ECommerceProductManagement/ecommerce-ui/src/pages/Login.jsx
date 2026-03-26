import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import api from "../services/api";

function Login() {
  const [form, setForm] = useState({ email: "", password: "" });
  const [error, setError] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const onSubmit = async (event) => {
    event.preventDefault();
    setError("");
    setSubmitting(true);

    try {
      const { data } = await api.post("/auth/login", form);
      login(data);

      navigate(data.user.role === "Admin" ? "/admin/dashboard" : "/admin/products");
    } catch {
      setError("Invalid email or password.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="auth-page">
      <div className="auth-card">
        <div className="auth-copy">
          <p className="eyebrow">Secure JWT Access</p>
          <h1>Sign in to the product management portal</h1>
          <p>
            Use the seeded users or your own signup account to manage catalog, workflow,
            and reporting features.
          </p>
          <div className="card subtle-card">
            <p>Demo accounts:</p>
            <p>`admin@ecommerce.local` / `Admin@123`</p>
            <p>`pm@ecommerce.local` / `Product@123`</p>
            <p>`content@ecommerce.local` / `Content@123`</p>
          </div>
        </div>

        <form className="auth-form" onSubmit={onSubmit}>
          <label>
            Email
            <input
              name="email"
              type="email"
              value={form.email}
              onChange={(event) => setForm({ ...form, email: event.target.value })}
              required
            />
          </label>

          <label>
            Password
            <input
              name="password"
              type="password"
              value={form.password}
              onChange={(event) => setForm({ ...form, password: event.target.value })}
              required
            />
          </label>

          {error ? <p className="banner error">{error}</p> : null}

          <button type="submit" disabled={submitting}>
            {submitting ? "Signing in..." : "Login"}
          </button>

          <p className="muted">
            Need a new account? <Link to="/auth/signup">Create one</Link>
          </p>
        </form>
      </div>
    </div>
  );
}

export default Login;
