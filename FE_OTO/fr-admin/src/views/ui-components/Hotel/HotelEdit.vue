<script setup>
import { ref, defineProps, watch } from "vue";
import axios from "axios";
import axioss from "../../../services/axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["update", "close"]);

const hotelName = ref("");
const hotelType = ref("");
const phone = ref("");
const address = ref("");
const images = ref("");
const imgUrls = ref([]);
const imgUrlsPreview = ref([]);
const fromPrice = ref("");
const rated = ref();
const description = ref("");

const service = ref([]);
const items = ref([
  "Bể bơi",
  "Bãi đỗ xe có nhân viên",
  "Bồn tắm",
  "Được phép mang thú nuôi vào",
  "Phòng tập",
]);
watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      hotelName.value = props.data?.hotelName;
      hotelType.value = props.data?.hotelType;
      phone.value = props.data?.phone;
      address.value = props.data?.address;
      fromPrice.value = props.data?.fromPrice;
      rated.value = props.data?.rated;
      description.value = props.data?.description;
      imgUrlsPreview.value = props.data?.image?.split(",");
      service.value = props.data?.service?.split(",");
    }
  }
);
const selectFile = async (e) => {
  try {
    const img = new FormData();
    images.value.forEach((element) => {
      img.append("file", element);
    });
    const res = await axioss.post("/uploadMultipleFiless", img);
    imgUrls.value = res.data;
  } catch (error) {
    console.log(error);
  }
};
const handleUpdate = async () => {
  try {
    const dataUpdate = {
      hotelName: hotelName.value,
      hotelType: hotelType.value,
      phone: phone.value,
      address: address.value,
      image: imgUrls.value.length
        ? imgUrls.value.map((img) => img.fileDownloadUri).join(",")
        : imgUrlsPreview.value,
      rated: rated.value,
      description: description.value,
      fromPrice: fromPrice.value,
      service: service._rawValue.join(","),
    };
    const res = await axios.put(
      "http://localhost:8080/api/v1/hotel/updated/" + props.data?.id,
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
                v-model="images"
                multiple=""
                label="Chọn ảnh"
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
                <!-- v-for="(img, index) in imgUrls" -->
              </div>
              <div v-else-if="imgUrlsPreview.length">
                <img
                  style="width: 100px; height: 140px"
                  v-for="(img, index) in imgUrlsPreview"
                  :key="index"
                  :src="img"
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
