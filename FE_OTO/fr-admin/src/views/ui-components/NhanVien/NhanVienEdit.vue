<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Cập nhật</span>
        </v-card-title>
        <v-card-text
          style="
            max-height: 1000px;
            min-width: 800px;
            overflow-x: auto;
            overflow-y: scroll;
          "
        >
          <v-row>
            <v-col cols="4">
              <v-combobox
                color="primary"
                v-model="tenCH"
                label="Tên cửa hàng"
                :items="listCH"
              ></v-combobox>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="tenNV"
                required
                label="Tên nhân viên"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <p>Ngày sinh</p>
              <VueDatePicker v-model="ngaySinh"></VueDatePicker>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="gioiTinh"
                required
                label="Giới tính"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="soDT"
                required
                label="Số điện thoại"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="chucVu"
                required
                label="Chức vụ"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-text-field
                color="primary"
                v-model="diaChi"
                required
                label="Địa chỉ"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="errorr" variant="text" @click="handleClose"> Đóng </v-btn>
          <v-btn color="success" variant="text" @click="handleUpdate"> Thêm </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script setup>
import { ref, defineProps, watch } from "vue";
import axios from "axios";
const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["update", "close"]);
const tenCH = ref("");
const tenNV = ref("");
const ngaySinh = ref(Date);
const gioiTinh = ref("");
const diaChi = ref("");
const soDT = ref("");
const chucVu = ref("");
const listCH = ref([]);
const maNV = ref("");
watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      (tenCH.value = props.data.tenCH),
        (tenNV.value = props.data.tenNV),
        (ngaySinh.value = props.data.ngaySinh),
        (gioiTinh.value = props.data.gioiTinh),
        (diaChi.value = props.data.diaChi),
        (soDT.value = props.data.soDT),
        (chucVu.value = props.data.chucVu),
        (maNV.value = props.data.maNV);
    }
  }
);
const handleUpdate = async () => {
  try {
    const entity = {
      maNV: maNV.value,
      tenNV: tenNV.value,
      ngaySinh: ngaySinh.value,
      gioiTinh: gioiTinh.value,
      diaChi: diaChi.value,
      soDT: soDT.value,
      chucVu: chucVu.value,
      tenCH: tenCH.value,
    };
    const res = await axios.put(
      "http://localhost:5222/api/NhanVien/UpdateNhanVien/" + props.data?.maNV,
      entity
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
<style scoped>
.text-uppercase {
  text-transform: uppercase !important;
}
</style>
