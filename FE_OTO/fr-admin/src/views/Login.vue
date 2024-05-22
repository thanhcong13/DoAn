<script setup>
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const router = useRouter();

const tenTaiKhoan = ref("");
const matKhau = ref("");
const login = async () => {
  try {
    const request = {
      tenTaiKhoan: tenTaiKhoan.value,
      matKhau: matKhau.value,
    };
    const res = await axios.post(`http://localhost:5222/api/TaiKhoan/Login`, request);
    console.log(res.data);
    if (res.data.statusCode != 401) {
      router.push("/");
      // localStorage.setItem(`id`, res.data.id);
      // localStorage.setItem(`token`, res.data.accessToken);
      console.log(res.data.innerBody.TenTaiKhoan);
      localStorage.setItem(`username`, res.data.innerBody.TenTaiKhoan);
      // localStorage.setItem(`email`, res.data.email);
    }
  } catch (error) {
    console.log(error);
  }
};
</script>
<template>
  <div class="bg-img">
    <v-container class="container">
      <div class="row justify-center">
        <div class="col-md-6 col-lg-4">
          <div class="login-wrap pa-0 mt-16">
            <h2 class="mb-4 text-white mt-6 text-center">Đăng nhập hệ thống</h2>
            <div class="d-flex justify-center">
              <v-form style="width:30%" class=" mt-16" @submit.prevent="login">
                <div class="form-group mt-6 w-50">
                  <input
                    class="input-login pa-5 mb-8"
                    type="text"
                    v-model="tenTaiKhoan"
                    placeholder="Username"
                    required
                  />
                </div>
                <div class="form-group">
                  <input
                    class="input-login pa-5"
                    type="password"
                    v-model="matKhau"
                    placeholder="Password"
                    required
                  />
                </div>
                <div class="form-group w-100 mt-8">
                  <button type="submit" class="w-100 pa-5 btn-login"> Login </button>
                </div>
              </v-form>
            </div>
          </div>
        </div>
      </div>
    </v-container>
  </div>
</template>
<style>
.bg-img {
  background: linear-gradient(rgba(64, 6, 97, 0.4), rgba(64, 6, 97, 0.4)),
    url('https://static.danhgiaxe.com/data/201525/aston-martin-vanquish_6443.jpg') top center / cover no-repeat;
  height: 100vh;
}
.input-login {
  border-radius: 20px !important;
  outline: none;
  width: 100%;
  color: white;
  background-color: rgba(144, 126, 154, 0.4);
}
::placeholder {
  color: white;
}
.rg_content {
  font-size: 20px;
}
.rg_link {
  font-size: 20px;
  color: #ee8a6a;
  font-weight: 600;
}

.text-white{
  color:  white !important;
}

.btn-login{
  background-color: #ee8a6a;
  color: white;
  border-radius: 14px;
  text-transform: uppercase;
  font-size: 16px;
}
</style>
