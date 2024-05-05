import { createRouter, createWebHistory } from 'vue-router'
import RegisterPage from '../views/RegisterPage.vue'
import LoginPage from '../views/LoginPage.vue'
import Home from '../views/Home.vue'
import UserInformation from '../views/user/UserInformation'
import CarForm from '@/views/car/CarForm.vue'


const routes = [
  {
    path: '/Register',
    name: 'Register',
    component: RegisterPage
  },
  {
    path: '/Login',
    name: 'Login',
    component: LoginPage
  },
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/UserInfo',
    name: 'UserInfo',
    component: UserInformation
  },
  {
    path: '/CarCreator',
    name: 'CarCreator',
    component: CarForm
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})


export default router