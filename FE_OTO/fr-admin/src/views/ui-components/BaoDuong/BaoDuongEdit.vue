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
              <v-text-field
                color="primary"
                v-model="tenKH"
                required
                label="Tên Khách hàng"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="emailKH"
                required
                label="Email khách hàng"
              ></v-text-field>
            </v-col>
            <v-col cols="4">
              <v-text-field
                color="primary"
                v-model="sDTKH"
                required
                label="Số điện thoại"
              ></v-text-field>
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
              <p>Ngày bảo dưỡng</p>
              <VueDatePicker v-model="ngayBD"></VueDatePicker>
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
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="errorr" variant="text" @click="handleClose"> Đóng </v-btn>
          <v-btn color="success" variant="text" @click="handleUpdate"> Cập nhật </v-btn>
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
const tenKH = ref("");
const maLH = ref("");
const emailKH = ref("");
const sDTKH = ref("");
const ngayBD = ref(Date);
const tenSP = ref("");
const tenCH = ref("");
watch(
  () => props.isOpen,
  (value) => {
    if (value) {
      (tenKH.value = props.data.tenKH),
        (maLH.value = props.data.maLH),
        (emailKH.value = props.data.emailKH),
        (sDTKH.value = props.data.sdtkh),
        (tenSP.value = props.data.tenSP),
        (ngayBD.value = props.data.ngayBD),
        (tenCH.value = props.data.tenCH);
    }
  }
);
const handleUpdate = async () => {
  try {
    const entity = {
      tenKH: tenKH.value,
      emailKH: emailKH.value,
      sDTKH: sDTKH.value,
      tenSP: tenSP.value,
      ngayBD: ngayBD.value,
      tenCH: tenCH.value,
    };
    const res = await axios.put(
      "http://localhost:5222/api/BaoDuong/Put/" + props.data?.maBD,
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
