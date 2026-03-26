import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../auth/useAuth";

function ProtectedRoute({ roles = [] }) {
  const { ready, isAuthenticated, user } = useAuth();

  if (!ready) {
    return <div className="screen-message">Restoring your session...</div>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/auth/login" replace />;
  }

  if (roles.length > 0 && !roles.includes(user?.role)) {
    return <Navigate to="/customer/products" replace />;
  }

  return <Outlet />;
}

export default ProtectedRoute;
