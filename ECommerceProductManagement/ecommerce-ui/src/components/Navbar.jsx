import { Link } from "react-router-dom";
import "../styles/main.css";

function Navbar() {
  return (
    <nav className="navbar">
      <h2>Ecommerce</h2>
      <div>
        <Link to="/dashboard">Dashboard</Link>
        <Link to="/products">Products</Link>
        <Link to="/workflow">Workflow</Link>
        <Link to="/reports">Reports</Link>
      </div>
    </nav>
  );
}

export default Navbar;
