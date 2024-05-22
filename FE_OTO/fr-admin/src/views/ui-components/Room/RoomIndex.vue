<script setup>
import { onMounted, ref, watch } from "vue";
import axios from "axios";
import moment from "moment";
import RoomAdd from "./RoomAdd.vue";
import RoomEdit from "./RoomEdit.vue";
import { useRoute } from "vue-router";
const desserts = ref([]);
const isOpenAdd = ref(false);
const isOpenEdit = ref(false);

const dataEdit = ref();
const route = useRoute();
const dialog = ref(false);
const deleteData = ref([]);
const deleteItem = ref(false);
onMounted(() => {
  console.log(route.params.id);
  getData();
});
watch(desserts, () => {
  getData();
});
const getData = async () => {
  try {
    const res = await axios.get(
      "http://localhost:8080/api/v1/hotel/getbyid/" + route.params.id
    );
    desserts.value = res.data.rooms;
  } catch (e) {
    console.error(e);
  }
};
const openDialogAdd = (data) => {
  console.log(data);
  isOpenAdd.value = true;
};
const openDialogEdit = (data) => {
  console.log(data);
  dataEdit.value = data;
  isOpenEdit.value = true;
};
const handleAdd = () => {
  console.log("run");
  isOpenAdd.value = false;
  getData();
};
const handleUpdate = () => {
  console.log("run");
  isOpenEdit.value = false;
  getData();
};
const handleClose = () => {
  isOpenAdd.value = false;
};
const handleUpdateClose = () => {
  isOpenEdit.value = false;
};
const openDelete = async (data) => {
  try {
    deleteData.value = data;
    const detele = {
      status: deleteItem.value,
    };
    const success = await axios.put(
      "http://localhost:8080/api/v1/room/delete/" + deleteData.value.id,
      detele
    );
    dialog.value = false;
  } catch (e) {
    console.log(e);
  }
};
const handleDelete = async (data) => {};
</script>

<template>
  <!-- ----------------------------------------------------------------------------- -->
  <!-- Dencity -->
  <!-- ----------------------------------------------------------------------------- -->
  <div>
    <div class="mt-4">
      <h2 class="heading-tb mb-3">Thông tin phòng</h2>
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
              Tên phòng
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Giá phòng
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Diện tích
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Loại phòng
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Hình ảnh
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Trạng thái
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Ngày tạo
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Cập nhật
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Chức năng
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in desserts" :key="item.id">
            <td class="text-center">{{ item.id }}</td>
            <td class="text-center">{{ item.roomName }}</td>
            <td class="text-center">{{ item.price }} VNĐ</td>
            <td class="text-center">{{ item.capacity }}m²</td>
            <td class="text-center">{{ item.bedType }}</td>
            <td class="text-center">
              <img
                style="height: 100px; width: 80px; border-radius: 4px"
                :src="`${item.image}`"
                alt=""
              />
            </td>
            <td class="text-center">
              {{
                item.status === false ? "Ngừng hoạt động" : "Đang được sử dụng"
              }}
            </td>
            <td class="text-center">
              {{
                moment(item.createdDate).format("dddd, MMMM Do YYYY, h:mm:ss a")
              }}
            </td>
            <td class="text-center">
              {{
                moment(item.modifiedDate).format(
                  "dddd, MMMM Do YYYY, h:mm:ss a"
                )
              }}
            </td>
            <td style="min-width: 160px; text-align: center">
              <v-btn
                @click="openDialogEdit(item)"
                icon="mdi-pencil"
                color="warning"
                class="mr-1"
                size="small"
              ></v-btn>
              <v-btn
                icon="mdi-delete"
                @click="openDelete(item)"
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
              <v-btn
                color="red-darken-1"
                variant="text"
                @click="dialog = false"
              >
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
  </div>
  <!-- <div class="text-center">
    <v-pagination
      v-model="page"
      :length="totalPages"
      color="success"
      rounded="circle"
    ></v-pagination>
  </div> -->
  <RoomAdd
    :isOpen="isOpenAdd"
    @close="handleClose"
    @add="handleAdd"
    :data="dataAdd"
  />
  <RoomEdit
    :isOpen="isOpenEdit"
    @update="handleUpdate"
    @close="handleUpdateClose"
    :data="dataEdit"
  />
</template>
<style>
.heading-tb {
  text-align: center;
  text-transform: uppercase;
}
</style>
