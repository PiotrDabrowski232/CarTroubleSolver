import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import router from './router'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import 'primevue/resources/themes/aura-light-green/theme.css'


import Button from "primevue/button"
import InputText from 'primevue/inputtext';


import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App);
app.use(PrimeVue);
app.component('BaseButton', Button);
app.component('InputText', InputText);


app.use(router);
app.mount('#app')
