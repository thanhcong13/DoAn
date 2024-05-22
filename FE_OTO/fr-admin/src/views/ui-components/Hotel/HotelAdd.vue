<script setup>
import { ref, defineProps } from "vue";
import axios from "../../../services/axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["add", "close"]);

const hotelName = ref("");
const hotelType = ref("");
const phone = ref("");
const address = ref("");
const fromPrice = ref("");
const rated = ref();
const description = ref("");
const images = ref("");
const imgUrls = ref([]);
const service = ref(["Wifi", "Nóng lạnh", "Điều hòa"]);
const items = ref([
  "Bể bơi",
  "Bãi đỗ xe có nhân viên",
  "Bồn tắm",
  "Được phép mang thú nuôi vào",
  "Phòng tập",
]);
const selectFile = async (e) => {
  try {
    const img = new FormData();
    images.value.forEach((element) => {
      img.append("file", element);
    });
    const res = await axios.post("/uploadMultipleFiless", img);
    imgUrls.value = res.data;
  } catch (error) {
    console.log(error);
  }
};
const handleAdd = async () => {
  try {
    const dataAdd = {
      hotelName: hotelName.value,
      hotelType: hotelType.value,
      phone: phone.value,
      fromPrice: fromPrice.value,
      rated: rated.value,
      address: address.value,
      description: description.value,
      image: imgUrls.value.map((img) => img.fileDownloadUri).join(","),
      service: service._rawValue.join(","),
    };
    // console.log(service.value.map((item) => item.service).join(","));
    console.log(service._rawValue.join(","));
    const res = await axios.post("/v1/hotel/createhotel", dataAdd);
    emit("add");
  } catch (e) {
    console.log(e);
  }
};
const handleClose = () => {
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
                  v-model="hotelName"
                  required
                  label="Tên khách sạn"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-combobox
                  color="primary"
                  v-model="hotelType"
                  label="Loại khách sạn"
                  :items="['Homestay', 'Hotel', 'Resort', 'Villa']"
                ></v-combobox>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  label="Giá từ"
                  v-model="fromPrice"
                  :options="{ precision: 2, decimal: '.', thousand: '.' }"
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
                <v-combobox
                  v-model="service"
                  :items="items"
                  label="I use chips"
                  multiple
                  chips
                ></v-combobox>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  v-model="rated"
                  label="Đánh giá"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-file-input
                  color="primary"
                  multiple=""
                  v-model="images"
                  label="File input"
                  ref="file"
                  @change="selectFile"
                ></v-file-input>
                <div v-if="imgUrls.length">
                  <img
                    style="width: 100px; height: 140px"
                    v-for="(img, index) in imgUrls"
                    :key="index"
                    :src="img.fileDownloadUri"
                    alt=""
                  />
                </div>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="address"
                  label="Địa chỉ"
                  persistent-hint
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="description"
                  label="Mô tả"
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
          <v-btn color="errorr" variant="text" @click="handleClose">
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
