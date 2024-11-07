import { createRouter, createWebHistory } from 'vue-router'
import RegisterPage from '../views/RegisterPage.vue'
import LoginPage from '../views/LoginPage.vue'
import Home from '../views/Home.vue'
import UserInformation from '../views/user/UserInformation'
import CarForm from '@/views/car/CarForm.vue'
import CarDetails from '@/views/car/CarDetails.vue'
import UpdateCarDetails from '@/views/car/UpdateCarDetails.vue'
import accidentForm from '@/views/accident/accidentForm.vue'
import WorkshopDetails from '@/views/Workshop/WorkshopDetails.vue'
import WorkshopList from '@/views/Workshop/WorkshopList.vue'
import MessageDetails from '@/views/accident/MessageDetails.vue'
import StatusHistory from '@/views/Status/StatusHistory.vue'
import RepairHistory from '@/views/Repairs/RepairHistory.vue'
import RepairsHistory from '@/views/Repairs/RepairsHistory.vue'

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
  {
    path: '/CarDetails/:vin',
    name: 'CarDetails',
    component: CarDetails
  },
  {
    path: '/ChangeCarDetails/:vin',
    name: 'ChangeCarDetails',
    component: UpdateCarDetails
  },
  {
    path: '/AddAccident',
    name: 'accidentForm',
    component: accidentForm
  },
  {
    path: '/WorkshopDetails',
    name: 'WorkshopDetails',
    component: WorkshopDetails
  },
  {
    path: '/WorkshopList',
    name: 'WorkshopList',
    component: WorkshopList
  },
  {
    path: '/MessageDetails/:id',
    name: 'MessageDetails',
    component: MessageDetails,
    props: true 
  },
  {
    path: '/RepairHistory/:id',
    name: 'RepairHistory',
    component: RepairHistory,
    props: true 
  },
  {
    path: '/StatusHistory/:id',
    name: 'StatusHistory',
    component: StatusHistory,
    props: true 
  },
  {
    path: '/RepairsHistory/:id',
    name: 'RepairsHistory',
    component: RepairsHistory,
    props: true 
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})


export default router