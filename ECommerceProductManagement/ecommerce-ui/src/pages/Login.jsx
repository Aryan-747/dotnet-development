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
                  <h1 style={{ color: 'white'}}>Sign In To The Product Management Portal</h1>
          <p>
            Use the seeded users or your own signup account to manage catalog, workflow,
            and reporting features.
          </p>
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
