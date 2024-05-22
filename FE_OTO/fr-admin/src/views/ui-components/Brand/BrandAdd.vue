<template>
  <v-row justify="center">
    <v-dialog v-model="isOpen" width="auto" persistent>
      <v-card>
        <v-card-title>
          <span class="text-h5 text-uppercase">Thêm mới</span>
        </v-card-title>
        <v-card-text
          style="max-height: 400px; min-width: 800px; overflow-x: auto"
        >
            <v-row>
              <v-col cols="9">
                <v-text-field
                  color="primary"
                  v-model="tenHang"
                  required
                  label="Tên hãng"
                ></v-text-field> 
              </v-col>
            </v-row>
            
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

<script setup >
import axios from "axios";
import { ref, defineProps } from "vue";
const props = defineProps({
  isOpen: Boolean,
  data: Object,
});
const emit = defineEmits(["add", "close"]);
const tenHang = ref("");
const handleClose = () => {
  emit("close");
};

const handleAdd = async () => {
  try {
    const entity = {
      tenHang: tenHang.value,
    }
    const res = await axios.post('http://localhost:5222/api/HangXe/Post',entity)
    emit("add");
  } catch (error) {
    console.log(error);
    
  }
}

</script>

<style>

</style>