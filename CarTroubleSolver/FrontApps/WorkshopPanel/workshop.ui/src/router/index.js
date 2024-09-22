import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/home/Home.vue'
import Login from '../views/Login/Login.vue'
import Register from '../views/Register/Register.vue'
import AccountDetails from '@/views/user/AccountDetails.vue'
import OpeningHoursForm from '@/views/user/OpeningHours/OpeningHoursForm.vue'

const routes = [ 
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/Register',
    name: 'Register',
    component: Register
  },
  {
    path: '/Login',
    name: 'Login',
    component: Login
  },
  {
    path: '/Account',
    name: 'Account',
    component: AccountDetails
  },
  {
    path: '/Hours',
    name: 'Hours',
    component: OpeningHoursForm
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})


export default router