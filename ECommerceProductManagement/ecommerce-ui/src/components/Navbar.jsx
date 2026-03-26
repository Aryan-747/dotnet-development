import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import "../styles/main.css";

function Navbar() {
  const { isAuthenticated, user, logout } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const onLogout = () => {
    logout();
    navigate("/auth/login");
  };

  const isActive = (path) => location.pathname.startsWith(path);

  return (
    <nav className="navbar">
      <div>
        <p className="eyebrow">ECommerce Product Management</p>
        <h2>Lifecycle Console</h2>
      </div>

      <div className="nav-links">
        <Link className={isActive("/customer") ? "active" : ""} to="/customer/products">
          Storefront
        </Link>

        {isAuthenticated ? (
          <>
            <Link className={isActive("/admin/products") ? "active" : ""} to="/admin/products">
              Products
            </Link>
            {user?.role === "Admin" ? (
              <>
                <Link
                  className={isActive("/admin/dashboard") ? "active" : ""}
                  to="/admin/dashboard"
                >
                  Dashboard
                </Link>
                <Link
                  className={isActive("/admin/workflow") ? "active" : ""}
                  to="/admin/workflow"
                >
                  Workflow
                </Link>
                <Link
                  className={isActive("/admin/reports") ? "active" : ""}
                  to="/admin/reports"
                >
                  Reports
                </Link>
              </>
            ) : null}
            <button className="nav-button" onClick={onLogout} type="button">
              {user?.name} · Logout
            </button>
          </>
        ) : (
          <>
            <Link className={isActive("/auth/login") ? "active" : ""} to="/auth/login">
              Login
            </Link>
            <Link className={isActive("/auth/signup") ? "active" : ""} to="/auth/signup">
              Signup
            </Link>
          </>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
