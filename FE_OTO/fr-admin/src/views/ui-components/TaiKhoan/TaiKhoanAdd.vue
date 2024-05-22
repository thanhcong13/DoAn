<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Thêm mới</span>
        </v-card-title>
        <v-card-text
          style="
            max-height: 600px;
            min-width: 800px;
            overflow-x: auto;
            overflow-y: scroll;
          "
        >
          <v-row>
            <v-col cols="4">
              <v-combobox
                color="primary"
                v-model="MaNV"
                label="Mã nhân viên"
                :items="listEmployee"
              ></v-combobox>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="tenTaiKhoan"
                required
                label="Tài khoản"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="matKhau"
                required
                label="Mật khẩu"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="errorr" variant="text" @click="handleClose"> Đóng </v-btn>
          <v-btn color="success" variant="text" @click="handleAdd"> Thêm </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script setup>
import axios from "axios";
import { ref, defineProps, onMounted } from "vue";
const props = defineProps({
  isOpen: Boolean,
  data: Object,
});
const emit = defineEmits(["add", "close"]);
const MaNV = ref("");
const tenTaiKhoan = ref("");
const matKhau = ref("");
const listEmployee = ref();
const handleClose = () => {
  emit("close");
};

const getAllCH = async () => {
  const res = await axios.get("http://localhost:5222/api/TaiKhoan/GetAllEmployee");
  listEmployee.value = res.data.innerBody.map((item) => item.MaNV);
};

onMounted(() => {
  getAllCH();
});
const handleAdd = async () => {
  try {
    const entity = {
     tenTaiKhoan: tenTaiKhoan.value,
     matKhau: matKhau.value,
     MaNV: MaNV.value
    };
    const res = await axios.post("http://localhost:5222/api/TaiKhoan/Post", entity);
    MaNV.value = "";
    tenTaiKhoan.value = "";
    matKhau.value = "";
    emit("add");
  } catch (error) {
    console.log(error);
  }
};
</script>

<style>
.v-dialog .v-overlay__content {
  max-width: 1000px !important;
  max-height: 800px !important;
}
</style>
