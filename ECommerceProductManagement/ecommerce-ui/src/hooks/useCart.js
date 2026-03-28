import { useEffect, useState } from "react";

const CART_STORAGE_KEY = "preview-cart";
const CART_EVENT = "preview-cart-updated";

function readCart() {
  const raw = localStorage.getItem(CART_STORAGE_KEY);
  return raw ? JSON.parse(raw) : [];
}

function writeCart(items) {
  localStorage.setItem(CART_STORAGE_KEY, JSON.stringify(items));
  window.dispatchEvent(new Event(CART_EVENT));
}

export function addCartItem(product) {
  const items = readCart();
  const existing = items.find((item) => item.id === product.id);

  if (existing) {
    existing.quantity += 1;
  } else {
    items.push({ ...product, quantity: 1 });
  }

  writeCart(items);
}

export function removeCartItem(productId) {
  const items = readCart().filter((item) => item.id !== productId);
  writeCart(items);
}

export function decreaseCartItem(productId) {
  const items = readCart()
    .map((item) =>
      item.id === productId ? { ...item, quantity: Math.max(0, item.quantity - 1) } : item
    )
    .filter((item) => item.quantity > 0);

  writeCart(items);
}

export function useCart() {
  const [items, setItems] = useState(readCart);

  useEffect(() => {
    const syncCart = () => setItems(readCart());

    window.addEventListener("storage", syncCart);
    window.addEventListener(CART_EVENT, syncCart);

    return () => {
      window.removeEventListener("storage", syncCart);
      window.removeEventListener(CART_EVENT, syncCart);
    };
  }, []);

  const totalItems = items.reduce((sum, item) => sum + Number(item.quantity || 0), 0);
  const totalPrice = items.reduce(
    (sum, item) => sum + Number(item.sellingPrice || 0) * Number(item.quantity || 0),
    0
  );

  return {
    items,
    totalItems,
    totalPrice,
    removeItem: removeCartItem,
    decreaseItem: decreaseCartItem,
  };
}
