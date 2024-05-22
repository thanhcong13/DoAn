<script setup>
import { onMounted, ref } from "vue";
// import { userInfo } from "@/views/Login.vue";
// import { profile } from "./data";
import { useRouter } from "vue-router";
const router = useRouter();
const userProfile = ref();
const user = localStorage.getItem("username");
const logOut = () => {
  router.push("login");
  window.localStorage.removeItem("token");
};
onMounted(() => {
  userProfile.value = localStorage.getItem("token");
});
</script>

<template>
  <div>
    <!-- ---------------------------------------------- -->
    <!-- User Profile -->
    <!-- ---------------------------------------------- -->
    <v-menu anchor="bottom end" origin="auto" min-width="300">
      <template v-slot:activator="{ props }">
        <v-btn
          v-bind="props"
          class="pa-0 px-1"
          elevation="0"
          color="transparent"
          plain
          :ripple="false"
        >
          <span style="color: black">Thành viên quản trị: {{ user }}</span>
        </v-btn>
      </template>

      <v-list class="pa-6" elevation="10" rounded="lg" @submit.prevent="logOut">
        <v-btn
          @click="logOut"
          type="submit"
          block
          color="secondary"
          variant="contained"
          class="mt-4 py-4"
          >Đăng xuất</v-btn
        >
      </v-list>
    </v-menu>
  </div>
</template>
