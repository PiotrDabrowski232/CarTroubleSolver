import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import router from './router'
import ToastService from 'primevue/toastservice';
import Dialog from 'primevue/dialog';

//Primevue Components
import Toast from 'primevue/toast';
import FloatLabel from 'primevue/floatlabel';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import Calendar from 'primevue/calendar';


import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import 'primevue/resources/themes/aura-light-green/theme.css'




//Datepicker
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App);
app.use(PrimeVue);
app.component('VueDatePicker', VueDatePicker);
app.component('Button', Button);
app.component('InputText', InputText);
app.component('FloatLabel', FloatLabel);
app.component('Calendar', Calendar);
app.component('Toast', Toast);
app.component('Dialog', Dialog);


app.use(router);
app.use(ToastService);
app.mount('#app')
