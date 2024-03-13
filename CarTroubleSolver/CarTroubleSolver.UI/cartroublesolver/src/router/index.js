import { createRouter, createWebHistory } from 'vue-router'
import RegisterPage from '../components/RegisterPage.vue'
import LoginPage from '../components/LoginPage.vue'

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
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
export default router