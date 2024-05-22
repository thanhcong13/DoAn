<script setup>
import { onMounted, ref, watch } from "vue";
import router from "@/router";
import axios from "axios";
import moment from "moment";
import HotelAdd from "./HotelAdd.vue";
import HotelEdit from "./HotelEdit.vue";

const desserts = ref([]);
const isOpenEdit = ref(false);
const page = ref(1);
const isOpenAdd = ref(false);
const dataEdit = ref();
const totalPage = ref(0);
const totalRecords = ref(0);

onMounted(() => {
  getData();
});
watch(page, () => {
  getData();
});
const getData = async () => {
  try {
    const res = await axios.get(
      `http://localhost:8080/api/v1/hotel/getallhotel?page=${page.value}&size=3`
    );
    desserts.value = res.data.data;
    totalPage.value = res.data.totalPages;
    totalRecords.value = res.data.totalRecords;
  } catch (e) {
    console.error(e);
  }
};
const openDialogAdd = (data) => {
  console.log(data);
  isOpenAdd.value = true;
};
const handleAdd = () => {
  console.log("run");
  isOpenAdd.value = false;
  getData();
};
const handleClose = () => {
  isOpenAdd.value = false;
};
const openDialogEdit = (data) => {
  console.log(data);
  dataEdit.value = data;
  isOpenEdit.value = true;
};

const openDialogDelete = (data) => {};
const handleUpdate = () => {
  console.log("run");
  isOpenEdit.value = false;
  getData();
};
const handleUpdateClose = () => {
  isOpenEdit.value = false;
};
</script>

<template>
  <!-- ----------------------------------------------------------------------------- -->
  <!-- Dencity -->
  <!-- ----------------------------------------------------------------------------- -->
  <div>
    <div class="mt-4">
      <h2 class="heading-tb mb-3">Thông tin khách sạn</h2>
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
              Tên khách sạn
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Loại khách sạn
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Địa chỉ
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Điện thoại
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Giá từ
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Mô tả
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Hình ảnh
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
              Trạng thái
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
            <td class="text-center">{{ item.hotelName }}</td>
            <td class="text-center">{{ item.hotelType }}</td>
            <td style="min-width: 200px" class="text-center">
              {{ item.address }}
            </td>
            <td style="min-width: 140px" class="text-center">
              {{ item.phone }}
            </td>
            <td class="text-center">{{ item.fromPrice }} VNĐ</td>
            <td style="min-width: 230px" class="text-center">
              {{ item.description }}
            </td>
            <td class="text-center" style="min-width: 350px">
              <div v-if="item.image">
                <!-- :src="`${item.image?.split(',')?.[0]}`" -->
                <img
                  v-for="img in item.image?.split(',')"
                  style="
                    height: auto;
                    width: 130px;
                    border-radius: 4px;
                    margin: 4px;
                  "
                  alt=""
                  :src="`${img}`"
                />
                <img src="" alt="" />
              </div>
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
            <td style="min-width: 130px" class="text-center">
              {{ item.status === true ? "Đang hợp tác" : "Ngừng hợp tác" }}
            </td>
            <td style="min-width: 160px; text-align: center">
              <v-btn
                icon="mdi-pencil"
                color="warning"
                @click="openDialogEdit(item)"
                class="mr-1"
                size="small"
              ></v-btn>
              <!-- <v-btn
                icon="mdi-delete"
                @click="openDialogDelete(item)"
                color="errorr"
                size="small"
              ></v-btn> -->
              <v-btn
                icon="mdi-eye"
                color="warning"
                class="mr-1"
                size="small"
                @click="router.push('/ui-components/hotels/' + item.id)"
              ></v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </div>
  </div>
  <div class="text-center">
    <v-pagination
      v-model="page"
      :length="totalPage"
      color="success"
      rounded="circle"
    ></v-pagination>
  </div>
  <HotelAdd
    :isOpen="isOpenAdd"
    @close="handleClose"
    @add="handleAdd"
    :data="dataAdd"
  />
  <HotelEdit
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
