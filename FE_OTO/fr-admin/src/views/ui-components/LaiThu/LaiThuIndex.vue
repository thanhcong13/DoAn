<template>
  <div>
    <div class="mt-4">
      <h1 class="heading-tb mt-3 mb-5">Thông tin lái thử</h1>
    </div>
    <div>
        <v-btn @click="exportExcel()" class="mb-4" color="secondary">Xuất file</v-btn>
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
            Tên khách hàng
          </th>
          <th
            class="text-subtitle-1 font-weight-bold text-center font-weight-black"
          >
            Số điện thoại
          </th>
          <th
            class="text-subtitle-1 font-weight-bold text-center font-weight-black"
          >
            Tên sản phẩm
          </th>
          <th
            class="text-subtitle-1 font-weight-bold text-center font-weight-black"
          >
            Ngày lái thử
          </th>
          <!-- <th class="text-subtitle-1 font-weight-bold text-center font-weight-black">Chức năng</th> -->
          <!-- <th>Ghi chú</th> -->
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in desserts" :key="item.maLT">
          <td class="text-center">
            {{ item.maLT }}
          </td>
          <td class="text-center">{{ item.tenKH }}</td>
          <td class="text-center">{{ item.sdtkh }}</td>
          <td class="text-center">{{ item.tenSP }}</td>
          <td class="text-center">{{ item.ngayLT }}</td>
          <!-- <td class="text-center">{{ item.note }}</td> -->
          <!-- <td style="min-width: 160px; text-align: center">
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
            </td> -->
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
</template>

<script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
const desserts = ref([]);
const isOpenAdd = ref(false);
const totalPage = ref(0);
const totalRecords = ref(0);
const dialog = ref(false);
const page = ref(1);
const dataAdd = ref();
const deleteData = ref();
const dataEdit = ref();
const isOpenEdit = ref(false);
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
      `http://localhost:5222/api/LaiThu/GetByLaiThu?page=${page.value}&pageSize=5`
    );
    desserts.value = res.data.innerBody;
    totalPage.value = res.data.count;
    if (totalPage.value % 5 == 0) {
      numberPage.value = totalPage.value / 5;
    } else {
      numberPage.value = totalPage.value / 5 + 1;
    }
    totalRecords.value = res.data.count;
  } catch (e) {
    console.error(e);
  }
};

const exportExcel = async () => {
  try {
    // Gọi API để lấy file Excel từ server
    const response = await axios.get('http://localhost:5222/api/LaiThu/ExportToExcel/export', {
      responseType: 'blob', // Đặt responseType thành 'blob' để trả về dữ liệu dạng Blob
    });

    // Tạo URL cho Blob
    const url = window.URL.createObjectURL(new Blob([response.data]));

    // Tạo một thẻ a để tải xuống file
    const a = document.createElement('a');
    a.href = url;
    const currentDate = new Date();
    a.download = `LaiThu - ${currentDate}.xlsx`; // Đặt tên file mà bạn muốn
    document.body.appendChild(a);
    a.click();

    // Xóa URL sau khi tải xuống hoàn tất
    window.URL.revokeObjectURL(url);
  } catch (error) {
    console.error('Error exporting Excel file:', error);
  }
};
const openDialogAdd = () => {
  isOpenAdd.value = true;
};
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
    "http://localhost:5222/api/BaoDuong/Delete/" + deleteData.value.maBD
  );
  dialog.value = false;
  getData();
};
</script>

<style>
.heading-tb {
  text-align: center;
  text-transform: uppercase;
}
</style>
