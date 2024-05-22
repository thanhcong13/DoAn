<script setup>
import { onMounted, ref, watch } from "vue";
import router from "@/router";
import axios from "axios";
import moment from "moment";
import BlogAdd from "./BlogAdd.vue";
import BlogEdit from "./BlogEdit.vue";

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
      `http://localhost:8080/api/v1/blog/getblogwithpage?page=${page.value}&size=5`
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
      <h2 class="heading-tb mb-3">Danh sách bài đăng</h2>
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
              STT
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Tiêu đề
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Hình ảnh
            </th>
            <th
              class="text-subtitle-1 font-weight-bold text-center font-weight-black"
            >
              Nội dung
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
            <td style="min-width: 150px" class="text-center">
              {{ item.title }}
            </td>
            <td class="text-center">
              <img style="width: 100%" :src="`${item.image}`" alt="" />
            </td>
            <td class="text-center">{{ item.content }}</td>
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
                icon="mdi-delete"
                color="errorr"
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
  <BlogAdd
    :isOpen="isOpenAdd"
    @close="handleClose"
    @add="handleAdd"
    :data="dataAdd"
  />
  <BlogEdit
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
