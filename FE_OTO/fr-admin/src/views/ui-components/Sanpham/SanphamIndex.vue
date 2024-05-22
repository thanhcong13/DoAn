<template>
  <div class="mt-4">
    <h1 class="heading-tb mt-3">Thông tin sản phẩm</h1>
  </div>
  <div>
    <v-btn @click="openDialogAdd(item)" class="mb-4" color="secondary"
      >Thêm mới</v-btn
    >
  </div>
  <v-table>
    <thead>
      <tr>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          ID
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Hình ảnh
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Tên sản phẩm
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Tên hãng
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Tên cửa hàng
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Màu sắc
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Tình trạng
        </th>
        <th
          class="text-subtitle-1 font-weight-bold text-center font-weight-black"
        >
          Chức năng
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in desserts" :key="item.maSP">
        <td>{{ item.maSP }}</td>
        <td>
          <img
            style="height: auto; width: 130px; border-radius: 4px; margin: 4px"
            :src="item.image"
            alt=""
          />
        </td>
        <td class="text-center">{{ item.tenSP }}</td>
        <td class="text-center">{{ item.tenHang }}</td>
        <td class="text-center">{{ item.tenCH }}</td>
        <td class="text-center">{{ item.mauSac }}</td>
        <td class="text-center">{{ item.tinhTrang }}</td>
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
          <!-- <v-btn
            icon="mdi-eye"
            color="warning"
            class="mr-1"
            size="small"
            @click="router.push('/ui-components/students/' + item.studentId)"
          ></v-btn> -->
        </td>
      </tr>
    </tbody>
  </v-table>
  <!-- <div v-if="isNullData" class="text-center mt-5">Hiện tại không có bản ghi nào!!!</div> -->
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
            <v-btn
              color="green-darken-1"
              variant="text"
              @click="handleDelete(item)"
            >
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
  <SanphamAdd
    :isOpen="isOpenAdd"
    @close="handleClose"
    @add="handleAdd"
    :data="dataAdd"
  />
  <SanPhamEdit
    :isOpen="isOpenEdit"
    @update="handleUpdate"
    @close="handleUpdateClose"
    :data="dataEdit"
  />
</template>

<script setup>
import axios from "axios";
import { ref, onMounted, watch } from "vue";
import SanphamAdd from "./SanphamAdd.vue";
import SanPhamEdit from "./SanphamEdit.vue";
const desserts = ref([]);
const isOpenAdd = ref(false);
const isOpenEdit = ref(false);
const dataEdit = ref();
const page = ref(1);
const deleteData = ref();
const totalPage = ref(0);
const totalRecords = ref(0);
const dialog = ref(false);
const isNullData = ref(false);
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
      `http://localhost:5222/api/SanPham/GetPageProduct?page=${page.value}&pageSize=5`
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
  } catch (error) {
    console.log(error);
  }
};
const openDialogAdd = (data) => {
  isOpenAdd.value = true;
};

watch(() => {
  getData();
});
onMounted(() => {
  getData();
});
const openDialogEdit = (data) => {
  console.log(data);
  dataEdit.value = data;
  isOpenEdit.value = true;
};
const handleUpdateClose = () => {
  isOpenEdit.value = false;
};

const handleUpdate = () => {
  isOpenEdit.value = false;
  getData();
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
  // const id = deleteData.value.studentId;
  const res = await axios.delete(
    "http://localhost:5222/api/Sanpham/Delete/"+ deleteData.value.maSP,
  );
  dialog.value = false;
  getData();
};
</script>

<style>
.heading-tb {
  text-align: center;
}
</style>
