import { Link } from "react-router-dom";

function Footer() {
  return (
    <footer className="site-footer">
      <button
        className="footer-top-link"
        onClick={() => window.scrollTo({ top: 0, behavior: "smooth" })}
        type="button"
      >
        Back to top
      </button>

      <div className="footer-main">
        <div className="footer-brand">
          <div className="brand-lockup">
            <span className="brand-badge">epm</span>
            <div>
              <h3>ShopSphere</h3>
              <p className="muted">
                Product discovery, preview cart, seller operations, and reports in one
                ecommerce workspace.
              </p>
            </div>
          </div>
        </div>

        <div className="footer-columns">
          <div className="footer-column">
            <h4>Shop</h4>
            <Link to="/customer/products">All Products</Link>
            <Link to="/customer/products?category=Electronics">Electronics</Link>
            <Link to="/customer/products?category=Fashion">Fashion</Link>
            <Link to="/customer/cart">Cart</Link>
          </div>

          <div className="footer-column">
            <h4>Seller</h4>
            <Link to="/admin/products">Seller Console</Link>
            <Link to="/admin/dashboard">Dashboard</Link>
            <Link to="/admin/workflow">Workflow</Link>
            <Link to="/admin/reports">Reports</Link>
          </div>

          <div className="footer-column">
            <h4>Support</h4>
            <a href="mailto:support@shopsphere.local">support@shopsphere.local</a>
            <a href="tel:+919999999999">+91 99999 99999</a>
            <a href="https://localhost:5000/gateway/catalog/products/preview">
              API Preview
            </a>
            <Link to="/auth/login">Account Access</Link>
          </div>

          <div className="footer-column">
            <h4>Why ShopSphere</h4>
            <p>Fast search-driven browsing</p>
            <p>Live preview cart</p>
            <p>Structured product lifecycle</p>
            <p>Role-based operational controls</p>
          </div>
        </div>
      </div>

      <div className="footer-bottom">
        <p>© 2026 ShopSphere. Crafted for ecommerce product management workflows.</p>
        <div className="footer-meta-links">
          <span>Privacy</span>
          <span>Terms</span>
          <span>Accessibility</span>
        </div>
      </div>
    </footer>
  );
}

export default Footer;
