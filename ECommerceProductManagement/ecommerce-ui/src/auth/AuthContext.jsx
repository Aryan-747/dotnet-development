import { useEffect, useState } from "react";
import api from "../services/api";
import { AUTH_STORAGE_KEY, AuthContext } from "./auth-context-value";

export function AuthProvider({ children }) {
  const [session, setSession] = useState(() => {
    const raw = localStorage.getItem(AUTH_STORAGE_KEY);
    return raw ? JSON.parse(raw) : { token: "", user: null };
  });
  const [ready, setReady] = useState(false);
  const sessionToken = session?.token ?? "";

  useEffect(() => {
    const restoreSession = async () => {
      if (!sessionToken) {
        setReady(true);
        return;
      }

      try {
        const { data } = await api.get("/auth/me");
        const next = { token: sessionToken, user: data };
        setSession(next);
        localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(next));
      } catch {
        localStorage.removeItem(AUTH_STORAGE_KEY);
        setSession({ token: "", user: null });
      } finally {
        setReady(true);
      }
    };

    restoreSession();
  }, [sessionToken]);

  const login = (payload) => {
    const next = { token: payload.token, user: payload.user };
    setSession(next);
    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(next));
  };

  const logout = () => {
    localStorage.removeItem(AUTH_STORAGE_KEY);
    setSession({ token: "", user: null });
  };

  return (
    <AuthContext.Provider
      value={{
        ready,
        token: session?.token ?? "",
        user: session?.user ?? null,
        isAuthenticated: Boolean(session?.token),
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}
