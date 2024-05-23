import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  //history: createWebHistory(import.meta.env.BASE_URL),
  history: createWebHistory("/"),
  routes: [
    {
      path: "/",
      redirect: "/dashboard",
      component: () => import("@/layouts/full/FullLayout.vue"),

      children: [
        {
          name: "Dashboard",
          path: "dashboard",
          component: () => import("@/views/dashboard/Dashboard.vue"),
        },

        {
          name: "Customers",
          path: "admin/customers",
          component: () => import("@/views/ui-components/Customers.vue"),
        },
        {
          name: "Blog",
          path: "admin/blog",
          component: () => import("@/views/ui-components/Blogs.vue"),
        },

        {
          name: "Hotels",
          path: "admin/hotels",
          component: () => import("@/views/ui-components/Hotels.vue"),
        },
        {
          name: "Admins",
          path: "admin/admins",
          component: () => import("@/views/ui-components/Admin.vue"),
        },
        {
          name: "Orders",
          path: "admin/orders",
          component: () => import("@/views/ui-components/Orders.vue"),
        },
        {
          path: "admin/hotels/:id",
          name: "Hotel",
          component: () => import("@/views/ui-components/Room/RoomIndex.vue"),
        },
        {
          path: "admin/user",
          name: "Room",
          component: () => import("@/views/ui-components/Admin/AdminIndex.vue"),
        },
        {
          path: "admin/brands",
          name: "Brand",
          component: () => import("@/views/ui-components/Brand/BrandIndex.vue"),
        },
        {
          path: "/admin/products",
          name: "Products",
          component: () => import("@/views/ui-components/Sanpham/SanphamIndex.vue"),
        },
        {
          path: "admin/stores",
          name: "Stores",
          component: () => import("@/views/ui-components/Cuahang/CuahangIndex.vue"),
        },
        {
          path: "admin/contacts",
          name: "Contacts",
          component: () => import("@/views/ui-components/LienHe/LienHeIndex.vue"),
        },
        {
          path: "admin/maintenances",
          name: "Maintenances",
          component: () => import("@/views/ui-components/BaoDuong/BaoDuongIndex.vue"),
        },
        {
          path: "admin/employees",
          name: "Employees",
          component: () => import("@/views/ui-components/NhanVien/NhanVienIndex.vue"),
        },
        {
          path: "admin/accounts",
          name: "Accounts",
          component: () => import("@/views/ui-components/TaiKhoan/TaiKhoanIndex.vue"),
        },
        {
          path: "admin/laithus",
          name: "LaiThus",
          component: () => import("@/views/ui-components/LaiThu/LaiThuIndex.vue"),
        },
      ],
    },
    {
      name: "Login",
      path: "/login",
      component: () => import("@/views/Login.vue"),
    },
  ],
});
router.beforeEach(async (to, from) => {
  const isAuthenticated = localStorage.getItem("username");
  console.log(isAuthenticated);

  if (
    // make sure the user is authenticated
    !isAuthenticated &&
    // Avoid an infinite redirect
    to.name !== "Login"
  ) {
    // redirect the user to the login page
    return { name: "Login" };
  }
});
export default router;
