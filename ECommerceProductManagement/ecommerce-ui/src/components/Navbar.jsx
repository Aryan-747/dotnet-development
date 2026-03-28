import { useState } from "react";
import { Link, useLocation, useNavigate, useSearchParams } from "react-router-dom";
import { useCart } from "../hooks/useCart";
import { useAuth } from "../auth/useAuth";
import "../styles/main.css";

function Navbar() {
  const { isAuthenticated, user, logout } = useAuth();
  const { totalItems } = useCart();
  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [searchText, setSearchText] = useState(searchParams.get("q") ?? "");
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const onLogout = () => {
    setIsMenuOpen(false);
    logout();
    navigate("/auth/login");
  };

  const isActive = (path) => location.pathname.startsWith(path);
  const isCustomerView = location.pathname.startsWith("/customer");

  const onSearchSubmit = (event) => {
    event.preventDefault();
    const nextQuery = searchText.trim();
    const nextParams = new URLSearchParams();

    if (nextQuery) {
      nextParams.set("q", nextQuery);
    }

    navigate({
      pathname: "/customer/products",
      search: nextParams.toString() ? `?${nextParams.toString()}` : "",
    });
  };

  return (
    <>
      <nav className="navbar storefront-nav">
        <div className="nav-brand">
          <Link className="brand-lockup" to="/customer/products">
            <span className="brand-badge">epm</span>
            <div>
              <h2>ShopSphere</h2>
            </div>
           </Link>
          </div>

        <form className="nav-search-shell" onSubmit={onSearchSubmit}>
          <span className="nav-search-tag">All</span>
          <input
            className="nav-search"
            onChange={(event) => setSearchText(event.target.value)}
            placeholder="Search products, brands and departments"
            value={searchText}
          />
          <button className="nav-search-tag search-cta nav-search-button" type="submit">
            Search
          </button>
        </form>

        <div className="nav-links nav-links-utility">
          <Link className={isActive("/customer") ? "active" : ""} to="/customer/products">
            Storefront
          </Link>
          <Link className={isActive("/customer/cart") ? "active cart-link" : "cart-link"} to="/customer/cart">
            Cart
            {totalItems > 0 ? <span className="cart-count">{totalItems}</span> : null}
          </Link>

          {isAuthenticated ? (
            <>
              <Link className={isActive("/admin/products") ? "active" : ""} to="/admin/products">
                Seller Console
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
              <div className="account-menu">
                <button
                  aria-expanded={isMenuOpen}
                  className="nav-button account-trigger"
                  onClick={() => setIsMenuOpen((current) => !current)}
                  type="button"
                >
                  {user?.name}
                  <span className="account-caret">{isMenuOpen ? "▲" : "▼"}</span>
                </button>

                {isMenuOpen ? (
                  <div className="account-dropdown">
                    <p className="account-name">{user?.name}</p>
                    <p className="account-role">{user?.role}</p>
                    <button className="account-dropdown-button" onClick={onLogout} type="button">
                      Logout
                    </button>
                  </div>
                ) : null}
              </div>
            </>
          ) : (
            <>
              <Link className={isActive("/auth/login") ? "active" : ""} to="/auth/login">
                Sign in
              </Link>
              <Link className={isActive("/auth/signup") ? "active" : ""} to="/auth/signup">
                Register
              </Link>
            </>
          )}
        </div>
      </nav>

      {isCustomerView ? (
        <div className="subnav-bar">
          <Link to="/customer/products">Today's Deals</Link>
          <Link to="/customer/products?category=Electronics">Electronics</Link>
          <Link to="/customer/products?category=Fashion">Fashion</Link>
          <Link to="/customer/products?category=Kitchen">Home & Kitchen</Link>
          <Link to="/customer/products?category=Fitness">Fitness</Link>
          <Link to="/customer/products?q=featured">Prime Preview</Link>
        </div>
      ) : null}
    </>
  );
}

export default Navbar;
