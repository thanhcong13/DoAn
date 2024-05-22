<script setup>
import { onMounted, defineProps, ref, watch } from "vue";
import router from "@/router";
import axios from "axios";
import moment from "moment";

const desserts = ref([]);
const isOpenEdit = ref(false);
const page = ref(1);
const isOpenAdd = ref(false);
const dataEdit = ref();
const totalPage = ref(0);
const dataConfirm = ref([]);
const isConfirm = ref(true);
const cancelConfirm = ref(false);
const totalRecords = ref(0);

onMounted(() => {
  getData();
});
watch(desserts, () => {
  getData();
});
const getData = async () => {
  try {
    const res = await axios.get(
      `http://localhost:8080/api/v1/order/getallhotel?page=${page.value}&size=5`
    );
    desserts.value = res.data.data;
    totalPage.value = res.data.totalPages;
    totalRecords.value = res.data.totalRecords;
  } catch (e) {
    console.error(e);
  }
};
const confirmOrder = async (data) => {
  try {
    console.log(data);
    dataConfirm.value = data;
    const success = {
      isConfirm: isConfirm.value,
    };
    const res = await axios.put(
      "http://localhost:8080/api/v1/order/confirm/" + dataConfirm.value.id,
      success
    );
  } catch (e) {
    console.log(e);
  }
};
const cancelOrder = async (data) => {
  dataConfirm.value = data;
  const cancel = {
    isConfirm: cancelConfirm.value,
  };
  const res = await axios.put(
    "http://localhost:8080/api/v1/order/cancel/" + dataConfirm.value.id,
    cancel
  );
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
      <h2 class="heading-tb mb-3">Thông tin đặt phòng</h2>
      <div>
        <!-- <v-btn @click="openDialogAdd(item)" class="mb-4" color="secondary"
          >Thêm mới</v-btn
        > -->
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
              Tên phòng
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Hình ảnh
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Giá
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Số ngày thuê
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Tổng tiền
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Họ tên
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Tài khoản
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Số điện thoại
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
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Ghi chú
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in desserts" :key="item.id">
            <td class="text-center">{{ item.id }}</td>
            <td style="min-width: 300px" class="text-center">
              {{ item.hotelName }}
            </td>
            <td class="text-center">{{ item.roomName }}</td>
            <td class="text-center" style="min-width: 200px">
              <div v-if="item.image">
                <!-- :src="`${item.image?.split(',')?.[0]}`" -->
                <img
                  style="
                    height: auto;
                    width: 130px;
                    border-radius: 4px;
                    margin: 4px;
                  "
                  alt=""
                  :src="`${item.image}`"
                />
                <img src="" alt="" />
              </div>
            </td>
            <td style="min-width: 120px" class="text-center">
              {{ item.price }}
            </td>
            <td class="text-center">{{ item.dayNum }}</td>
            <td class="text-center">{{ item.totalMoney }} VNĐ</td>
            <td style="min-width: 160px" class="text-center">
              {{ item.fullName }}
            </td>
            <td class="text-center">{{ item.username }}</td>
            <td class="text-center">{{ item.phone }}</td>

            <td class="text-center" style="min-width: 200px">
              {{
                moment(item.createdDate).format("dddd, MMMM Do YYYY, h:mm:ss a")
              }}
            </td>
            <td class="text-center" style="min-width: 200px">
              {{
                moment(item.modifiedDate).format(
                  "dddd, MMMM Do YYYY, h:mm:ss a"
                )
              }}
            </td>
            <td style="min-width: 130px" class="text-center">
              {{ item.status === true ? "Đã thanh toán" : "Chưa thanh toán" }}
            </td>
            <td style="min-width: 300px; text-align: center">
              <div v-if="item.isConfirm === null">
                <v-btn
                  color="success"
                  @click="confirmOrder(item)"
                  class="mr-1"
                  size="small"
                  >Xác nhận</v-btn
                >
                <v-btn
                  @click="cancelOrder(item)"
                  color="errorr"
                  class="mr-1"
                  size="small"
                  >Hủy</v-btn
                >
                <!-- @click="router.push('/ui-components/hotels/' + item.id)" -->
              </div>
              <div v-else>
                <span class="bd-confirm">{{
                  item.isConfirm === true ? "Đã xác nhận" : "Từ chối"
                }}</span>
              </div>
            </td>
            <td style="min-width: 130px" class="text-center">
              {{ item.delete === true ? "" : "Đã hủy" }}
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
</template>
<style>
.heading-tb {
  text-align: center;
  text-transform: uppercase;
}
.bd-confirm {
  padding: 6px;
  border: 1px solid #ee8a6a;
  border-radius: 4px;
  cursor: default;
}
</style>
