<script setup>
import { onMounted, ref, watch } from "vue";
import axios from "axios";
import AdminEdit from "./AdminEdit.vue";
import AdminAdd from "./AdminAdd.vue";
import moment from "moment";
const desserts = ref([]);
const page = ref(1);
const isOpenEdit = ref(false);
const isOpenAdd = ref(false);
const dataEdit = ref();
const totalPage = ref(0);
const totalElements = ref(0);

onMounted(() => {
  getData();
  // test();
});

watch(page, () => {
  getData();
});
const getData = async () => {
  try {
    const res = await axios.get(
      `http://localhost:8080/api/v1/user/getAllUserByRoleAdmin?page=${page.value}&size=5`
    );
    console.log(res.data.content);
    desserts.value = res.data.content;
    totalPage.value = res.data.totalPages;
    totalElements.value = res.data.totalElements;
  } catch (error) {
    console.error(error);
  }
};

// const test  = async () =>{ await axios.get(`http://localhost:8080/api/v1/user/getAllUserByRoleName`).then(res => {
//   debugger;
//   if(res != null) {
//     console.log(res.data.data)
//   }
// })
// }
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
const handleClose = () => {
  isOpenEdit.value = false;
};
const openDialogAdd = () => {
  isOpenAdd.value = true;
};
const handleAdd = () => {
  console.log("run");
  isOpenAdd.value = false;
  getData();
};
const handleAddClose = () => {
  isOpenAdd.value = false;
};
</script>

<template>
  <!-- ----------------------------------------------------------------------------- -->
  <!-- Fixed Header -->
  <!-- ----------------------------------------------------------------------------- -->
  <div>
    <h2 class="text-center text-uppercase">Thông tin người quản trị</h2>
    <div>
      <v-btn style="cursor: default" variant="outlined" color="primary"
        >Tổng khách hàng: {{ totalElements }}</v-btn
      >
    </div>
    <div>
      <v-btn @click="openDialogAdd()" class="mb-4 mt-3" color="secondary"
        >Thêm mới</v-btn
      >
    </div>
    <div class="mt-4">
      <v-table fixed-header height="300px">
        <thead>
          <tr>
            <th class="text-center">STT</th>
            <th class="text-center">Tài khoản</th>
            <th class="text-center">Họ và tên</th>
            <th class="text-center">Điện thoại</th>
            <th class="text-center">Địa chỉ</th>
            <th class="text-center">Ngày tạo</th>
            <th class="text-center">Ngày sửa</th>
            <th class="text-center">Chúc năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in desserts" :key="item.id">
            <td class="text-center">{{ item.id }}</td>
            <td class="text-center">{{ item.username }}</td>
            <td class="text-center">{{ item.fullName }}</td>
            <td class="text-center">{{ item.phone }}</td>
            <td class="text-center">{{ item.address }}</td>
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
                icon="mdi-pencil"
                color="warning"
                class="mr-1"
                size="small"
                @click="openDialogEdit(item)"
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

    <div class="text-center">
      <v-pagination
        v-model="page"
        :length="totalPage"
        color="success"
        rounded="circle"
      ></v-pagination>
    </div>
  </div>
  <AdminAdd
    :isOpen="isOpenAdd"
    @add="handleAdd"
    @close="handleAddClose"
    :data="dataAdd"
  />
  <AdminEdit
    :isOpen="isOpenEdit"
    @update="handleUpdate"
    @close="handleClose"
    :data="dataEdit"
  />
</template>
<style scoped>
.text-uppercase {
  text-transform: uppercase;
}
</style>
