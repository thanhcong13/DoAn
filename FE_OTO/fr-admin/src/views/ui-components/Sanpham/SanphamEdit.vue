<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Cập nhật thông tin</span>
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
          <v-col cols="12">
            <v-file-input
              color="primary"
              v-model="image"
              label="File input"
              ref="file"
              @change="selectFile"
            ></v-file-input>
            <div v-if="isShowImage === true">
              <img style="width: 100px; height: 140px" :src="imgUrl" alt="" />
            </div>
          </v-col>
        </v-row>
          <v-row>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="tenSP"
                required
                label="Tên sản phẩm"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-combobox
                color="primary"
                v-model="tenHang"
                label="Tên hãng"
                :items="listHang"
              ></v-combobox>
            </v-col>
            <v-col cols="4">
              <v-combobox
                color="primary"
                v-model="tenCH"
                label="Tên cửa hàng"
                :items="listCH"
              ></v-combobox>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="4">
              <p>Năm sản xuất</p>
              <VueDatePicker v-model="namSX"></VueDatePicker>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="mauSac"
                required
                label="Màu sắc"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="soDam"
                required
                label="Số dặm"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="dongCo"
                required
                label="Động cơ"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="soGhe"
                required
                label="Số ghế"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="tinhTrang"
                required
                label="Tình trạng"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="dungTich"
                required
                label="Dung tích"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="loaiHopSo"
                required
                label="Loại hộp số"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <p>Năm sản xuất</p>
              <VueDatePicker v-model="namDangKy"></VueDatePicker>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-text-field color="primary" v-model="moTa" required label="Mô tả">
              </v-text-field>
            </v-col>
          </v-row>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="errorr" variant="text" @click="handleClose"> Đóng </v-btn>
            <v-btn color="success" variant="text" @click="handleUpdate"> Cập nhật </v-btn>
          </v-card-actions>
        </v-card-text>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script setup>
import axios from "axios";
import { ref, defineProps, onMounted,watch } from "vue";
const props = defineProps({
  isOpen: Boolean,
  data: Object,
});
const emit = defineEmits(["update", "close"]);
const tenSP = ref("");
const tenHang = ref("");
const tenCH = ref("");
const namSX = ref(Date);
const namDangKy = ref(Date);
const mauSac = ref("");
const soDam = ref(0);
const dongCo = ref("");
const soGhe = ref(0);
const tinhTrang = ref("");
const dungTich = ref(0);
const loaiHopSo = ref("");
const moTa = ref("");
const listCH = ref([]);
const image = ref("");
let imgUrl = ref("");
const listHang = ref([]);
let isShowImage = ref(false);
const maSP = ref("");

watch(
    () => props.isOpen,
    (value) => {
      if (value) {
        maSP.value = props.data.maSP,
       tenSP.value = props.data.tenSP,
       tenHang.value = props.data.tenHang,
       tenCH.value = props.data.tenCH,
       namSX.value = props.data.namSX,
       namDangKy.value = props.data.namDangKy,
       mauSac.value = props.data.mauSac,
       soDam.value= props.data.soDam,
       dongCo.value = props.data.dongCo,
       soGhe.value = props.data.soGhe,
       tinhTrang.value = props.data.tinhTrang,
       dungTich.value = props.data.dungTich,
       loaiHopSo.value = props.data.loaiHopSo,
       moTa.value = props.data.moTa,
       imgUrl.value = props.data.image,
       isShowImage.value = true
      }
    }
  );
const handleClose = () => {
  emit("close");
};

const getAllCH = async () => {
  const res = await axios.get("http://localhost:5222/api/SanPham/GetAllCH");
  listCH.value = res.data.innerBody.map((item) => item.TenCH);
};
const getAllHang = async () => {
  const res = await axios.get("http://localhost:5222/api/SanPham/GetAllHang");
  listHang.value = res.data.innerBody.map((item) => item.TenHang);
};
onMounted(() => {
  getAllCH();
  getAllHang();
});
const handleUpdate = async () => {
  try {
    const entity = {
      maSP: maSP.value,
      tenSP: tenSP.value,
      moTa: moTa.value,
      mauSac: mauSac.value,
      namSX: namSX.value,
      namDangKy: namDangKy.value,
      soDam: soDam.value,
      dongCo: dongCo.value,
      soGhe: soGhe.value,
      dungTich: dungTich.value,
      loaiHopSo: loaiHopSo.value,
      tinhTrang: tinhTrang.value,
      tenHang: tenHang.value,
      tenCH: tenCH.value,
      image: imgUrl.value
    };
    const res = await axios.put("http://localhost:5222/api/SanPham/Put/" + maSP.value, entity);
    emit("update");
    
  } catch (error) {
    console.log(error);
  }
};
const selectFile = async (e) => {
  try {
    const img = new FormData();
    img.append("file", image.value[0]);
    const res = await axios.post("http://localhost:5222/api/SanPham/Upload", img);
    isShowImage.value = true;
    imgUrl.value = res.data.url;
    // image.value = imgUrl.value;
    console.log(isShowImage.value);
    console.log(imgUrl.value);
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
