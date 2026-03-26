import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:5000/gateway",
});

api.interceptors.request.use((config) => {
  const rawSession = localStorage.getItem("ecommerce-auth");
  const token = rawSession ? JSON.parse(rawSession)?.token : "";

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default api;
