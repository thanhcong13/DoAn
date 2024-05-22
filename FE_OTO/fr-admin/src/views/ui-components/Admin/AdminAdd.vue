<script setup>
import { ref, defineProps } from "vue";
import axios from "../../../services/axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["add", "close"]);

const username = ref("");
const password = ref("");
const phone = ref("");
const address = ref("");
const fullName = ref("");
const email = ref("");
const role = ref(["admin"]);

const handleAdd = async () => {
  try {
    const dataAdd = {
      username: username.value,
      password: password.value,
      phone: phone.value,
      address: address.value,
      fullName: fullName.value,
      email: email.value,
      role: role.value,
    };
    // console.log(service.value.map((item) => item.service).join(","));
    const res = await axios.post(
      "http://localhost:8080/api/auth/signup",
      dataAdd
    );
    emit("add");
  } catch (e) {
    console.log(e);
  }
};
const handleAddClose = () => {
  emit("close");
};
</script>
<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Thêm mới</span>
        </v-card-title>
        <v-card-text
          style="max-height: 400px min-width: 800px; overflow-x: auto"
        >
          <v-container>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  v-model="username"
                  required
                  label="Tên đăng nhập"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  v-model="password"
                  required
                  type="password"
                  label="Mật khẩu"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  label="Họ và tên"
                  v-model="fullName"
                  required="không được để trống!!"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  v-model="phone"
                  label="Điện thoại"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="address"
                  label="Địa chỉ"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="email"
                  label="Email"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="errorr" variant="text" @click="handleAddClose">
            Đóng
          </v-btn>
          <v-btn color="success" variant="text" @click="handleAdd">
            Thêm
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<style>
.text-uppercase {
  text-transform: uppercase !important;
}
.v-dialog .v-overlay__content {
  max-height: 600px;
  overflow-y: scroll !important;
  max-width: 700px;
}
.v-overlay-scroll-blocked {
  padding-inline-end: 0;
}
.mdi-paperclip::before {
  content: "\F03E2";
  display: none;
}
</style>
