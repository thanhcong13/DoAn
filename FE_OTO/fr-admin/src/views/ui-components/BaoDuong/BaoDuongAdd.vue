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
          <!-- <v-row>
              <v-col cols="12">
                <v-text-field
                  color="primary"
                  v-model="noiDung"
                  required
                  label="Nội dung"
                ></v-text-field>
              </v-col>
            </v-row> -->
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
const tenKH = ref("");
const emailKH = ref("");
const sDTKH = ref("");
const tenSP = ref("");
const ngayBD = ref(Date);
const tenCH = ref("");
const listCH = ref();
const handleClose = () => {
  emit("close");
};

const getAllCH = async () => {
  const res = await axios.get("http://localhost:5222/api/SanPham/GetAllCH");
  listCH.value = res.data.innerBody.map((item) => item.TenCH);
};

onMounted(() => {
  getAllCH();
});
const handleAdd = async () => {
  try {
    const entity = {
      tenKH: tenKH.value,
      emailKH: emailKH.value,
      sDTKH: sDTKH.value,
      tenSP: tenSP.value,
      ngayBD: ngayBD.value,
      tenCH: tenCH.value,
    };
    const res = await axios.post("http://localhost:5222/api/BaoDuong/Post", entity);
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
