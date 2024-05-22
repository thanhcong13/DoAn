<script setup>
import { ref, defineProps } from "vue";
import axios from "../../../services/axios";
import { useRoute } from "vue-router";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["add", "close"]);

const roomName = ref("");
const price = ref();
const capacity = ref("");
const bedType = ref("");
const hotelId = ref();
const images = ref("");
const imgUrls = ref([]);
const route = useRoute();

const selectFile = async (e) => {
  try {
    const img = new FormData();
    img.append("file", images.value[0]);
    const res = await axios.post("/uploadMultipleFiless", img);
    imgUrls.value = res.data;
  } catch (error) {
    console.log(error);
  }
};
const handleAdd = async () => {
  try {
    const dataAdd = {
      roomName: roomName.value,
      price: price.value,
      bedType: bedType.value,
      capacity: capacity.value,
      image: imgUrls.value[0].fileDownloadUri,
      hotelId: route.params.id,
    };
    const res = await axios.post(
      "http://localhost:8080/api/v1/room/create",
      dataAdd
    );
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
                  v-model="roomName"
                  required
                  label="Tên phòng"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-combobox
                  color="primary"
                  v-model="bedType"
                  label="Loại phòng"
                  :items="['Giường đơn', 'Giường đôi', 'Nhiều giường']"
                ></v-combobox>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  label="Diện tích"
                  v-model="capacity"
                  required="không được để trống!!"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  color="primary"
                  v-model="price"
                  label="Giá"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-file-input
                  color="primary"
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
