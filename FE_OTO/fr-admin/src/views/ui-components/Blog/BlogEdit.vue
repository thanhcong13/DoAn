<script setup>
import { ref, defineProps, watch } from "vue";
import axios from "axios";
import axioss from "../../../services/axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["update", "close"]);

const title = ref("");
const content = ref("");
const image = ref("");
const images = ref("");
const imgUrls = ref([]);

watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      title.value = props.data?.title;
      content.value = props.data?.content;
      image.value = props.data?.image;
    }
  }
);
const selectFile = async (e) => {
  try {
    const img = new FormData();
    img.append("file", images.value[0]);
    const res = await axioss.post("/uploadMultipleFiless", img);
    imgUrls.value = res.data;
  } catch (error) {
    console.log(error);
  }
};
const handleUpdate = async () => {
  try {
    const dataUpdate = {
      content: content.value,
      title: title.value,
      image: imgUrls.value[0].fileDownloadUri,
    };
    const res = await axios.put(
      "http://localhost:8080/api/v1/blog/update/" + props.data?.id,
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
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Cập nhật</span>
        </v-card-title>
        <v-card-text
          style="max-height: 400px min-width: 800px; overflow-x: auto"
        >
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="title"
                  required
                  label="Tiêu đề"
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
                <div v-else-if="image">
                  <img
                    style="width: 100px; height: 140px"
                    :src="image"
                    alt=""
                  />
                </div>
              </v-col>
              <v-col cols="12">
                <v-textarea
                  color="primary"
                  v-model="content"
                  label="Mô tả"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-textarea>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="errorr" variant="text" @click="handleClose">
            Đóng
          </v-btn>
          <v-btn color="success" variant="text" @click="handleUpdate">
            Cập nhật
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<style scoped>
.text-uppercase {
  text-transform: uppercase !important;
}
</style>
