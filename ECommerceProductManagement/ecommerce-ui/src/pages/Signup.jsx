import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../services/api";

const initialForm = {
  name: "",
  email: "",
  password: "",
  confirmPassword: "",
  role: "ProductManager",
};

function Signup() {
  const [form, setForm] = useState(initialForm);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const navigate = useNavigate();

  const onChange = (event) => {
    const { name, value } = event.target;
    setForm((current) => ({ ...current, [name]: value }));
  };

  const onSubmit = async (event) => {
    event.preventDefault();
    setError("");
    setMessage("");

    if (form.password !== form.confirmPassword) {
      setError("Password and confirm password must match.");
      return;
    }

    setSubmitting(true);

    try {
      await api.post("/auth/signup", {
        name: form.name,
        email: form.email,
        password: form.password,
        role: form.role,
      });

      setMessage("Account created. Redirecting to login...");
      setTimeout(() => navigate("/auth/login"), 900);
    } catch (requestError) {
      setError(requestError?.response?.data || "Unable to create account.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="auth-page">
      <div className="auth-card">
        <div className="auth-copy">
          <p className="eyebrow">Internal User Onboarding</p>
          <h1>Create your operations account</h1>
          <p>
            Set up access for Admin, Product Manager, or Content Executive workflows.
          </p>
        </div>

        <form className="auth-form" onSubmit={onSubmit}>
          <label>
            Full name
            <input name="name" value={form.name} onChange={onChange} required />
          </label>

          <label>
            Email
            <input
              name="email"
              type="email"
              value={form.email}
              onChange={onChange}
              required
            />
          </label>

          <label>
            Role
            <select name="role" value={form.role} onChange={onChange}>
              <option value="Admin">Admin</option>
              <option value="ProductManager">Product Manager</option>
              <option value="ContentExecutive">Content Executive</option>
            </select>
          </label>

          <label>
            Password
            <input
              name="password"
              type="password"
              value={form.password}
              onChange={onChange}
              required
            />
          </label>

          <label>
            Confirm password
            <input
              name="confirmPassword"
              type="password"
              value={form.confirmPassword}
              onChange={onChange}
              required
            />
          </label>

          {message ? <p className="banner success">{message}</p> : null}
          {error ? <p className="banner error">{error}</p> : null}

          <button type="submit" disabled={submitting}>
            {submitting ? "Creating..." : "Create account"}
          </button>

          <p className="muted">
            Already have access? <Link to="/auth/login">Go to login</Link>
          </p>
        </form>
      </div>
    </div>
  );
}

export default Signup;
