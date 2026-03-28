import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./auth/AuthContext";
import Footer from "./components/Footer";
import Navbar from "./components/Navbar";
import ProtectedRoute from "./components/ProtectedRoute";
import CartPreview from "./pages/CartPreview";
import CustomerProducts from "./pages/CustomerProducts";
import Dashboard from "./pages/Dashboard";
import Login from "./pages/Login";
import ProductDetails from "./pages/ProductDetails";
import ProductList from "./pages/ProductList";
import Reports from "./pages/Reports";
import Signup from "./pages/Signup";
import Workflow from "./pages/Workflow";

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/" element={<Navigate to="/customer/products" replace />} />
          <Route path="/auth/login" element={<Login />} />
          <Route path="/auth/signup" element={<Signup />} />
          <Route path="/customer/products" element={<CustomerProducts />} />
          <Route path="/customer/products/:id" element={<ProductDetails mode="customer" />} />
          <Route path="/customer/cart" element={<CartPreview />} />

          <Route
            element={
              <ProtectedRoute roles={["Admin", "ProductManager", "ContentExecutive"]} />
            }
          >
            <Route path="/admin/products" element={<ProductList />} />
            <Route path="/admin/products/:id" element={<ProductDetails mode="admin" />} />
          </Route>

          <Route element={<ProtectedRoute roles={["Admin"]} />}>
            <Route path="/admin/dashboard" element={<Dashboard />} />
            <Route path="/admin/workflow" element={<Workflow />} />
            <Route path="/admin/reports" element={<Reports />} />
          </Route>
        </Routes>
        <Footer />
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
