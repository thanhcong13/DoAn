<script setup>
import { ref, defineProps, watch } from "vue";
import axios from "axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["update", "close"]);

const fullName = ref("");
const userName = ref("");
const phone = ref("");
const address = ref("");
const email = ref("");

watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      fullName.value = props.data?.fullName;
      userName.value = props.data?.username;
      phone.value = props.data?.phone;
      address.value = props.data?.address;
      email.value = props.data?.email;
    }
  }
);

const handleUpdate = async () => {
  try {
    const dataUpdate = {
      fullName: fullName.value,
      username: userName.value,
      phone: phone.value,
      address: address.value,
      email: email.value,
      id: props.data?.id,
    };
    const res = await axios.put(
      "http://localhost:8080/api/v1/user/update/" + props.data?.id,
      dataUpdate
    );
    emit("update");
  } catch (e) {
    console.log(e);
  }
};
const handleClose = () => {
  emit("close");
};
</script>
<template>
  <v-dialog v-model="isOpen" width="auto" persistent>
    <v-card>
      <v-card-title>
        <span class="text-h5 text-uppercase">Cập nhật</span>
      </v-card-title>
      <v-card-text style="max-height: 400px; overflow-x: auto">
        <v-container>
          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model="userName"
                required
                label="Tài khoản"
              ></v-text-field>
            </v-col>
            <v-col cols="6">
              <v-text-field
                label="Họ và tên"
                v-model="fullName"
                required="không được để trống!!"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="phone"
                label="Điện thoại"
                hint="example of persistent helper text"
                persistent-hint
                required
              ></v-text-field>
            </v-col>

            <v-col cols="12">
              <v-text-field
                v-model="address"
                label="Địa chỉ"
                hint="example of persistent helper text"
                persistent-hint
                required
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="email"
                label="Email*"
                disabled
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="errorr" variant="text" @click="handleClose"> Đóng </v-btn>
        <v-btn color="success" variant="text" @click="handleUpdate">
          Cập nhật
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<style scoped>
.text-uppercase {
  text-transform: uppercase !important;
}
</style>
