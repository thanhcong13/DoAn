import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import "@/scss/style.scss";
import VueApexCharts from "vue3-apexcharts";
import VueNumberFormat from "vue-number-format";
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

const app = createApp(App);

app.use(VueApexCharts);
app.component('VueDatePicker', VueDatePicker);
app.use(router);
app.use(VueNumberFormat);
app.use(vuetify).mount("#app");
