import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import router from './router'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import 'primevue/resources/themes/aura-light-green/theme.css'


//Primevue Components
import FloatLabel from 'primevue/floatlabel';
import Button from "primevue/button"
import InputText from 'primevue/inputtext';
import Calendar from 'primevue/calendar';

//Datepicker
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App);
app.use(PrimeVue);
app.component('VueDatePicker', VueDatePicker);
app.component('BaseButton', Button);
app.component('InputText', InputText);
app.component('FloatLabel', FloatLabel);
app.component('Calendar', Calendar);





app.use(router);
app.mount('#app')
