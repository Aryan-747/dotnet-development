export interface UserProfile {
  id: string;
  name: string;
  email: string;
  role: string;
}

export interface AuthResponse {
  token?: string;
  user?: UserProfile;
  requiresOtp?: boolean;
  email?: string;
}

export interface Product {
  id: string;
  name: string;
  sku: string;
  brand: string;
  description: string;
  categoryName: string;
  seoTitle: string;
  seoDescription: string;
  tags: string;
  primaryImageUrl: string;
  sellingPrice: number;
  stockQuantity: number;
  isPublished: boolean;
  createdAt: string;
  updatedAt: string;
  status: string | number;
}

export interface WorkflowSnapshot {
  price?: {
    mrp: number;
    sellingPrice: number;
    updatedAt: string;
  };
  inventory?: {
    quantity: number;
    availabilityMessage: string;
    updatedAt: string;
  };
  approval?: {
    status: string;
    remarks: string;
    requestedBy?: string;
    updatedAt?: string;
  };
}

export interface CartItem extends Product {
  quantity: number;
}

export interface DashboardSummary {
  totalActivities: number;
  auditsToday: number;
  pendingAlerts: number;
  recentActivities: AuditLog[];
}

export interface AuditLog {
  id: string;
  action: string;
  details: string;
  actorEmail: string;
  createdAt: string;
}

export interface ApprovalQueueItem {
  id: string;
  productId: string;
  status: string;
  remarks: string;
  requestedBy?: string;
  updatedAt?: string;
}

export interface UserAddress {
  id?: string;
  userId?: string;
  fullName: string;
  streetAddress: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  phoneNumber: string;
  isDefault: boolean;
}

export interface OrderItem {
  id?: string;
  orderId?: string;
  productId: string;
  productName: string;
  quantity: number;
  unitPrice: number;
}

export interface Order {
  id: string;
  userId: string;
  userEmail: string;
  createdAt: string;
  updatedAt: string;
  status: string;
  totalAmount: number;
  shippingAddress: UserAddress;
  items: OrderItem[];
}

