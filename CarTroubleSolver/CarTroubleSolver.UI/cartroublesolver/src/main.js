import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';

import 'primevue/resources/themes/aura-light-green/theme.css'


import Button from "primevue/button"


const app = createApp(App);
app.use(PrimeVue);
app.component('BaseButton', Button);

app.mount('#app')
