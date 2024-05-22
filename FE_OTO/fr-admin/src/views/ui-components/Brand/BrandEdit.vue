
<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Cập nhật</span>
        </v-card-title>
        <v-card-text
          style="max-height: 400px; min-width: 800px; overflow-x: auto"
        >
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="tenHang"
                  required
                  label="Tên hãng"
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
          <v-btn color="success" variant="text" @click="handleUpdate">
            Cập nhật
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script setup>
import { ref, defineProps, watch } from "vue";
import axios from "axios";
import axioss from "../../../services/axios";

const props = defineProps({
  isOpen: Boolean,
  data: Object,
});

const emit = defineEmits(["update", "close"]);
const tenHang = ref("");
const maHang = ref("");

watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      tenHang.value = props.data?.tenHang;
      maHang.value = props.data?.maHang
    }
  }
);
const handleUpdate = async () => {
  try {
    const entity = {
      tenHang: tenHang.value,
      maHang: maHang.value,
    };
    const res = await axios.put(
      "http://localhost:5222/api/HangXe/Put/" + props.data?.maHang,
      entity
    );
    handleClose();
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
