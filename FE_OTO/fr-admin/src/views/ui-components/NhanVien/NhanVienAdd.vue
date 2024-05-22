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
const tenCH = ref("");
const tenNV = ref("");
const ngaySinh = ref(Date);
const gioiTinh = ref("");
const diaChi = ref(""); 
const soDT = ref("");
const chucVu = ref("");
const listCH = ref([]);
const maNV = ref("");
const employeeCode = ref();
const handleClose = () => {
  emit("close");
};

const getAllCH = async () => {
  const res = await axios.get("http://localhost:5222/api/SanPham/GetAllCH");
  listCH.value = res.data.innerBody.map((item) => item.TenCH);
};
const getEmployeeCode = async () => {
  const res = await axios.get("http://localhost:5222/api/NhanVien/GetEmployeeCode");
  console.log(res.data.innerBody);
  if(res.data.innerBody.length === 0){
    maNV.value = "NV01"
  }
  else{
    maNV.value = res.data.innerBody[0].MaNV;
    const eNum = maNV.value.slice(2);
    const numConvert = parseInt(eNum,10);
    if(numConvert < 10) {
      maNV.value = 'NV0' + (numConvert + 1).toString();
    }
    else{
      maNV.value = 'NV' + (numConvert + 1).toString();
    }
    console.log(maNV.value);
  }
};
onMounted(() => {
  getAllCH();
  getEmployeeCode();
});
const handleAdd = async () => {
  try {
    const entity = {
      maNV: maNV.value,
      tenNV: tenNV.value,
      ngaySinh: ngaySinh.value,
      gioiTinh: gioiTinh.value,
      diaChi: diaChi.value,
      soDT: soDT.value,
      chucVu: chucVu.value,
      tenCH: tenCH.value
    };
    const res = await axios.post("http://localhost:5222/api/NhanVien/Post", entity);
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
