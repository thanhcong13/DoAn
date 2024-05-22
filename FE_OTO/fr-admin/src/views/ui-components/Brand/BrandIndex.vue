<template>
  <div>
    <div class="mt-4">
      <h1 class="heading-tb mt-3">Thông tin hãng xe</h1>
    </div>
    <div>
      <v-btn @click="openDialogAdd(item)" class="mb-4" color="secondary">Thêm mới</v-btn>
    </div>
    <v-table>
      <thead>
        <tr>
          <th class="text-subtitle-1 font-weight-bold text-center font-weight-black">
            ID
          </th>
          <th class="text-subtitle-1 font-weight-bold text-center font-weight-black">
            Tên
          </th>
          <th class="text-subtitle-1 font-weight-bold text-center font-weight-black">
            Chức năng
          </th>
          <!-- <th>Ghi chú</th> -->
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in desserts" :key="item.maHang">
          <td class="text-center">
            {{ item.maHang }}
          </td>
          <td class="text-center">{{ item.tenHang }}</td>
          <!-- <td class="text-center">{{ item.note }}</td> -->
          <td style="min-width: 160px; text-align: center">
            <v-btn
              icon="mdi-pencil"
              color="warning"
              @click="openDialogEdit(item)"
              class="mr-1"
              size="small"
            ></v-btn>
            <v-btn
              icon="mdi-delete"
              @click="openDialogDelete(item)"
              color="errorr"
              size="small"
            ></v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </div>
  <template>
    <v-row justify="space-around">
      <v-dialog
        v-model="dialog"
        transition="dialog-top-transition"
        persistent
        width="auto"
      >
        <v-card>
          <v-card-title class="text-h5"> Cảnh báo! </v-card-title>
          <v-card-text>Bạn có chắc muốn xóa không?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="red-darken-1" variant="text" @click="dialog = false">
              Không
            </v-btn>
            <v-btn color="green-darken-1" variant="text" @click="handleDelete(item)">
              Đồng ý
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
  </template>
  <div class="text-center">
    <v-pagination
      v-model="page"
      :length="numberPage"
      color="success"
      rounded="circle"
    ></v-pagination>
  </div>
  <BrandAdd :isOpen="isOpenAdd" @close="handleClose" @add="handleAdd" :data="dataAdd" />
  <BrandEdit
    :isOpen="isOpenEdit"
    @update="handleUpdate"
    @close="handleUpdateClose"
    :data="dataEdit"
  />
</template>

<script setup lang="ts">
import axios from "axios";
import { ref, onMounted, watch } from "vue";
import BrandAdd from "./BrandAdd.vue";
import BrandEdit from "./BrandEdit.vue";
const desserts = ref([]);
const isOpenAdd = ref(false);
const totalPage = ref(0);
const totalRecords = ref(0);
const isOpenEdit = ref(false);
const dataEdit = ref();
const dialog = ref(false);
const page = ref(1);
const deleteData = ref();
const numberPage = ref(Number);
const handleClose = () => {
  isOpenAdd.value = false;
};

const handleAdd = () => {
  console.log("run");
  isOpenAdd.value = false;
  getData();
};

const getData = async () => {
  try {
    const res = await axios.get(
      `http://localhost:5222/api/HangXe/GetPageHangXe?page=${page.value}&pageSize=5`
    );
    desserts.value = res.data.innerBody;
    totalPage.value = res.data.count;
    if(totalPage.value % 5 == 0){
      numberPage.value = totalPage.value / 5;
    }
    else{
      numberPage.value= totalPage.value / 5 + 1;
    }
    totalRecords.value = res.data.count;
  } catch (e) {
    console.error(e);
  }
};
const openDialogAdd = (data) => {
  console.log(data);
  isOpenAdd.value = true;
};
onMounted(() => {
  getData();
});

watch(page,() => {
  getData();
});
const openDialogEdit = (item) => {
  isOpenEdit.value = true;
  dataEdit.value = item;
};
const openDialogDelete = (data) => {
  try {
    deleteData.value = data;
    dialog.value = true;
  } catch (e) {
    console.log(e);
  }
};
const handleDelete = async () => {
  const id = deleteData.value.maHang;
  const res = await axios.delete(
    "http://localhost:5222/api/HangXe/Delete/" + id
  );
  dialog.value = false;
  getData();
};
const handleUpdateClose = () => {
  isOpenEdit.value = false;
};
</script>

<style>
.heading-tb {
  text-align: center;
  text-transform: uppercase;
}
</style>
