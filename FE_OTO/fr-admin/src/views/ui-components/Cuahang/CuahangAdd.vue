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
                <v-col cols="12">
                  <v-text-field
                    color="primary"
                    v-model="tenCH"
                    required
                    label="Tên cửa hàng"
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
  const tenCH = ref("");
  const diaChi = ref("");
  const handleClose = () => {
    emit("close");
  };
  
 
  const handleAdd = async () => {
    try {
      const entity = {
        tenCH: tenCH.value,
        diaChi: diaChi.value
      }
      const res = await axios.post('http://localhost:5222/api/CuaHang/Post',entity)
      emit("add");
    } catch (error) {
      console.log(error);
      
    }
  }
  
  </script>
  
  <style>
  
  </style>